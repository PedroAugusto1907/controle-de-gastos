import { useState } from "react";
import type { Pessoa, CriarPessoaRequest } from "../../types/pessoa";
import { extrairMensagemDeErro } from "../../api/errors";
import { ListaPessoas } from "./ListaPessoas";
import { FormPessoa } from "./FormPessoa";

interface PessoasSectionProps {
  pessoas: Pessoa[];
  carregando: boolean;
  onCreate: (dados: CriarPessoaRequest) => Promise<void>;
  onDelete: (id: number) => Promise<void>;
}

export function PessoasSection({
  pessoas,
  carregando,
  onCreate,
  onDelete,
}: PessoasSectionProps) {
  const [erro, setErro] = useState<string | null>(null);

  async function handleCriar(dados: CriarPessoaRequest) {
    setErro(null);

    try {
      await onCreate(dados);
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    }
  }

  async function handleDeletar(id: number) {
    setErro(null);

    const confirmar = window.confirm(
      "Remover esta pessoa também apagará todas as suas transações. Confirma?",
    );
    if (!confirmar) return;

    try {
      await onDelete(id);
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    }
  }

  return (
    <section>
      <h2 className="text-xl font-semibold text-gray-900">Pessoas</h2>

      <FormPessoa aoCriar={handleCriar} />

      {erro && (
        <p className="mt-2 rounded border border-red-200 bg-red-50 p-2 text-sm text-red-600">
          {erro}
        </p>
      )}

      {carregando ? (
        <p className="mt-4 text-gray-500">Carregando...</p>
      ) : (
        <ListaPessoas pessoas={pessoas} aoDeletar={handleDeletar} />
      )}
    </section>
  );
}
