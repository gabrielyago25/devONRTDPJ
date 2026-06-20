import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import "./Navbar.css";

export function Navbar() {
    const {signOut} = useAuth();
    const navigate = useNavigate();

    function Desconectar() {
        signOut();
        navigate("/login");
    }

    return (
        <header className="navbar">
            <div className = "navbar-texto">TESTE</div>
                    <nav className = "navbar-links">
                        <Link to="/dashboard">Dashboard</Link>
                        <Link to="/registros">Registros</Link>
                    </nav>

                    <button className= "navbar-logout" onClick={Desconectar}>Sair</button>
        </header>
    );
}