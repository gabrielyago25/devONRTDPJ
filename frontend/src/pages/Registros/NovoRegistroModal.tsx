import {useState} from "react";
import {criarRegistro} from "../../services/registroService";
import { useToast } from "../../contexts/ToastContext";
import { formatarCpfCnpj } from "../../utils/cpfCnpj";
import { apenasNumeros } from "../../utils/cpfCnpj";
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
        const {showToast} = useToast();
        const [errosCampos, setErrosCampos] = useState({nomeApresentante: "", cpfCnpj: "", dataEntrada: "",});

        async function handleCriarRegistro(event: React.FormEvent) {
            event.preventDefault();
            if (!validarFormulario()) {
                return;
            }

            try {
                setErro("");
                setSalvando(true);

                await criarRegistro({tipo, nomeApresentante, cpfCnpj: apenasNumeros(cpfCnpj), dataEntrada, observacoes});
                showToast("Registro criado com sucesso!", "success");
                onRegistroCriado();
                onClose();
            } catch {
                showToast("Erro ao criar registro.", "error");
            } finally {
                setSalvando(false);
            }
        }

        function validarFormulario() {
            const formErros = {nomeApresentante: "", cpfCnpj: "", dataEntrada: ""};

            if (!nomeApresentante.trim()) {
                formErros.nomeApresentante = "Informe o nome do apresentante."
            }

            const cpfCnpjNumeros = apenasNumeros(cpfCnpj);

            if (!cpfCnpjNumeros) {
                formErros.cpfCnpj = "Informe o CPF/CNPJ.";
            } else if (cpfCnpjNumeros.length !== 11 && cpfCnpjNumeros.length !== 14){
                formErros.cpfCnpj = "CPF/CNPJ deve ter 11 ou 14 dígitos."
            }
            if (!dataEntrada) {
                formErros.dataEntrada = "Informe a data de entrada.";
            }

            setErrosCampos(formErros);

            return !Object.values(formErros).some((erro => erro))
        }

        return (
            <div className= "modal-overlay">
                <div className= "modal" role="dialog" aria-modal="true" aria-labelledby="novo-registro-titulo">
                    <header className = "modal-header">
                        <h2 id="novo-registro-titulo">Inclusão de Novo Registro</h2>
                        <button type="button" onClick={onClose} aria-label="Fechar">×</button>
                    </header>

                    <form onSubmit={handleCriarRegistro} className="modal-form">
                        <label>Tipo
                            <select value={tipo} onChange={(event) => setTipo(Number(event.target.value))}>
                                <option value={1}>Contrato</option>
                                <option value={2}>Procuração</option>
                                <option value={3}>Notificação</option>
                            </select>   
                        </label>

                        <label> Apresentante *
                            <input type="text" value={nomeApresentante} onChange={(event) => setNomeApresentante(event.target.value)}></input>
                            {errosCampos.nomeApresentante && (<span className="campo-erro">{errosCampos.nomeApresentante}</span>)}
                        </label>

                        <label> CPF/CNPJ *
                            <input type="text" value={cpfCnpj} onChange={(event) => setCpfCnpj(formatarCpfCnpj(event.target.value))}></input>
                            {errosCampos.cpfCnpj && (<span className="campo-erro">{errosCampos.cpfCnpj}</span>)}

                        </label>

                        <label>Data de Apresentação *
                            <input type="date" value={dataEntrada} onChange={(event) => setDataEntrada(event.target.value)}></input>
                            {errosCampos.dataEntrada && (<span className="campo-erro">{errosCampos.dataEntrada}</span>)}
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
