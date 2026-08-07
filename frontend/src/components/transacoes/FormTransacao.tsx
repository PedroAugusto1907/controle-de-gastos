import { useState, type SubmitEvent } from "react";
import type {
  CriarTransacaoRequest,
  TipoTransacao,
} from "../../types/transacao";
import type { Pessoa } from "../../types/pessoa";

interface FormTransacaoProps {
  pessoas: Pessoa[];
  aoCriar: (dados: CriarTransacaoRequest) => Promise<void>;
}

export function FormTransacao({ pessoas, aoCriar }: FormTransacaoProps) {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<TipoTransacao>("Despesa");
  const [pessoaId, setPessoaId] = useState("");
  const [enviando, setEnviando] = useState(false);

  async function handleSubmit(evento: SubmitEvent<HTMLFormElement>) {
    evento.preventDefault();
    setEnviando(true);

    try {
      await aoCriar({
        descricao,
        valor: Number(valor),
        tipo,
        pessoaId: Number(pessoaId),
      });
      setDescricao("");
      setValor("");
      setTipo("Despesa");
      setPessoaId("");
    } finally {
      setEnviando(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-wrap gap-2 items-end">
      <div>
        <label className="block text-sm">Descrição</label>
        <input
          type="text"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
          className="border rounded px-2 py-1"
        />
      </div>

      <div>
        <label className="block text-sm">Valor</label>
        <input
          type="number"
          step="0.01"
          min="0.01"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          required
          className="border rounded px-2 py-1 w-28"
        />
      </div>

      <div>
        <label className="block text-sm">Tipo</label>
        <select
          value={tipo}
          onChange={(e) => setTipo(e.target.value as TipoTransacao)}
          className="border rounded px-2 py-1"
        >
          <option value="Despesa">Despesa</option>
          <option value="Receita">Receita</option>
        </select>
      </div>

      <div>
        <label className="block text-sm">Pessoa</label>
        <select
          value={pessoaId}
          onChange={(e) => setPessoaId(e.target.value)}
          required
          className="border rounded px-2 py-1"
        >
          <option value="" disabled>
            Selecione...
          </option>
          {pessoas.map((pessoa) => (
            <option key={pessoa.id} value={pessoa.id}>
              {pessoa.nome} {pessoa.ehMenorDeIdade ? "(menor de idade)" : ""}
            </option>
          ))}
        </select>
      </div>

      <button
        type="submit"
        disabled={enviando}
        className="bg-blue-600 text-white rounded px-4 py-1 disabled:opacity-50"
      >
        {enviando ? "Salvando..." : "Cadastrar"}
      </button>
    </form>
  );
}
