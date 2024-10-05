using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests;
public record GeneroRequest([Required]string Nome, string Descricao);
