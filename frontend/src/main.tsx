import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { ToastExib } from "./contexts/ToastContext";

createRoot(document.getElementById('root')!).render(
  <ToastExib>
    <App />
  </ToastExib>,
)
