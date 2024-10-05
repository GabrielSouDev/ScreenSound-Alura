using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos;
using System.Numerics;

namespace ScreenSound.API.EndPoints;

public static class GenerosExtensions
{
    public static void AddEndPointsGeneros(this WebApplication app)
    {
        #region EndPointsGeneros
        app.MapGet("/Generos", async ([FromServices] DAL<Genero> dal) =>
        {
            return EntityListToResponse(await dal.ListarAsync());
        });

        app.MapGet("/Generos/{nome}", async ([FromServices] DAL<Genero> dal, string nome) =>
        {
            var musica = await dal.RecuperarPorAsync(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if(musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(musica));
        });

        app.MapPost("/Generos", async ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) => 
        {
            var genero = await dal.RecuperarPorAsync(a => a.Nome == generoRequest.Nome);
            if(genero is not null)
            {
                return Results.Conflict();
            }
            await dal.AdicionarAsync(RequestToEntity(generoRequest));
            return Results.Created();
        });

        app.MapPut("/Generos", async ([FromServices] DAL<Genero> dal, [FromBody] GeneroEditRequest generoRequest) =>
        {
            var genero = await dal.RecuperarPorAsync(a => a.Id.Equals(generoRequest.Id));
            if (genero is null)
            {
                return Results.NotFound();
            }
            genero.Nome = generoRequest.Nome;
            genero.Descricao = generoRequest.Descricao;
            await dal.AtualizarAsync(genero);
            return Results.Ok();
        });

        app.MapDelete("/Generos/{id}", async ([FromServices] DAL<Genero> dal, int id) =>
        {
            var genero = await dal.RecuperarPorAsync(a => a.Id.Equals(id));
            if(genero is null)
            {
                return Results.NotFound();
            }
            await dal.DeletarAsync(genero);
            return Results.Ok();
        });
        #endregion
    }
    public static async Task<ICollection<Genero>> ResponseListToEntityListAsync(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
    {
        var listaDeGeneros = new List<Genero>();
        foreach (var item in generos)
        {
            var entity = RequestToEntity(item);
            var genero = await dalGenero.RecuperarPorAsync(a => a.Nome.ToUpper().Equals(item.Nome.ToUpper()));
            if(genero is not null)
            {
                listaDeGeneros.Add(genero);
            }
            else
            {
                listaDeGeneros.Add(entity);
            }
        }
        return listaDeGeneros;
    }
    private static Genero RequestToEntity(GeneroRequest generos) 
    { 
        return new Genero() { Nome = generos.Nome, Descricao = generos.Descricao };
    }
    private static ICollection<GeneroResponse> EntityListToResponse(IEnumerable<Genero> generos)
    {
        return generos.Select(a=> EntityToResponse(a)).ToList();
    }
    private static GeneroResponse EntityToResponse(Genero genero)
    { 
        return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
    }
}
