import { useState } from "react";
import { atualizarRegistro } from "../../services/registroService";
import type { Registro } from "../../types/registro";
import { useToast } from "../../contexts/ToastContext";
import { apenasNumeros } from "../../utils/cpfCnpj";
import { formatarCpfCnpj } from "../../utils/cpfCnpj";
import "./EditarRegistroModal.css";

interface EditarRegistroModalProps {
  registro: Registro;
  onClose: () => void;
  onRegistroAtualizado: () => void;
}

export function EditarRegistroModal({registro, onClose, onRegistroAtualizado,}: EditarRegistroModalProps) {
  const [tipo, setTipo] = useState(registro.tipo);
  const [nomeApresentante, setNomeApresentante] = useState(registro.nomeApresentante);
  const [cpfCnpj, setCpfCnpj] = useState(formatarCpfCnpj(registro.cpfCnpj));
  const [dataEntrada, setDataEntrada] = useState(registro.dataEntrada);
  const [observacoes, setObservacoes] = useState(registro.observacoes ?? "");
  const [salvando, setSalvando] = useState(false);
  const {showToast} = useToast();
  const [errosCampos, setErrosCampos] = useState({nomeApresentante: "", cpfCnpj: "", dataEntrada: "",});

  async function handleSubmit(event: React.FormEvent) {
    event.preventDefault();
    if (!validarFormulario()) {
                return;
            }
    try {
      setSalvando(true);

      await atualizarRegistro(registro.id, {
        tipo,
        nomeApresentante,
        cpfCnpj: apenasNumeros(cpfCnpj),
        dataEntrada,
        observacoes,
      });

      onRegistroAtualizado();
      showToast("Registro atualizado com sucesso!", "success");
      onClose();
    } catch {
      showToast("Erro ao atualizar registro.", "error");
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
    <div className="editar-modal-overlay">
      <div className="editar-modal">
        <header className="editar-modal-header">
          <h2>Editar Registro</h2>
          <button type="button" onClick={onClose}>×</button>
        </header>

        <form onSubmit={handleSubmit} className="editar-modal-form">
          <label>
            Tipo
            <select value={tipo} onChange={(e) => setTipo(Number(e.target.value))}>
              <option value={1}>Contrato</option>
              <option value={2}>Procuração</option>
              <option value={3}>Notificação</option>
            </select>
          </label>

          <label>
            Apresentante
              <input type="text" value={nomeApresentante} onChange={(event) => setNomeApresentante(event.target.value)}></input>
              {errosCampos.nomeApresentante && (<span className="campo-erro">{errosCampos.nomeApresentante}</span>)}
          </label>

          <label>
            CPF/CNPJ
              <input type="text" value={cpfCnpj} onChange={(event) => setCpfCnpj(formatarCpfCnpj(event.target.value))}></input>
              {errosCampos.cpfCnpj && (<span className="campo-erro">{errosCampos.cpfCnpj}</span>)}
          </label>

          <label>
            Data de Entrada
              <input type="date" value={dataEntrada} onChange={(event) => setDataEntrada(event.target.value)}></input>
              {errosCampos.dataEntrada && (<span className="campo-erro">{errosCampos.dataEntrada}</span>)}
          </label>

          <label>
            Observações
            <textarea value={observacoes} onChange={(e) => setObservacoes(e.target.value)} />
          </label>

          <div className="editar-modal-acoes">
            <button type="button" onClick={onClose}>Cancelar</button>
            <button type="submit" disabled={salvando}>
              {salvando ? "Salvando..." : "Salvar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}