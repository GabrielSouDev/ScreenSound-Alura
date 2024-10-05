namespace ScreenSound.API.Requests;
public record MusicaEditRequest(int Id, string Nome, int ArtistaId, int AnoLancamento) : MusicaRequest(Nome, ArtistaId, AnoLancamento);