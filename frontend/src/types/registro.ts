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