using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como 'api/reclamacao'.
    public class ReclamacaoController : ControllerBase
    {
        private readonly ReclamacaoService _reclamacaoService;

        // Injeção do contexto do banco de dados para inicializar o serviço de reclamações.
        public ReclamacaoController(MyDbContext dbContext)
        {
            _reclamacaoService = new ReclamacaoService(new ReclamacaoRepository(dbContext));
        }

        // Endpoint para listar todas as reclamações.
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var reclamacoes = _reclamacaoService.GetAllReclamacoes();
                if (reclamacoes == null || !reclamacoes.Any())
                    return NotFound("Nenhuma reclamação cadastrada."); // Retorna 404 se não houver reclamações.
                return Ok(reclamacoes); // Retorna 200 com a lista de reclamações.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar uma reclamação específica pelo ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var reclamacao = _reclamacaoService.GetReclamacaoById(id);
                if (reclamacao == null)
                    return NotFound("Reclamação não encontrada."); // Retorna 404 se a reclamação não existir.
                return Ok(reclamacao); // Retorna 200 com a reclamação encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar uma nova reclamação.
        [HttpPost]
        public IActionResult Create([FromBody] ReclamacaoDto reclamacaoDto)
        {
            if (reclamacaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _reclamacaoService.CreateReclamacao(reclamacaoDto);
                return CreatedAtAction(nameof(GetAll), null); // Retorna 201 indicando a criação bem-sucedida.
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}"); // Retorna 400 em caso de erro de validação.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualizar uma reclamação existente.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReclamacaoDto reclamacaoDto)
        {
            if (reclamacaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _reclamacaoService.UpdateReclamacao(id, reclamacaoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Reclamação não encontrada."); // Retorna 404 se a reclamação não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualização parcial de uma reclamação.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] ReclamacaoDto reclamacaoDto)
        {
            if (reclamacaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _reclamacaoService.PartialUpdateReclamacao(id, reclamacaoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Reclamação não encontrada."); // Retorna 404 se a reclamação não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar uma reclamação por ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _reclamacaoService.DeleteReclamacao(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Reclamação não encontrada."); // Retorna 404 se a reclamação não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
