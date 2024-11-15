using Repository;
using Repository.Models;
using Service.Dto;
using Service.Parser;
using Service.Validate;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ContatoService
    {
        private readonly ContatoRepository _repository;

        // Construtor que recebe uma instância de ContatoRepository.
        public ContatoService(ContatoRepository repository)
        {
            _repository = repository;
        }

        // Método para buscar todos os contatos e convertê-los em DTOs.
        public List<ContatoDto> GetAllContatos()
        {
            var contatos = _repository.GetAll();
            return contatos.Select(ContatoParser.ToDto).ToList();
        }

        // Método para buscar um contato pelo ID e retornar como DTO.
        public ContatoDto GetContatoById(int id)
        {
            var contato = _repository.GetById(id);
            return contato != null ? ContatoParser.ToDto(contato) : null;
        }

        // Método para criar um novo contato após validação dos dados.
        public void CreateContato(ContatoDto dto)
        {
            // Valida os dados do DTO.
            ContatoValidator.Validate(dto);

            // Verifica se o ID já existe.
            IdValidator.ValidateUniqueId(dto.Id, _repository);

            var contato = ContatoParser.ToEntity(dto);
            _repository.Add(contato);
        }

        // Método para atualizar completamente um contato pelo ID.
        public void UpdateContato(int id, ContatoDto dto)
        {
            var existingContato = _repository.GetById(id);
            if (existingContato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            // Desanexa a entidade existente para evitar conflitos no rastreamento.
            _repository.Detach(existingContato);

            ContatoValidator.Validate(dto);
            var updatedContato = ContatoParser.ToEntity(dto);
            updatedContato.Id = id; // Mantém o ID original.
            _repository.Update(updatedContato);
        }

        // Método para atualização parcial de um contato.
        public void PartialUpdateContato(int id, ContatoDto dto)
        {
            var existingContato = _repository.GetById(id);
            if (existingContato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            // Desanexa a entidade para evitar conflitos.
            _repository.Detach(existingContato);

            // Atualiza somente os campos não nulos/fornecidos no DTO.
            if (!string.IsNullOrWhiteSpace(dto.Nome)) existingContato.Nome = dto.Nome;
            if (!string.IsNullOrWhiteSpace(dto.Telefone)) existingContato.Telefone = dto.Telefone;
            if (!string.IsNullOrWhiteSpace(dto.Email)) existingContato.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Interesse)) existingContato.Interesse = dto.Interesse;

            _repository.Update(existingContato);
        }

        // Método para deletar um contato pelo ID.
        public void DeleteContato(int id)
        {
            var contato = _repository.GetById(id);
            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            _repository.Delete(id);
        }
    }
}
