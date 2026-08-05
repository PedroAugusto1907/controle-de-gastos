using ControleDeGastos.DTOs.Relatorio;
using ControleDeGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase {
    private readonly IRelatorioService _service;

    public RelatoriosController(IRelatorioService service) {
        _service = service;
    }

    [HttpGet("totais")]
    [ProducesResponseType(typeof(RelatorioGeralResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<RelatorioGeralResponse>> ObterTotais() {
        var relatorio = await _service.ObterTotaisAsync();
        return Ok(relatorio);
    }
}