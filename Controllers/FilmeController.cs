using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.Data;
using Locadora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private DataContext _context;
        string mensagem;

        public FilmeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Filme>> Get([FromServices]DataContext context)
        {
            List<Filme> result = await context.Filmes
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Filme> Get([FromServices]DataContext context, int Id)
        {
            Filme result = await context.Filmes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdFilme == Id);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromServices]DataContext context, [FromBody]Filme filme)
        {
            if (ModelState.IsValid)
            {
                context.Add(filme);
                await context.SaveChangesAsync();
                return mensagem = $"Filme '{filme.Nome}' adicionado";
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<string>> Put([FromServices]DataContext context, [FromBody]Filme filme, int Id)
        {
            if (ModelState.IsValid)
            {
                Filme f = await Get(context, Id);
                var nomeAntigo = f.Nome;
                if (f == null)
                    return null;
                f.Nome = filme.Nome;
                f.Categoria = filme.Categoria;
                f.Disponivel = filme.Disponivel;
                context.Filmes.Update(f);
                await context.SaveChangesAsync();
                return mensagem = $"Filme '{nomeAntigo}' alterado para '{f.Nome}'.";
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<string> Delete([FromServices]DataContext context, int Id)
        {
            Filme filme = await Get(context, Id);
            string nomeAntigo;
            bool estaLocado = context.Locacoes.Where(x => x.FilmeId == Id).Any();
            if (filme != null && !estaLocado)
            {
                nomeAntigo = filme.Nome;
                context.Remove(filme);
                await context.SaveChangesAsync();
                return mensagem = $"Filme '{nomeAntigo}' excluído!";
            }
            return mensagem = "O filme não pôde ser excluído por estar alugado ou não estar cadastrado na base";
        }
    }
}