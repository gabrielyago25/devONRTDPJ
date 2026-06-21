export function formatarData(data: string): string {
    const [ano, mes, dia] = data.split("-");

    return `${dia}/${mes}/${ano}`;
}

export function formatarDataHora(data: string): string {
    return new Date(data).toLocaleString("pt-BR");
}