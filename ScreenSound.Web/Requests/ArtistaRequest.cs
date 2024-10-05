using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests;
public record ArtistaRequest([Required] string Nome, [Required] string Bio);