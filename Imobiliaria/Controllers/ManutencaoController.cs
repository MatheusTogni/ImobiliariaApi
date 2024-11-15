using Microsoft.AspNetCore.Mvc;
using Repository;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como 'api/manutencao'.
    public class ManutencaoController : ControllerBase
    {
        private readonly ManutencaoService _manutencaoService;

        // Injeção do contexto do banco de dados para inicializar o serviço de manutenção.
        public ManutencaoController(MyDbContext dbContext)
        {
            _manutencaoService = new ManutencaoService(new ManutencaoRepository(dbContext), new ImovelRepository(dbContext));
        }

        // Endpoint para listar todas as manutenções.
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var manutencoes = _manutencaoService.GetAllManutencoes();
                if (manutencoes == null || !manutencoes.Any())
                    return NotFound("Nenhuma manutenção cadastrada."); // Retorna 404 se não houver manutenções.
                return Ok(manutencoes); // Retorna 200 com a lista de manutenções.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar uma manutenção específica pelo ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var manutencao = _manutencaoService.GetManutencaoById(id);
                if (manutencao == null)
                    return NotFound("Manutenção não encontrada."); // Retorna 404 se a manutenção não existir.
                return Ok(manutencao); // Retorna 200 com a manutenção encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar uma nova manutenção.
        [HttpPost]
        public IActionResult Create([FromBody] ManutencaoDto manutencaoDto)
        {
            if (manutencaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _manutencaoService.CreateManutencao(manutencaoDto);
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

        // Endpoint para atualizar uma manutenção existente.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ManutencaoDto manutencaoDto)
        {
            if (manutencaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _manutencaoService.UpdateManutencao(id, manutencaoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Manutenção não encontrada."); // Retorna 404 se a manutenção não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualização parcial de uma manutenção.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] ManutencaoDto manutencaoDto)
        {
            if (manutencaoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _manutencaoService.PartialUpdateManutencao(id, manutencaoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Manutenção não encontrada."); // Retorna 404 se a manutenção não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar uma manutenção por ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _manutencaoService.DeleteManutencao(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Manutenção não encontrada."); // Retorna 404 se a manutenção não for encontrada.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
