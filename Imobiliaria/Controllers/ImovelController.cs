using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como 'api/imovel'.
    public class ImovelController : ControllerBase
    {
        private readonly ImovelService _imovelService;

        // Injeção do contexto do banco de dados para inicializar o serviço de imóveis.
        public ImovelController(MyDbContext dbContext)
        {
            _imovelService = new ImovelService(new ImovelRepository(dbContext));
        }

        // Endpoint para listar todos os imóveis.
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var imoveis = _imovelService.GetAllImoveis();
                if (imoveis == null || !imoveis.Any())
                    return NotFound("Nenhum imóvel cadastrado."); // Retorna 404 se não houver imóveis.
                return Ok(imoveis); // Retorna 200 com a lista de imóveis.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar um imóvel específico pelo ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var imovel = _imovelService.GetImovelById(id);
                if (imovel == null)
                    return NotFound("Imóvel não encontrado."); // Retorna 404 se o imóvel não existir.
                return Ok(imovel); // Retorna 200 com o imóvel encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar um novo imóvel.
        [HttpPost]
        public IActionResult Create([FromBody] ImovelDto imovelDto)
        {
            if (imovelDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _imovelService.CreateImovel(imovelDto);
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

        // Endpoint para atualizar um imóvel existente.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ImovelDto imovelDto)
        {
            if (imovelDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _imovelService.UpdateImovel(id, imovelDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Imóvel não encontrado."); // Retorna 404 se o imóvel não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualização parcial de um imóvel.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] ImovelDto imovelDto)
        {
            if (imovelDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _imovelService.PartialUpdateImovel(id, imovelDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Imóvel não encontrado."); // Retorna 404 se o imóvel não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar um imóvel por ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _imovelService.DeleteImovel(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Imóvel não encontrado."); // Retorna 404 se o imóvel não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
