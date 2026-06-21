import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { describe, expect, it, vi } from "vitest";
import { LoginPagina } from "../pages/Login/Login";

vi.mock("../contexts/AuthContext", () => ({
  useAuth: () => ({
    signIn: vi.fn(),
  }),
}));

describe("LoginPagina", () => {
  it("deve renderizar os campos de email, senha e o botão entrar", () => {
    render(
      <MemoryRouter>
        <LoginPagina />
      </MemoryRouter>
    );

    expect(screen.getByLabelText("Email")).toBeInTheDocument();
    expect(screen.getByLabelText("Senha")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /entrar/i })).toBeInTheDocument();
  });
});