import { useState } from "react";
import type { CriarPessoaRequest } from "../../types/pessoa";

interface FormPessoaProps {
  aoCriar: (dados: CriarPessoaRequest) => Promise<void>;
}

export function FormPessoa({ aoCriar }: FormPessoaProps) {
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [enviando, setEnviando] = useState(false);

  async function handleSubmit(evento: React.SubmitEvent<HTMLFormElement>) {
    evento.preventDefault();
    setEnviando(true);

    try {
      await aoCriar({ nome, idade: Number(idade) });
      setNome("");
      setIdade("");
    } finally {
      setEnviando(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex gap-2 items-end">
      <div>
        <label className="block text-sm">Nome</label>
        <input
          type="text"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
          className="border rounded px-2 py-1"
        />
      </div>

      <div>
        <label className="block text-sm">Idade</label>
        <input
          type="number"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          required
          min={0}
          max={150}
          className="border rounded px-2 py-1 w-20"
        />
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
