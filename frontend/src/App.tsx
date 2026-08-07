import { useEffect } from "react";
import { apiClient } from "./api/client";

function App() {
  useEffect(() => {
    apiClient
      .get("/pessoas")
      .then((response) => console.log("Conexão OK:", response.data))
      .catch((error) => console.error("Erro de conexão:", error));
  }, []);

  return (
    <div>
      <h1>Controle de Gastos Residenciais</h1>
    </div>
  );
}

export default App;
