using ControleDeGastos.DTOs.Transacao;
using ControleDeGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase {
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service) {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransacaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TransacaoResponse>>> Listar() {
        var transacoes = await _service.ListarAsync();
        return Ok(transacoes);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransacaoResponse>> Criar(CriarTransacaoRequest request) {
        var transacao = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(Listar), new { id = transacao.Id }, transacao);
    }
}