using Domain.Events;
using Microsoft.AspNetCore.Mvc;
using Producer.Dtos;
using Producer.MessageBus;

namespace Producer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public ClienteController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto cliente)
        {
            try
            {
                var clienteCriado = new ClienteCriadoEvent
                {
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    DataCadastro = DateTime.Now
                };

                await _messageBus.Publish(clienteCriado);

                return Ok("Cliente Cadastrado com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao cadastrar o cliente");
            }
        }
    }
}