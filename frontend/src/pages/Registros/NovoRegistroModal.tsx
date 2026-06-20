import {useState} from "react";
import {criarRegistro} from "../../services/registroService";
import "./NovoRegistroModal.css";

interface RegistroModalProp {
    onClose: () => void;
    onRegistroCriado: () => void;
}

export function RegistroModal ({onClose, onRegistroCriado,}: RegistroModalProp) {
        const [tipo, setTipo] = useState(1);
        const [nomeApresentante, setNomeApresentante] = useState("");
        const [cpfCnpj, setCpfCnpj] = useState("");
        const [dataEntrada, setDataEntrada] = useState("");
        const [observacoes, setObservacoes] = useState("");
        const [erro, setErro] = useState("");
        const [salvando, setSalvando] = useState(false);

        async function handleCriarRegistro(event: React.FormEvent) {
            event.preventDefault();

            try {
                setErro("");
                setSalvando(true);

                await criarRegistro({tipo, nomeApresentante, cpfCnpj, dataEntrada, observacoes});

                onRegistroCriado();
                onClose();
            } catch {
                setErro("Erro ao criar o registro, verifique os dados informados.")
            } finally {
                setSalvando(false);
            }
        }

        return (
            <div className= "modal-overlay">
                <div className= "modal">
                    <header className = "modal-header">
                        <h2>Inclusão de Novo Registro</h2>
                        <button type="button" onClick={onClose}>×</button>
                    </header>

                    <form onSubmit={handleCriarRegistro} className="modal-form">
                        <label>Tipo
                            <select value={tipo} onChange={(event) => setTipo(Number(event.target.value))}>
                                <option value={1}>Contrato</option>
                                <option value={2}>Procuração</option>
                                <option value={3}>Notificação</option>
                            </select>
                        </label>

                        <label> Apresentante
                            <input type="text" value={nomeApresentante} onChange={(event) => setNomeApresentante(event.target.value)} required></input>
                        </label>

                        <label> CPF/CNPJ
                            <input type="text" value={cpfCnpj} onChange={(event) => setCpfCnpj(event.target.value)} required></input>
                        </label>

                        <label>Data de Apresentação
                            <input type="date" value={dataEntrada} onChange={(event) => setDataEntrada(event.target.value)} required></input>
                        </label>

                        <label>Observações
                            <textarea value={observacoes} onChange={(event) => setObservacoes(event.target.value)}></textarea>
                        </label>

                        {erro && <p className="modal-erro">{erro}</p>}

                        <div className= "modal-acoes">
                            <button type= "button" onClick={onClose}>Cancelar</button>
                            <button type= "submit" disabled={salvando}>{salvando? "Salvando..." : "Salvar"}</button>
                        </div>
                    </form>
                </div>
            </div>
        )
    }