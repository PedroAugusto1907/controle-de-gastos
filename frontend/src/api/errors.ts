import { AxiosError } from "axios";

export function extrairMensagemDeErro(erro: unknown): string {
  if (erro instanceof AxiosError) {
    const dados = erro.response?.data;

    if (dados?.detail) {
      return dados.detail as string;
    }

    if (dados?.errors) {
      const mensagens = Object.values(dados.errors).flat();
      return mensagens.join(" ");
    }

    if (erro.code === "ERR_NETWORK") {
      return "Não foi possível conectar à API";
    }
  }

  return "Ocorreu um erro inesperado. Tente novamente.";
}
