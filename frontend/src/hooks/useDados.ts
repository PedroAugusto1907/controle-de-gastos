import { useEffect, useState } from "react";
import {
  listarPessoas,
  criarPessoa as criarPessoaApi,
  deletarPessoa as deletarPessoaApi,
} from "../api/pessoas";
import {
  listarTransacoes,
  criarTransacao as criarTransacaoApi,
} from "../api/transacoes";
import { obterTotais } from "../api/relatorios";
import type { Pessoa, CriarPessoaRequest } from "../types/pessoa";
import type { Transacao, CriarTransacaoRequest } from "../types/transacao";
import type { RelatorioGeral } from "../types/relatorio";

export function useDados() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [relatorio, setRelatorio] = useState<RelatorioGeral | null>(null);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    carregarDados();
  }, []);

  async function carregarDados() {
    setCarregando(true);

    try {
      const [dadosPessoas, dadosTransacoes, dadosRelatorio] = await Promise.all(
        [listarPessoas(), listarTransacoes(), obterTotais()],
      );

      setPessoas(dadosPessoas);
      setTransacoes(dadosTransacoes);
      setRelatorio(dadosRelatorio);
    } finally {
      setCarregando(false);
    }
  }

  async function criarPessoa(dados: CriarPessoaRequest) {
    await criarPessoaApi(dados);
    await carregarDados();
  }

  async function deletarPessoa(id: number) {
    await deletarPessoaApi(id);
    await carregarDados();
  }

  async function criarTransacao(dados: CriarTransacaoRequest) {
    await criarTransacaoApi(dados);
    await carregarDados();
  }

  return {
    pessoas,
    transacoes,
    relatorio,
    carregando,
    criarPessoa,
    deletarPessoa,
    criarTransacao,
  };
}
