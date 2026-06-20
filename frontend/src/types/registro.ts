export interface Registro {
    id: string;
    tipo: number;
    nomeApresentante: string;
    cpfCnpj: string;
    dataEntrada:string;
    status: number;
    observacoes?: string;
    criadoPor: string;
    dataCriado: string;
    dataAtualizado: string;
}

export interface ModalRegistroRequest {
    tipo: number;
    nomeApresentante: string;
    cpfCnpj: string;
    dataEntrada: string;
    observacoes: string;
}

export interface FiltroRegistroRequest {
  tipo?: number;
  status?: number;
  pagina: number;
  limite: number;
}