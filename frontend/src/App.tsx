import { PessoasSection } from "./components/pessoas/PessoasSection";
import { RelatoriosSection } from "./components/relatorios/RelatoriosSection";
import { TransacoesSection } from "./components/transacoes/TransacoesSection";
import { useDados } from "./hooks/useDados";

function App() {
  const {
    pessoas,
    transacoes,
    relatorio,
    carregando,
    criarPessoa,
    deletarPessoa,
    criarTransacao,
  } = useDados();

  return (
    <div className="max-w-4xl mx-auto p-6 space-y-10">
      <h1 className="text-2xl font-bold">Controle de Gastos Residenciais</h1>

      <PessoasSection
        pessoas={pessoas}
        carregando={carregando}
        onCreate={criarPessoa}
        onDelete={deletarPessoa}
      />

      <TransacoesSection
        transacoes={transacoes}
        pessoas={pessoas}
        carregando={carregando}
        onCreate={criarTransacao}
      />

      <RelatoriosSection relatorio={relatorio} carregando={carregando} />
    </div>
  );
}

export default App;
