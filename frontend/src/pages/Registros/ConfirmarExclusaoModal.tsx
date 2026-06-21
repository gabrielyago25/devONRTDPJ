import type { Registro } from "../../types/registro";
import "./ConfirmarExclusaoModal.css";


interface ConfirmarExclusaoModalProps {
  registro: Registro;
  onClose: () => void;
  onConfirmar: () => void;
}

export function ConfirmarExclusaoModal({registro, onClose, onConfirmar,}: ConfirmarExclusaoModalProps) {
  return (
    <div className="exclusao-modal-overlay">
      <div className="exclusao-modal">
        <h2>Excluir registro</h2>

        <p>Tem certeza que deseja excluir o registro?</p>

        <p className="exclusao-alerta">Essa ação não poderá ser desfeita.</p>

        <div className="exclusao-modal-acoes">
          <button type="button" onClick={onClose}>Cancelar</button>

          <button type="button" className="danger" onClick={onConfirmar}>Excluir</button>
        </div>
      </div>
    </div>
  );
}