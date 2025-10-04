using Consumer.Dtos;
using Consumer.Services;
using Domain.Events;
using MassTransit;

namespace Consumer.Consumers
{
    public class ClienteCriadoConsumer : IConsumer<ClienteCriadoEvent>
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClienteCriadoConsumer> _logger;

        public ClienteCriadoConsumer(ILogger<ClienteCriadoConsumer> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        public async Task Consume(ConsumeContext<ClienteCriadoEvent> context)
        {
            var mensagem = context.Message;

            try
            {
                _logger.LogInformation($"Processando cadastro do cliente: {mensagem.Nome}");

                var clienteDto = new ClienteDto
                {
                    Nome = mensagem.Nome,
                    Email = mensagem.Email,
                    DataCadastro = mensagem.DataCadastro
                };

                var result = await _clienteService.SalvarCliente(clienteDto);

                if (!await _clienteService.SalvarCliente(clienteDto))
                {
                    _logger.LogInformation($"Erro ao cadastrar o cliente: {mensagem.Nome}");
                    return;
                }

                _logger.LogInformation($"Cliente {mensagem.Nome} cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao processar cliente {mensagem.Nome}");
                throw;
            }
        }
    }
}