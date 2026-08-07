import type { Pessoa } from "../../types/pessoa";

interface ListaPessoasProps {
  pessoas: Pessoa[];
  aoDeletar: (id: number) => void;
}

export function ListaPessoas({ pessoas, aoDeletar }: ListaPessoasProps) {
  if (pessoas.length === 0) {
    return <p className="text-gray-500">Nenhuma pessoa cadastrada.</p>;
  }

  return (
    <table className="w-full border-collapse">
      <thead>
        <tr className="border-b text-left">
          <th className="p-2">Nome</th>
          <th className="p-2">Idade</th>
          <th className="p-2"></th>
        </tr>
      </thead>
      <tbody>
        {pessoas.map((pessoa) => (
          <tr key={pessoa.id} className="border-b">
            <td className="p-2">
              {pessoa.nome}
              {pessoa.ehMenorDeIdade && (
                <span className="ml-2 text-xs text-amber-600">
                  (menor de idade)
                </span>
              )}
            </td>
            <td className="p-2">{pessoa.idade}</td>
            <td className="p-2 text-right">
              <button
                onClick={() => aoDeletar(pessoa.id)}
                className="text-red-600 hover:underline"
              >
                Remover
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
