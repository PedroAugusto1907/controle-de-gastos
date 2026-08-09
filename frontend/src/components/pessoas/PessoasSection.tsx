import type { Pessoa, CriarPessoaRequest } from "../../types/pessoa";
import { ListaPessoas } from "./ListaPessoas";
import { FormPessoa } from "./FormPessoa";

interface PessoasSectionProps {
  pessoas: Pessoa[];
  carregando: boolean;
  erro: string | null;
  aoCriar: (dados: CriarPessoaRequest) => Promise<void>;
  aoDeletar: (id: number) => Promise<void>;
}

export function PessoasSection({
  pessoas,
  carregando,
  erro,
  aoCriar,
  aoDeletar,
}: PessoasSectionProps) {
  async function handleDeletar(id: number) {
    // Avisa sobre o cascade delete (backend remove as transações da pessoa)
    const confirmar = window.confirm(
      "Remover esta pessoa também apagará todas as suas transações. Confirma?",
    );
    if (!confirmar) return;

    await aoDeletar(id);
  }

  return (
    <section>
      <h2 className="text-xl font-semibold text-gray-900">Pessoas</h2>

      <FormPessoa aoCriar={aoCriar} />

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
