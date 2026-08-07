import type { Transacao } from "../../types/transacao";

interface ListaTransacoesProps {
  transacoes: Transacao[];
}

export function ListaTransacoes({ transacoes }: ListaTransacoesProps) {
  if (transacoes.length === 0) {
    return <p className="text-gray-500">Nenhuma transação cadastrada.</p>;
  }

  return (
    <table className="w-full border-collapse">
      <thead>
        <tr className="border-b text-left">
          <th className="p-2">Descrição</th>
          <th className="p-2">Pessoa</th>
          <th className="p-2">Tipo</th>
          <th className="p-2 text-right">Valor</th>
        </tr>
      </thead>
      <tbody>
        {transacoes.map((transacao) => (
          <tr key={transacao.id} className="border-b">
            <td className="p-2">{transacao.descricao}</td>
            <td className="p-2">{transacao.pessoaNome}</td>
            <td className="p-2">
              <span
                className={
                  transacao.tipo === "Receita"
                    ? "text-green-600"
                    : "text-red-600"
                }
              >
                {transacao.tipo}
              </span>
            </td>
            <td className="p-2 text-right">
              {transacao.valor.toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL",
              })}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
