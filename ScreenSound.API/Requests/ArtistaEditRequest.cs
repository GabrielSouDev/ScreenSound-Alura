namespace ScreenSound.API.Requests;
public record ArtistaEditRequest(int Id, string Nome, string Bio) : ArtistaRequest(Nome, Bio);