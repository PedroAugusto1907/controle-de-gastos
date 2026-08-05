using ControleDeGastos.DTOs.Pessoa;
using ControleDeGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase {
    private readonly IPessoaService _service;

    public PessoasController(IPessoaService service) {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PessoaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PessoaResponse>>> Listar() {
        var pessoas = await _service.ListarAsync();
        return Ok(pessoas);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PessoaResponse>> ObterPorId(long id) {
        var pessoa = await _service.ObterPorIdAsync(id);
        return Ok(pessoa);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PessoaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PessoaResponse>> Criar(CriarPessoaRequest request) {
        var pessoa = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(long id) {
        await _service.DeletarAsync(id);
        return NoContent();
    }
}