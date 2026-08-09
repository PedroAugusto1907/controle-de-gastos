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
import { extrairMensagemDeErro } from "../api/errors";

type Erros = {
  pessoa: string | null;
  transacao: string | null;
  dados: string | null;
};

export function useDados() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [relatorio, setRelatorio] = useState<RelatorioGeral | null>(null);
  const [carregando, setCarregando] = useState(true);
  const [erros, setErros] = useState<Erros>({
    pessoa: null,
    transacao: null,
    dados: null,
  });

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
      setErros({
        pessoa: null,
        transacao: null,
        dados: null,
      });
    } catch (erro) {
      setErros((prev) => ({ ...prev, dados: extrairMensagemDeErro(erro) }));
    } finally {
      setCarregando(false);
    }
  }

  async function criarPessoa(dados: CriarPessoaRequest) {
    try {
      await criarPessoaApi(dados);
      await carregarDados();
    } catch (erro) {
      setErros((prev) => ({
        ...prev,
        pessoa: extrairMensagemDeErro(erro),
      }));
    }
  }

  async function deletarPessoa(id: number) {
    try {
      await deletarPessoaApi(id);
      await carregarDados();
    } catch (erro) {
      setErros((prev) => ({
        ...prev,
        pessoa: extrairMensagemDeErro(erro),
      }));
    }
  }

  async function criarTransacao(dados: CriarTransacaoRequest) {
    try {
      await criarTransacaoApi(dados);
      await carregarDados();
    } catch (erro) {
      setErros((prev) => ({
        ...prev,
        transacao: extrairMensagemDeErro(erro),
      }));
    }
  }

  return {
    pessoas,
    transacoes,
    relatorio,
    carregando,
    erros,
    criarPessoa,
    deletarPessoa,
    criarTransacao,
  };
}
