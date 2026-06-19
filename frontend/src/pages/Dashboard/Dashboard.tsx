import { useAuth } from "../../contexts/AuthContext";

export function DashboardPage() {
  const { signOut } = useAuth();

  return (
    <main>
      <h1>Dashboard</h1>

      <p>Login realizado com sucesso.</p>

      <button onClick={signOut}>Sair</button>
    </main>
  );
}