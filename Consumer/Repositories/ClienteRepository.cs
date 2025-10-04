using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.Context;
using Consumer.Entity;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetClienteByEmail(string email);
        Task<bool> SalvarCliente(Cliente cliente);
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetClienteByEmail(string email)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<bool> SalvarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}