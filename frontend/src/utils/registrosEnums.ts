export function alterarTipoRegistros(tipoRegistro: number): string {
    switch (tipoRegistro){
        case 1:
            return "Contrato";
        case 2:
            return "Procuração";
        case 3:
            return "Notificação";
        default:
            return "Erro";
    }
}

export function alterarStatusRegistro(statusRegistro: number): string{
    switch(statusRegistro){
        case 1:
            return "Pendente";
        case 2:
            return "Registrado";
        case 3:
            return "Devolvido";
        default:
            return "Erro";
    }
}