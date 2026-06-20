import {useState} from "react";
import {atualizarStatus} from "../../services/registroService";
import type {Registro} from "../../types/registro";
import "./StatusRegistroModal.css";

interface StatusRegistroModalProp {
    registro: Registro;
    onClose: () => void;
    onStatusAtualizado: () => void;
}

export function StatusRegistroModal({registro, onClose, onStatusAtualizado,}: StatusRegistroModalProp) {
    const [erro, setErro] = useState("");
    const [salvando, setSalvando] = useState(false);

    async function alterarStatus(status: number) {
        try {
            setErro("");
            setSalvando(true);

            await atualizarStatus(registro.id, status);

            onStatusAtualizado();
            onClose();
        } catch {
            setErro("Não foi possível alterar o status do registro.");
        } finally {
            setSalvando(false);
        }
    }
    return (
        <div className="status-modal-overlay">
            <div className="status-modal">
                <header className="status-modal-header">
                    <h2>Alterar Status
                    </h2>
                    <button type="button" onClick={onClose}>×</button>
                </header>
                <p>Registro de <strong>{registro.nomeApresentante}</strong>.</p>
                {erro && <p className="status-modal-error">{erro}</p>}

                <div className="status-modal-acoes">
                    {registro.status === 1 &&(<><button type="button" disabled={salvando} onClick={() => alterarStatus(2)}>Registrar</button> <button type="button" disabled={salvando} onClick={() => alterarStatus(3)}>Devolver</button></>)}
                    {registro.status === 3 &&(<><button type="button" disabled={salvando} onClick={() => alterarStatus(1)}>Retornar Pendente</button> </>)}
                    {registro.status === 2 &&(<p>Registro finalizado.</p>)}
                </div>
            </div>
        </div>
    )
}