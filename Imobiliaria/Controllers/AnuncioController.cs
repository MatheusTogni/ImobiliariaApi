using Microsoft.AspNetCore.Mvc;
using Repository;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base para o controlador, facilitando o acesso aos endpoints.
    public class AnuncioController : ControllerBase
    {
        private readonly AnuncioService _anuncioService;

        // Injeção do contexto do banco de dados para inicializar o serviço de anúncios.
        public AnuncioController(MyDbContext dbContext)
        {
            _anuncioService = new AnuncioService(new AnuncioRepository(dbContext));
        }

        // Endpoint para listar todos os anúncios cadastrados.
        // Implementa a separação entre a leitura (GET) e a escrita (POST/PUT/PATCH).
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var anuncios = _anuncioService.GetAllAnuncios();
                if (anuncios == null || !anuncios.Any())
                    return NotFound("Nenhum anúncio cadastrado."); // Retorna 404 se não houver anúncios.

                return Ok(anuncios); // Retorna 200 com a lista de anúncios.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar um anúncio específico pelo ID.
        // Utiliza o padrão "DTO" para abstrair a lógica da entidade.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var anuncio = _anuncioService.GetAnuncioById(id);
                if (anuncio == null)
                    return NotFound("Anúncio não encontrado."); // Retorna 404 se o anúncio não existir.

                return Ok(anuncio); // Retorna 200 com o anúncio encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar um novo anúncio.
        // Aplica o padrão "DTO" para receber os dados necessários.
        [HttpPost]
        public IActionResult Create([FromBody] AnuncioDto anuncioDto)
        {
            if (anuncioDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _anuncioService.CreateAnuncio(anuncioDto);
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

        // Endpoint para atualizar um anúncio existente.
        // Utiliza o método PUT para substituir completamente os dados do anúncio.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AnuncioDto anuncioDto)
        {
            if (anuncioDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _anuncioService.UpdateAnuncio(id, anuncioDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Anúncio não encontrado."); // Retorna 404 se o anúncio não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para fazer uma atualização parcial de um anúncio.
        // Utiliza o método PATCH para modificar apenas campos específicos.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] AnuncioDto anuncioDto)
        {
            if (anuncioDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _anuncioService.PartialUpdateAnuncio(id, anuncioDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Anúncio não encontrado."); // Retorna 404 se o anúncio não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar um anúncio pelo ID.
        // Aplica o padrão "Command" para ações de escrita e modificação.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _anuncioService.DeleteAnuncio(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Anúncio não encontrado."); // Retorna 404 se o anúncio não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
