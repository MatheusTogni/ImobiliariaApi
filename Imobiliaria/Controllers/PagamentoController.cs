using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como 'api/pagamento'.
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _pagamentoService;

        // Injeção do contexto do banco de dados para inicializar o serviço de pagamentos.
        public PagamentoController(MyDbContext dbContext)
        {
            _pagamentoService = new PagamentoService(new PagamentoRepository(dbContext));
        }

        // Endpoint para listar todos os pagamentos.
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var pagamentos = _pagamentoService.GetAllPagamentos();
                if (pagamentos == null || !pagamentos.Any())
                    return NotFound("Nenhum pagamento cadastrado."); // Retorna 404 se não houver pagamentos.
                return Ok(pagamentos); // Retorna 200 com a lista de pagamentos.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar um pagamento específico pelo ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var pagamento = _pagamentoService.GetPagamentoById(id);
                if (pagamento == null)
                    return NotFound("Pagamento não encontrado."); // Retorna 404 se o pagamento não existir.
                return Ok(pagamento); // Retorna 200 com o pagamento encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar um novo pagamento.
        [HttpPost]
        public IActionResult Create([FromBody] PagamentoDto pagamentoDto)
        {
            if (pagamentoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _pagamentoService.CreatePagamento(pagamentoDto);
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

        // Endpoint para atualizar um pagamento existente.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PagamentoDto pagamentoDto)
        {
            if (pagamentoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _pagamentoService.UpdatePagamento(id, pagamentoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pagamento não encontrado."); // Retorna 404 se o pagamento não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualização parcial de um pagamento.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] PagamentoDto pagamentoDto)
        {
            if (pagamentoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _pagamentoService.PartialUpdatePagamento(id, pagamentoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pagamento não encontrado."); // Retorna 404 se o pagamento não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar um pagamento por ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _pagamentoService.DeletePagamento(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pagamento não encontrado."); // Retorna 404 se o pagamento não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
