import { useEffect, useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
import { useToast } from "../../contexts/ToastContext";
import { useNavigate } from "react-router-dom";
import { SESSION_EXPIRED_KEY } from "../../services/authSession";
import "./Login.css";

export function LoginPagina() {
  const { signIn } = useAuth();
  const { showToast } = useToast();
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const [carregando, setCarregando] = useState(false);

  useEffect(() => {
    if (sessionStorage.getItem(SESSION_EXPIRED_KEY) === "true") {
      sessionStorage.removeItem(SESSION_EXPIRED_KEY);
      showToast("Sua sessão expirou. Entre novamente para continuar.", "error");
    }
  }, [showToast]);

  async function handleSubmit(event: React.FormEvent) {
    event.preventDefault();

    try {
      setErro("");
      setCarregando(true);

      await signIn(email, senha);

      alert("Login realizado com sucesso!");
      navigate("/registros");
    } catch {
      setErro("Dados de acesso incorretos.");
    } finally {
      setCarregando(false);
    }
  }

  return (
    <main className="login-page">
      <section className="login-card" aria-labelledby="login-title">
        <h1 id="login-title">Teste Técnico ONRTDPJ</h1>
        <p className="login-desc">Informe os dados para acessar.</p>
        <form className="login-form" onSubmit={handleSubmit}>
          <div className="login-campo">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              required
            />
          </div>

          <div className="login-campo">
            <label htmlFor="senha">Senha</label>
            <input
              id="senha"
              type="password"
              value={senha}
              onChange={(event) => setSenha(event.target.value)}
              required
            />
          </div>

          {erro && (<p className="login-error" role="alert">{erro}</p>)}

          <button className="login-button" type="submit" disabled={carregando}>
            {carregando ? "Entrando..." : "Entrar"}
          </button>
        </form>
      </section>
    </main>
  );
}
