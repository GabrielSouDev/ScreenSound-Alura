namespace ScreenSound.API.Requests;
public record GeneroEditRequest(int Id, string Nome, string Descricao) : GeneroRequest(Nome, Descricao);
