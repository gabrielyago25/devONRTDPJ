import { createContext } from "react";
import { useContext } from "react";
import { useState } from "react";
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
    const [toast, setToast] = useState<ToastState | null>(null);
    function showToast(message: string, type: ToastType) {
        setToast({message, type});
        setTimeout(() => {setToast(null);}, 3000)
    }

    return (
        <ToastContext.Provider value={{showToast}}>
            {children}

            {toast &&(
                <div className={`toast toast-${toast.type}`}>
                    {toast.message}
                </div>
            )}
        </ToastContext.Provider>
    )
}
export function useToast() {
  const context = useContext(ToastContext);

  if (!context) {
    throw new Error("useToast deve ser usado dentro de ToastExib");
  }

    return context;
}