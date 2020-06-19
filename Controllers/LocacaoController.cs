using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Locadora.Data;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private DataContext _context;
        string mensagem;

        public LocacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Locacao>> Get([FromServices]DataContext context)
        {
            List<Locacao> result = await context.Locacoes
                .Include(x => x.Filme)
                .Include(x => x.Cliente)
                .ToListAsync();
            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Locacao> Get([FromServices]DataContext context, int Id)
        {
            Locacao result = await context.Locacoes
                .Include(x => x.Filme)
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.IdLocacao == Id);
            return result;
        }


        [HttpPost]
        public async Task<ActionResult<string>> Post([FromServices]DataContext context, [FromBody]Locacao locacao)
        {
            Cliente cliente = await context.Clientes.FirstOrDefaultAsync(x => x.IdCliente == locacao.ClienteId);
            Filme filme = await context.Filmes.FirstOrDefaultAsync(x => x.IdFilme == locacao.FilmeId);
            if (cliente == null || filme == null)
                return mensagem = "O filme ou cliente não estão cadastrados. Cadastre e tente novamente.";
            if (ModelState.IsValid)
            {
                context.Add(locacao);
                await context.SaveChangesAsync();
                return mensagem =
                    $"Locação do filme '{locacao.Filme.Nome}' concluída com sucesso no nome de '{locacao.Cliente.Nome}'";
            }
            else
                return BadRequest(ModelState);
        }

        [Route("id:int")]
        [HttpPut("{id}")]
        public async Task<string> Put([FromServices] DataContext context, [FromBody]Locacao locacao, int id)
        {
            Cliente cliente = await context.Clientes.FirstOrDefaultAsync(x => x.IdCliente == locacao.ClienteId);
            Filme filme = await context.Filmes.FirstOrDefaultAsync(x => x.IdFilme == locacao.FilmeId);
            Locacao loc = await Get(context, id);
            if (loc == null)
            {
                return mensagem = "Locação inexistente";
            }
            if (!loc.Devolveu)
            {
                loc.Devolveu = locacao.Devolveu;  //Só é possível alterar a propriedade da locação uma vez,
                context.Locacoes.Update(loc);     //no caso, se não tiver devolvido, alterar para devolvido
                await context.SaveChangesAsync();
                return mensagem = "Locação alterada com sucesso com sucesso.";
            }
            return mensagem = "Não foi possível alterar.";
        }
    }
}