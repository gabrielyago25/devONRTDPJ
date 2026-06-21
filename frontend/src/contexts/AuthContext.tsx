import {createContext, useContext, useEffect, useState} from "react";
import type { ReactNode } from "react";
import { login } from "../services/authService";
import { acessoExpirado } from "../services/authSession";

interface AuthContextType{
    token: string | null;
    role: string | null;
    usuario: {
        nome: string | null;
        email: string | null;
    } | null;
    isAuthenticated: boolean;
    signIn: (email: string, senha: string) => Promise<void>;
    signOut: () => void;
}

function obterRoleDoToken(token: string | null): string | null {
    if (!token) return null;

    try {
        const payloadBase64 = token.split(".")[1];
        const payloadJson = atob(payloadBase64);
        const payload = JSON.parse(payloadJson);

        return (
            payload.role ||
            payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
            null
        );
    } catch {
        return null;
    }
}

function obterDadosToken(token: string | null) {
    if (!token) return null;

    try {
        const payloadBase64 = token.split(".")[1];
        const payloadJson = atob(payloadBase64);
        const payload = JSON.parse(payloadJson);

        return {
            nome: payload.unique_name || payload.name || payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null,
            email: payload.email || payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || null,
        };
    } catch {
        return null;
    }
}

function obterExpiracaoDoToken(token: string): number | null {
    try {
        const payloadBase64 = token.split(".")[1]
            .replace(/-/g, "+")
            .replace(/_/g, "/");
        const payload = JSON.parse(atob(payloadBase64));

        return typeof payload.exp === "number" ? payload.exp * 1000 : null;
    } catch {
        return null;
    }
}

function obterTokenInicial(): string | null {
    const tokenSalvo = localStorage.getItem("token");
    if (!tokenSalvo) return null;

    const expiracao = obterExpiracaoDoToken(tokenSalvo);
    if (expiracao !== null && expiracao <= Date.now()) {
        acessoExpirado();
        return null;
    }

    return tokenSalvo;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider ({children} : {children: ReactNode}){
    const [token, setToken] = useState<string | null>(obterTokenInicial);

    useEffect(() => {
        if (!token) return;

        const expiracao = obterExpiracaoDoToken(token);
        if (expiracao === null) return;

        const tempoRestante = Math.max(expiracao - Date.now(), 0);
        const timer = window.setTimeout(() => {
            acessoExpirado();
            setToken(null);
        }, tempoRestante);

        return () => window.clearTimeout(timer);
    }, [token]);

    async function signIn(email: string, senha: string) {
        const response = await login({email, senha});

        localStorage.setItem("token", response.accessToken);
        setToken(response.accessToken);
    }

    function signOut(){
        localStorage.removeItem("token");
        setToken(null);
    }
    return(<AuthContext.Provider value ={{token, role:obterRoleDoToken(token),usuario:(obterDadosToken(token)), isAuthenticated: !!token, signIn, signOut,}}> {children} </AuthContext.Provider>);
    }
    // eslint-disable-next-line react-refresh/only-export-components
    export function useAuth() {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error("useAuth deve ser usado dentro de AuthProvider");
    }

    return context;
    }
