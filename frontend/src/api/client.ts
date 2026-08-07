import axios from "axios";

/**
 * Instância única do axios usada por toda a aplicação, com a URL
 * base da API configurada via variável de ambiente (.env).
 * Centralizar aqui evita repetir a baseURL em cada arquivo de
 * chamada (pessoas.ts, transacoes.ts, relatorios.ts) e facilita
 * adicionar comportamento global (ex: interceptor de erro) em
 * um único lugar no futuro.
 */
export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});
