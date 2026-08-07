import type { RelatorioGeral } from "../../types/relatorio";

interface TabelaTotaisProps {
  relatorio: RelatorioGeral;
}

export function TabelaTotais({ relatorio }: TabelaTotaisProps) {
  const formatarMoeda = (valor: number) =>
    valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });

  return (
    <table className="w-full border-collapse">
      <thead>
        <tr className="border-b text-left">
          <th className="p-2">Pessoa</th>
          <th className="p-2 text-right">Receitas</th>
          <th className="p-2 text-right">Despesas</th>
          <th className="p-2 text-right">Saldo</th>
        </tr>
      </thead>
      <tbody>
        {relatorio.pessoas.map((pessoa) => (
          <tr key={pessoa.pessoaId} className="border-b">
            <td className="p-2">{pessoa.nome}</td>
            <td className="p-2 text-right text-green-600">
              {formatarMoeda(pessoa.totalReceitas)}
            </td>
            <td className="p-2 text-right text-red-600">
              {formatarMoeda(pessoa.totalDespesas)}
            </td>
            <td
              className={`p-2 text-right font-medium ${
                pessoa.saldo < 0 ? "text-red-600" : "text-gray-900"
              }`}
            >
              {formatarMoeda(pessoa.saldo)}
            </td>
          </tr>
        ))}
      </tbody>
      <tfoot>
        <tr className="border-t-2 font-semibold">
          <td className="p-2">Total geral</td>
          <td className="p-2 text-right text-green-600">
            {formatarMoeda(relatorio.totalGeralReceitas)}
          </td>
          <td className="p-2 text-right text-red-600">
            {formatarMoeda(relatorio.totalGeralDespesas)}
          </td>
          <td
            className={`p-2 text-right ${
              relatorio.saldoLiquidoGeral < 0 ? "text-red-600" : "text-gray-900"
            }`}
          >
            {formatarMoeda(relatorio.saldoLiquidoGeral)}
          </td>
        </tr>
      </tfoot>
    </table>
  );
}
