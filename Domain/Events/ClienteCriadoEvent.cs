namespace Domain.Events
{
    public class ClienteCriadoEvent
    {
        public string Nome { get; set; }        
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}