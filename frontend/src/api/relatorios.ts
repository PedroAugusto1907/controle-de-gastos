import { apiClient } from "./client";
import type { RelatorioGeral } from "../types/relatorio";

/**
 * Busca os totais consolidados: receitas, despesas e saldo por
 * pessoa, além do total geral agregando todas as pessoas.
 */
export async function obterTotais(): Promise<RelatorioGeral> {
  const response = await apiClient.get<RelatorioGeral>("/relatorios/totais");
  return response.data;
}
