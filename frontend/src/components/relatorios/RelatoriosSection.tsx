import type { RelatorioGeral } from "../../types/relatorio";
import { TabelaTotais } from "./TabelaTotais";

interface RelatoriosSectionProps {
  relatorio: RelatorioGeral | null;
  carregando: boolean;
}

export function RelatoriosSection({
  relatorio,
  carregando,
}: RelatoriosSectionProps) {
  return (
    <section className="space-y-4">
      <h2 className="text-xl font-semibold">Relatório de Totais</h2>

      {carregando ? (
        <p className="text-gray-500">Carregando...</p>
      ) : relatorio && relatorio.pessoas.length > 0 ? (
        <TabelaTotais relatorio={relatorio} />
      ) : (
        <p className="text-gray-500">
          Nenhuma pessoa cadastrada para exibir totais.
        </p>
      )}
    </section>
  );
}
