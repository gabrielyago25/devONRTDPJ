import {createContext, useContext, useState} from "react";
import type { ReactNode } from "react";
import { login } from "../services/authService";

interface AuthContextType{
    token: string | null;
    isAuthenticated: boolean;
    signIn: (email: string, senha: string) => Promise<void>;
    signOut: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider ({children} : {children: ReactNode}){
    const [token, setToken] = useState<string | null>(
        localStorage.getItem("token")
    );

    async function signIn(email: string, senha: string) {
        const response = await login({email, senha});

        localStorage.setItem("token", response.accessToken);
        setToken(response.accessToken);
    }

    function signOut(){
        localStorage.removeItem("token");
        setToken(null);
    }

    return(<AuthContext.Provider value ={{token, isAuthenticated: !!token, signIn, signOut,}}> {children} </AuthContext.Provider>);
    }
    
    export function useAuth() {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error("useAuth deve ser usado dentro de AuthProvider");
    }

    return context;
    }