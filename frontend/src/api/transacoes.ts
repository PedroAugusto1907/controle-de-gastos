import { apiClient } from "./client";
import type { Transacao, CriarTransacaoRequest } from "../types/transacao";

/** Busca todas as transações cadastradas */
export async function listarTransacoes(): Promise<Transacao[]> {
  const response = await apiClient.get<Transacao[]>("/transacoes");
  return response.data;
}

/** Cadastra uma nova transação */
export async function criarTransacao(
  dados: CriarTransacaoRequest,
): Promise<Transacao> {
  const response = await apiClient.post<Transacao>("/transacoes", dados);
  return response.data;
}
