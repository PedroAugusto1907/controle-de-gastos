import { apiClient } from "./client";
import type { Pessoa, CriarPessoaRequest } from "../types/pessoa";

/** Busca todas as pessoas cadastradas */
export async function listarPessoas(): Promise<Pessoa[]> {
  const response = await apiClient.get<Pessoa[]>("/pessoas");
  return response.data;
}

/** Cadastra uma nova pessoa */
export async function criarPessoa(dados: CriarPessoaRequest): Promise<Pessoa> {
  const response = await apiClient.post<Pessoa>("/pessoas", dados);
  return response.data;
}

/** Remove uma pessoa */
export async function deletarPessoa(id: number): Promise<void> {
  await apiClient.delete(`/pessoas/${id}`);
}
