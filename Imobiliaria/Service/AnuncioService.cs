using Repository;
using Repository.Models;
using Service.Dto;
using Service.Parser;
using Service.Validate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Service
{
    public class AnuncioService
    {
        private readonly AnuncioRepository _repository;

        // Construtor que recebe o repositório de anúncios para interação com a camada de dados.
        public AnuncioService(AnuncioRepository repository)
        {
            _repository = repository;
        }

        // Método para obter todos os anúncios e convertê-los em DTOs.
        public List<AnuncioDto> GetAllAnuncios()
        {
            var anuncios = _repository.GetAll();
            return anuncios.Select(AnuncioParser.ToDto).ToList();
        }

        // Método para buscar um anúncio por ID e retornar um DTO.
        public AnuncioDto GetAnuncioById(int id)
        {
            var anuncio = _repository.GetById(id);
            return anuncio != null ? AnuncioParser.ToDto(anuncio) : null;
        }

        // Método para criar um novo anúncio após validar os dados do DTO.
        public void CreateAnuncio(AnuncioDto dto)
        {
            // Valida os dados do DTO.
            AnuncioValidator.Validate(dto);

            // Verifica se já existe um anúncio com o mesmo ID.
            var existingAnuncio = _repository.GetById(dto.Id);
            if (existingAnuncio != null)
                throw new ValidationException("Já existe um anúncio com este Id.");

            // Converte o DTO em entidade e adiciona ao repositório.
            var anuncio = AnuncioParser.ToEntity(dto);
            _repository.Add(anuncio);
        }

        // Método para atualizar um anúncio por ID, substituindo todos os seus campos.
        public void UpdateAnuncio(int id, AnuncioDto dto)
        {
            var existingAnuncio = _repository.GetById(id);
            if (existingAnuncio == null)
                throw new KeyNotFoundException("Anúncio não encontrado.");

            // Desanexa a entidade existente para evitar conflitos no contexto de dados.
            _repository.Detach(existingAnuncio);

            // Valida os dados do DTO.
            AnuncioValidator.Validate(dto);

            // Atualiza o anúncio com os dados do DTO.
            var updatedAnuncio = AnuncioParser.ToEntity(dto);
            updatedAnuncio.Id = id; // Mantém o ID original.
            _repository.Update(updatedAnuncio);
        }

        // Método para atualizar parcialmente os campos de um anúncio.
        public void PartialUpdateAnuncio(int id, AnuncioDto dto)
        {
            var existingAnuncio = _repository.GetById(id);
            if (existingAnuncio == null)
                throw new KeyNotFoundException("Anúncio não encontrado.");

            // Desanexa a entidade existente para evitar conflitos no contexto de dados.
            _repository.Detach(existingAnuncio);

            // Atualiza apenas os campos fornecidos no DTO.
            if (!string.IsNullOrWhiteSpace(dto.Titulo)) existingAnuncio.Titulo = dto.Titulo;
            if (!string.IsNullOrWhiteSpace(dto.Descricao)) existingAnuncio.Descricao = dto.Descricao;
            if (dto.DataPublicacao != default) existingAnuncio.DataPublicacao = dto.DataPublicacao;
            existingAnuncio.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.Plataforma)) existingAnuncio.Plataforma = dto.Plataforma;

            // Salva as alterações no repositório.
            _repository.Update(existingAnuncio);
        }

        // Método para deletar um anúncio por ID.
        public void DeleteAnuncio(int id)
        {
            var anuncio = _repository.GetById(id);
            if (anuncio == null)
                throw new KeyNotFoundException("Anúncio não encontrado.");

            // Remove o anúncio do repositório.
            _repository.Delete(id);
        }
    }
}
