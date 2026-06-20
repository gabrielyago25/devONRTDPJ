import { Outlet } from "react-router-dom";
import { Navbar } from "../components/Navbar";
import "./Main.css"

export function Main (){
    return (
        <div className= "app-layout">
            <Navbar />

            <main className= "app-layout">
                <Outlet />
            </main>
        </div>
    );
}