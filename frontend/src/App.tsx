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
    erros,
    criarPessoa,
    deletarPessoa,
    criarTransacao,
  } = useDados();

  return (
    <div className="max-w-4xl mx-auto p-6 space-y-10">
      <h1 className="text-2xl font-bold">Controle de Gastos Residenciais</h1>

      {erros.dados && (
        <p className="rounded border border-red-200 bg-red-50 p-2 text-red-600">
          {erros.dados}
        </p>
      )}

      <PessoasSection
        pessoas={pessoas}
        carregando={carregando}
        erro={erros.pessoa}
        aoCriar={criarPessoa}
        aoDeletar={deletarPessoa}
      />

      <TransacoesSection
        transacoes={transacoes}
        pessoas={pessoas}
        carregando={carregando}
        erro={erros.transacao}
        aoCriar={criarTransacao}
      />

      <RelatoriosSection relatorio={relatorio} carregando={carregando} />
    </div>
  );
}

export default App;
