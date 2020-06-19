using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.Data;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private DataContext _context;
        string mensagem;
        public ClienteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Cliente>> Get([FromServices] DataContext context)
        {
            List<Cliente> clientes = await context.Clientes
                .AsNoTracking()
                .ToListAsync();
            return clientes;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Cliente> Get([FromServices]DataContext context, int Id)
        {
            Cliente cliente = await context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdCliente == Id);
            return cliente;
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<Cliente> Get([FromServices]DataContext context, string cpf)
        {
            Cliente cliente = await context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CPF == cpf);
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromServices] DataContext context, [FromBody]Cliente cliente)
        {
            cliente.CPF = cliente.CPF.Replace("-", "").Replace(".", "");
            Cliente cli = await Get(context, cliente.CPF);
            if(cli == null || cli.CPF != cliente.CPF)
            {
                if (ModelState.IsValid)
                {
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                    return mensagem = $"Cliente '{cliente.Nome}' adicionado.";
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return mensagem = $"Cliente '{cliente.Nome}' já existe.";
            
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<string>> Put([FromServices] DataContext context, [FromBody] Cliente cliente, int Id)
        {
            Cliente cli = await Get(context, Id);
            if (ModelState.IsValid)
            {
                cliente.CPF = cliente.CPF.Replace("-", "").Replace(".", "");
                if (cli.CPF != cliente.CPF && cli != null)
                {
                    var clienteAntigo = cliente.Nome;
                    cli.Nome = cliente.Nome;
                    cli.Sobrenome = cliente.Sobrenome;
                    cli.CPF = cliente.CPF;
                    context.Clientes.Update(cli);
                    await context.SaveChangesAsync();
                    return mensagem = $"Cliente '{clienteAntigo}' alterado com sucesso para '{cliente.Nome}'.";
                }
                else
                    mensagem = "Não foi possível alterar o cliente.";
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<string> Delete([FromServices]DataContext context, int Id)
        {
            Cliente cliente = await Get(context, Id);
            bool estaLocando = context.Locacoes.Where(x => x.ClienteId == Id).Any();
            if (cliente != null && !estaLocando)
            {
                var clienteAntigo = cliente.Nome;
                context.Remove(cliente);
                await context.SaveChangesAsync();
                return mensagem = $"Cliente '{clienteAntigo}' excluído.";
            }
            return mensagem = "O cliente não pôde ser excluído por estar com algum filme locado ou não estar cadastrado.";
        }
    }
}