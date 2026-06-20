import { useEffect, useState } from "react";
import { listarRegistros, excluirRegistro } from "../../services/registroService";
import type { Registro } from "../../types/registro";
import { alterarStatusRegistro, alterarTipoRegistros } from "../../utils/registrosEnums";
import { formatarDataHora } from "../../utils/dataFormat";
import { formatarCpfCnpj } from "../../utils/cpfCnpj";
import { RegistroModal } from "./NovoRegistroModal";
import { StatusRegistroModal } from "./StatusRegistroModal";
import { EditarRegistroModal } from "./EditarRegistroModal";
import { useToast } from "../../contexts/ToastContext";
import "./Registros.css";


export function RegistrosPagina() {
    const [registros, setRegistros] = useState<Registro[]>([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState("");
    const [modalAberto, setModalAberto] = useState(false);
    const [registroStatus, setRegistroStatus] = useState<Registro | null>(null);
    const [registroEdicao, setRegistroEdicao] = useState<Registro | null>(null);
    const [tipoFiltro, setTipoFiltro] = useState("");
    const [statusFiltro, setStatusFiltro] = useState("");
    const [pagina, setPagina] = useState(1);
    const [limite, setLimite] = useState(10);
    const {showToast} = useToast();

    async function carregarRegistros() {
    try {
        setErro("");
        setCarregando(true);

        const dados = await listarRegistros({
            tipo: tipoFiltro ? Number(tipoFiltro) : undefined,
            status: statusFiltro ? Number(statusFiltro) : undefined,
            pagina,
            limite,});
        setRegistros(dados);
    } catch {
        setErro("Erro ao carregar registros.");
    } finally {
        setCarregando(false);
    }
    }

    useEffect(() => {
        carregarRegistros();
    }, [pagina, limite]);


    if (carregando) {
        return <p>Carregando registros...</p>;
    }

    if (erro) {
        return <p>{erro}</p>;
    }

    async function removeRegistro(id:string) {
        const confirmar = window.confirm("Deseja excluir o registro?");

        if(!confirmar) {
            return;
        }
        try {
            await excluirRegistro(id);
            await carregarRegistros();

            showToast("Registro removido com sucesso!", "success");
        } catch {
            showToast("Erro ao excluir registro", "error");
        }
    }
    return (       
        <div className= "registros-pagina">
            <div className= "registros-header">
            <h1>Registros</h1>
            <button className="novo-registro-button" onClick={() => setModalAberto(true)}>+ Registro</button>
            </div>

            <div className="registros-filtro">
                <select value={tipoFiltro} onChange={(event) => setTipoFiltro(event.target.value)}>
                    <option value="">Todos os Tipos</option>
                    <option value="1">Contrato</option>
                    <option value="2">Procuração</option>
                    <option value="3">Notificação</option>
                </select>

                <select value={statusFiltro} onChange={(event) => setStatusFiltro(event.target.value)}>
                    <option value="">Todos os Status</option>
                    <option value="1">Pendente</option>
                    <option value="2">Registrado</option>
                    <option value="3">Devolvido</option>
                </select>

                <button onClick={() => {setPagina(1); carregarRegistros()}}>Filtrar</button>
                <button onClick={() => {setTipoFiltro(""); setStatusFiltro(""); setPagina(1); setLimite(10); carregarRegistros();}}>Limpar</button>
            </div>

            <div className= "registros-card">
                <table className= "registros-table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>CPF/CNPJ</th>
                            <th>Tipo</th>
                            <th>Status</th>
                            <th>Data Entrada</th>
                            <th>Data Criado</th>
                            <th>Última Atualização</th>
                            <th>Ações</th>
                        </tr>
                    </thead>

                    <tbody>
                        {registros.map((registro) =>(
                            <tr key={registro.id}>
                                <td>{registro.nomeApresentante}</td>
                                <td>{formatarCpfCnpj(registro.cpfCnpj)}</td>
                                <td>{alterarTipoRegistros(registro.tipo)}</td>
                                <td>{alterarStatusRegistro(registro.status)}</td>
                                <td>{formatarDataHora(registro.dataEntrada)}</td>
                                <td>{formatarDataHora(registro.dataCriado)}</td>
                                <td>{formatarDataHora(registro.dataAtualizado)}</td>
                                <td>
                                    <div className="acoes">
                                        <button className="acoes-button" onClick={() => setRegistroEdicao(registro)}>Editar</button>
                                        <button className="acoes-button" onClick={() => setRegistroStatus(registro)}>Ação</button>
                                        <button className="acoes-button" onClick={() => removeRegistro(registro.id)}>Excluir</button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <div className="paginacao">
                <button
                    disabled={pagina === 1}
                    onClick={() => setPagina((paginaAtual) => paginaAtual - 1)}
                >
                    Anterior
                </button>

                <span>Página {pagina}</span>

                <button
                    disabled={registros.length < limite}
                    onClick={() => setPagina((paginaAtual) => paginaAtual + 1)}
                >
                    Próxima
                </button>
            </div>
            {modalAberto && (<RegistroModal
                onClose={() => setModalAberto(false)}
                onRegistroCriado={carregarRegistros}/>)}
            
            {registroStatus && (<StatusRegistroModal
                registro={registroStatus}
                onClose={() => setRegistroStatus(null)}
                onStatusAtualizado={carregarRegistros}/>)}

            {registroEdicao && (<EditarRegistroModal
                registro={registroEdicao}
                onClose={() => setRegistroEdicao(null)}
                onRegistroAtualizado={carregarRegistros}/>)}
        </div>
    )
}