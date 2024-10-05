using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests;
public record GeneroRequest([Required] string Nome, string Descricao);
