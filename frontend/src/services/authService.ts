import { authApi } from "./api";
import type { LoginRequest, LoginResponse } from "../types/auth";

export async function login(data: LoginRequest): Promise<LoginResponse> {
  const response = await authApi.post<LoginResponse>("/auth/login", data);
  return response.data;
}