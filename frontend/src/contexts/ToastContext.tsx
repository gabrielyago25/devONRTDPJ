import { createContext } from "react";
import { useContext } from "react";
import { useCallback, useState } from "react";
import { useEffect, useRef } from "react";
import type { ReactNode } from "react";
import "../components/common/Toast.css"

type ToastType = "success" | "error";

interface ToastState{
    message: string;
    type: ToastType;
}

interface ToastContextType {
    showToast: (message: string, type: ToastType) => void;
}

const ToastContext = createContext<ToastContextType | undefined>(undefined);

export function ToastExib({children,}: {children: ReactNode;}) {
    const toastTimerRef = useRef<number | null>(null);
    const [toast, setToast] = useState<ToastState | null>(null);
    const showToast = useCallback((message: string, type: ToastType) => {
        if (toastTimerRef.current !== null){
            window.clearTimeout(toastTimerRef.current);
        }

        setToast({message, type});

        toastTimerRef.current = window.setTimeout(() => {
            setToast(null);
            toastTimerRef.current = null;
        }, 3000);
    }, []);
    useEffect (() => {
        return () => {
            if (toastTimerRef.current !== null){
                window.clearTimeout(toastTimerRef.current);
            }
        };
    }, []);
    return (
        <ToastContext.Provider value={{showToast}}>
            {children}

            {toast &&(
                <div className={`toast toast-${toast.type}`}
                role={toast.type === "error" ? "alert" : "status"}
                aria-live={toast.type === "error" ? "assertive": "polite"}>
                    {toast.message}
                </div>
            )}
        </ToastContext.Provider>
    )
}
    // eslint-disable-next-line react-refresh/only-export-components
    export function useToast() {
    const context = useContext(ToastContext);

    if (!context) {
        throw new Error("useToast deve ser usado dentro de ToastExib");
    }

        return context;
    }
