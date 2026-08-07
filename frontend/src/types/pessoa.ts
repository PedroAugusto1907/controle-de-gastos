export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
  ehMenorDeIdade: boolean;
}

export interface CriarPessoaRequest {
  nome: string;
  idade: number;
}
