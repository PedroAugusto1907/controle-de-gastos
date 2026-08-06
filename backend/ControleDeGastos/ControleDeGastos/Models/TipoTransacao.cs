using System.Text.Json.Serialization;

namespace ControleDeGastos.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoTransacao {
    Despesa = 0,
    Receita = 1
}