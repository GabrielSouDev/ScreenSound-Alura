namespace ScreenSound.Web.Requests;
public record ArtistaEditRequest(int Id, string Nome, string Bio) : ArtistaRequest(Nome, Bio);