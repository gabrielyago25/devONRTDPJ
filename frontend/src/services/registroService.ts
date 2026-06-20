import { regApi } from "./api";
import type { Registro } from "../types/registro";

export async function listarRegistros(): Promise<Registro[]> {
  const response = await regApi.get<Registro[]>("/registros");

  return response.data;
}