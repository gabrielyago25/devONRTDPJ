import { useState } from "react";
import { atualizarRegistro } from "../../services/registroService";
import type { Registro } from "../../types/registro";
import "./EditarRegistroModal.css";

interface EditarRegistroModalProps {
  registro: Registro;
  onClose: () => void;
  onRegistroAtualizado: () => void;
}

export function EditarRegistroModal({registro, onClose, onRegistroAtualizado,}: EditarRegistroModalProps) {
  const [tipo, setTipo] = useState(registro.tipo);
  const [nomeApresentante, setNomeApresentante] = useState(registro.nomeApresentante);
  const [cpfCnpj, setCpfCnpj] = useState(registro.cpfCnpj);
  const [dataEntrada, setDataEntrada] = useState(registro.dataEntrada);
  const [observacoes, setObservacoes] = useState(registro.observacoes ?? "");
  const [erro, setErro] = useState("");
  const [salvando, setSalvando] = useState(false);

  async function handleSubmit(event: React.FormEvent) {event.preventDefault();

    try {
      setErro("");
      setSalvando(true);

      await atualizarRegistro(registro.id, {
        tipo,
        nomeApresentante,
        cpfCnpj,
        dataEntrada,
        observacoes,
      });

      onRegistroAtualizado();
      onClose();
    } catch {
      setErro("Erro ao atualizar registro.");
    } finally {
      setSalvando(false);
    }
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
            <input value={nomeApresentante} onChange={(e) => setNomeApresentante(e.target.value)} required />
          </label>

          <label>
            CPF/CNPJ
            <input value={cpfCnpj} onChange={(e) => setCpfCnpj(e.target.value)} required />
          </label>

          <label>
            Data de Entrada
            <input type="date" value={dataEntrada} onChange={(e) => setDataEntrada(e.target.value)} required />
          </label>

          <label>
            Observações
            <textarea value={observacoes} onChange={(e) => setObservacoes(e.target.value)} />
          </label>

          {erro && <p className="editar-modal-error">{erro}</p>}

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