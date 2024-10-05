namespace ScreenSound.Web.Requests;
public record MusicaEditRequest(int Id, string Nome, int ArtistaId, int AnoLancamento) : MusicaRequest(Nome, ArtistaId, AnoLancamento);