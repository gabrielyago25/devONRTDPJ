import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { RegistrosPagina } from "../pages/Registros/Registros";

const mocks = vi.hoisted(() => ({
  role: "Consulta",
  listarRegistros: vi.fn(),
}));

vi.mock("../contexts/AuthContext", () => ({
  useAuth: () => ({
    role: mocks.role,
    usuario: {
      nome: "Usuário Teste",
      email: "teste@teste.com",
    },
    token: "token-teste",
    isAuthenticated: true,
    signIn: vi.fn(),
    signOut: vi.fn(),
  }),
}));

vi.mock("../contexts/ToastContext", () => ({
  useToast: () => ({
    showToast: vi.fn(),
  }),
}));

vi.mock("../services/registroService", () => ({
  listarRegistros: mocks.listarRegistros,
  excluirRegistro: vi.fn(),
  criarRegistro: vi.fn(),
  atualizarRegistro: vi.fn(),
  atualizarStatusRegistro: vi.fn(),
}));

const registrosMock = [
  {
    id: "1",
    tipo: 1,
    nomeApresentante: "Maria Oliveira",
    cpfCnpj: "52998224725",
    dataEntrada: "2026-06-20",
    status: 1,
    observacoes: "Registro de teste",
    criadoPor: "usuario-1",
    dataCriado: "2026-06-20T10:00:00Z",
    dataAtualizado: "2026-06-20T10:00:00Z",
  },
];

describe("RegistrosPagina", () => {
  beforeEach(() => {
    mocks.listarRegistros.mockClear();
    mocks.listarRegistros.mockResolvedValue(registrosMock);
    mocks.role = "Consulta";
  });

  it("não deve exibir ações para usuário Consulta", async () => {
    render(<RegistrosPagina />);

    expect(await screen.findByText("Maria Oliveira")).toBeInTheDocument();

    expect(screen.queryByText("+ Registro")).not.toBeInTheDocument();
    expect(screen.queryByText("Ações")).not.toBeInTheDocument();
    expect(screen.queryByText("Editar")).not.toBeInTheDocument();
    expect(screen.queryByText("Excluir")).not.toBeInTheDocument();
  });

  it("deve exibir ações para usuário Administrador", async () => {
    mocks.role = "Administrador";

    render(<RegistrosPagina />);

    expect(await screen.findByText("Maria Oliveira")).toBeInTheDocument();

    expect(screen.getByText("+ Registro")).toBeInTheDocument();
    expect(screen.getByText("Ações")).toBeInTheDocument();
    expect(screen.getByText("Editar")).toBeInTheDocument();
    expect(screen.getByText("Excluir")).toBeInTheDocument();
  });

  it("deve chamar listarRegistros com filtros de tipo e status", async () => {
    mocks.role = "Administrador";
    const user = userEvent.setup();

    render(<RegistrosPagina />);

    expect(await screen.findByText("Maria Oliveira")).toBeInTheDocument();

    await user.selectOptions(screen.getByDisplayValue("Todos os Tipos"), "1");
    await user.selectOptions(screen.getByDisplayValue("Todos os Status"), "2");

    await user.click(screen.getByRole("button", { name: /filtrar/i }));

    expect(mocks.listarRegistros).toHaveBeenLastCalledWith({
      tipo: 1,
      status: 2,
      pagina: 1,
      limite: 10,
    });
  });
});