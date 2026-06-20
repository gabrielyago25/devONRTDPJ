import { useEffect, useState } from "react";
import { listarRegistros } from "../../services/registroService";
import type { Registro } from "../../types/registro";
import { alterarStatusRegistro, alterarTipoRegistros } from "../../utils/registrosEnums";
import { formatarDataHora } from "../../utils/dataFormat";
import { formatarCpfCnpj } from "../../utils/cpfCnpj";
import "./Registros.css";


export function RegistrosPagina() {
    const [registros, setRegistros] = useState<Registro[]>([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState("");

    useEffect(() => {
        async function carregarRegistro() {
            try {
                const dados = await listarRegistros();
                setRegistros(dados);
            } catch {
                setErro("Erro ao carregar registros.");
            } finally {
                setCarregando(false);
            }
        }
        carregarRegistro();

    }, [])

    if (carregando) {
        return <p>Carregando registros...</p>;
    }

    if (erro) {
        return <p>{erro}</p>;
    }

    return (
        <div className= "registros-pagina">
            <div className= "registros-header">
            <h1>Registros</h1>
            <button className="novo-registro-button">+ Registro</button>
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
                        </tr>
                    ))}
                </tbody>
            </table>
            </div>
        </div>
    )
}