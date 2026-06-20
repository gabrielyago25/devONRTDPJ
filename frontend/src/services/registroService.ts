import { regApi } from "./api";
import type { Registro } from "../types/registro";
import type { ModalRegistroRequest } from "../types/registro";
import type { FiltroRegistroRequest } from "../types/registro";

export async function listarRegistros(filtros?: FiltroRegistroRequest): Promise<Registro[]> {
  const response = await regApi.get<Registro[]>("/registros", {params: filtros,});
  return response.data;
}

export async function criarRegistro(data: ModalRegistroRequest): Promise<Registro> {
  const response = await regApi.post<Registro>("/registros", data);
  return response.data;
}

export async function excluirRegistro(id:string): Promise<void> {
  await regApi.delete(`/registros/${id}`);
}

export async function atualizarStatus(id: string, status: number): Promise<Registro> {
  const response = await regApi.patch<Registro>(`/registros/${id}/status`, {status,});
  return response.data;
}

export async function atualizarRegistro(id: string,data: ModalRegistroRequest): Promise<Registro> {
  const response = await regApi.put<Registro>(`/registros/${id}`, data);
  return response.data;
}