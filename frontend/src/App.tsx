import { PessoasSection } from "./components/pessoas/PessoasSection";

function App() {
  return (
    <div className="max-w-4xl mx-auto p-6">
      <h1 className="text-2xl font-bold mb-6">
        Controle de Gastos Residenciais
      </h1>
      <PessoasSection />
    </div>
  );
}

export default App;
