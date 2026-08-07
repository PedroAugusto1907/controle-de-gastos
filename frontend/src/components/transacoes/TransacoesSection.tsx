import { useState } from "react";
import type { Transacao, CriarTransacaoRequest } from "../../types/transacao";
import type { Pessoa } from "../../types/pessoa";
import { extrairMensagemDeErro } from "../../api/errors";
import { ListaTransacoes } from "./ListaTransacoes";
import { FormTransacao } from "./FormTransacao";

interface TransacoesSectionProps {
  transacoes: Transacao[];
  pessoas: Pessoa[];
  carregando: boolean;
  onCreate: (dados: CriarTransacaoRequest) => Promise<void>;
}

export function TransacoesSection({
  transacoes,
  pessoas,
  carregando,
  onCreate,
}: TransacoesSectionProps) {
  const [erro, setErro] = useState<string | null>(null);

  async function handleCriar(dados: CriarTransacaoRequest) {
    setErro(null);

    try {
      await onCreate(dados);
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    }
  }

  return (
    <section>
      <h2 className="text-xl font-semibold text-gray-900">Transações</h2>

      {pessoas.length === 0 && !carregando ? (
        <p className="mt-2 rounded border border-amber-200 bg-amber-50 p-2 text-amber-600">
          Cadastre ao menos uma pessoa antes de lançar transações.
        </p>
      ) : (
        <FormTransacao pessoas={pessoas} aoCriar={handleCriar} />
      )}

      {erro && (
        <p className="mt-2 rounded border border-red-200 bg-red-50 p-2 text-sm text-red-600">
          {erro}
        </p>
      )}

      {carregando ? (
        <p className="mt-4 text-gray-500">Carregando...</p>
      ) : (
        <ListaTransacoes transacoes={transacoes} />
      )}
    </section>
  );
}
