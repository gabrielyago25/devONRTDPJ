import { useAuth } from "../../contexts/AuthContext";

export function DashboardPagina() {
  const { signOut } = useAuth();

  return (
    <section>
      <h1>Dashboard</h1>

      <p>Login realizado com sucesso.</p>

    </section>
  );
}