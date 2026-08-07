import { useEffect, useState } from "react";
import { listarPessoas, criarPessoa, deletarPessoa } from "../../api/pessoas";
import { extrairMensagemDeErro } from "../../api/errors";
import type { Pessoa, CriarPessoaRequest } from "../../types/pessoa";
import { ListaPessoas } from "./ListaPessoas";
import { FormPessoa } from "./FormPessoa";

export function PessoasSection() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState<string | null>(null);

  useEffect(() => {
    carregarPessoas();
  }, []);

  async function carregarPessoas() {
    setCarregando(true);
    setErro(null);

    try {
      const dados = await listarPessoas();
      setPessoas(dados);
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    } finally {
      setCarregando(false);
    }
  }

  async function handleCriar(dados: CriarPessoaRequest) {
    setErro(null);

    try {
      await criarPessoa(dados);
      await carregarPessoas();
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    }
  }

  async function handleDeletar(id: number) {
    const confirmar = window.confirm(
      "Remover esta pessoa também apagará todas as suas transações. Confirma?",
    );
    if (!confirmar) return;

    setErro(null);

    try {
      await deletarPessoa(id);
      await carregarPessoas();
    } catch (erro) {
      setErro(extrairMensagemDeErro(erro));
    }
  }

  return (
    <section className="space-y-4">
      <h2 className="text-xl font-semibold">Pessoas</h2>

      <FormPessoa aoCriar={handleCriar} />

      {erro && (
        <p className="text-red-600 bg-red-50 border border-red-200 rounded p-2">
          {erro}
        </p>
      )}

      {carregando ? (
        <p className="text-gray-500">Carregando...</p>
      ) : (
        <ListaPessoas pessoas={pessoas} aoDeletar={handleDeletar} />
      )}
    </section>
  );
}
