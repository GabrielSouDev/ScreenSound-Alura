using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos;

namespace ScreenSound.API.EndPoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointsMusicas(this WebApplication app)
        {
            #region EndPoint.Musicas
            app.MapGet("/Musicas", async ([FromServices] DAL<Musica> dal) =>
            {
                return Results.Ok(EntityListToResponseList(await dal.ListarAsync()));
            });

            app.MapGet("/Musicas/{nome}", async ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = await dal.RecuperarPorAsync(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(EntityToResponse(musica));
            });

            app.MapPost("/Musicas", async ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest, [FromServices] DAL<Genero> dalGenero) =>
            {
                var musica = new Musica(musicaRequest.Nome)
                {
                    ArtistaId = musicaRequest.ArtistaId,
                    AnoLancamento = musicaRequest.AnoLancamento,
                    Generos = musicaRequest.Generos is not null ? await GenerosExtensions.ResponseListToEntityListAsync(musicaRequest.Generos, dalGenero) :
                    new List<Genero>()
                };
                await dal.AdicionarAsync(musica);
                return Results.Created();
            });

            app.MapDelete("/Musicas/{id}", async ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musica = await dal.RecuperarPorAsync(a => a.Id.Equals(id));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                await dal.DeletarAsync(musica);
                return Results.NoContent();
            });
            app.MapPut("/Musicas", async ([FromServices] DAL<Musica> dal, [FromBody] MusicaEditRequest musicaEditRequest) =>
            {
                var musicaRecuperada = await dal.RecuperarPorAsync(a => a.Id.Equals(musicaEditRequest.Id));
                if (musicaRecuperada is null)
                {
                    return Results.NotFound();
                }

                musicaRecuperada.Nome = musicaEditRequest.Nome;
                musicaRecuperada.AnoLancamento = musicaEditRequest.AnoLancamento;
                await dal.AtualizarAsync(musicaRecuperada);
                return Results.Ok();
            });
            #endregion
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
    }
}
