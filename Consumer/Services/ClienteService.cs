using Consumer.Dtos;
using Consumer.Entity;
using Consumer.Repositories;

namespace Consumer.Services
{
    public interface IClienteService
    {
        Task<bool> SalvarCliente(ClienteDto clienteDto);
    }

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(ILogger<ClienteService> logger, IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        public async Task<bool> SalvarCliente(ClienteDto clienteDto)
        {
            var clienteExistente = await _clienteRepository.GetClienteByEmail(clienteDto.Email);

            if (clienteExistente != null)
            {
                _logger.LogWarning($"Email {clienteDto.Email} já cadastrado para o cliente {clienteExistente.Nome}");
                return false;
            }

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                DataCadastro = clienteDto.DataCadastro,
                DataProcessamento = DateTime.Now
            };

            if (!await _clienteRepository.SalvarCliente(cliente))
            {
                _logger.LogInformation($"Erro ao cadastrar o cliente: {clienteDto.Nome} !");
                return false;
            }

            _logger.LogInformation($"Cliente {clienteDto.Nome} cadastrado com sucesso!");
            return true;
        }
    }
}