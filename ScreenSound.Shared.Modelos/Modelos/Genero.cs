namespace ScreenSound.Shared.Modelos;

public class Genero
{
    public Genero()
    {
        Musicas = new List<Musica>();
    }
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public virtual ICollection<Musica> Musicas { get; set; }
    public override string ToString()
    {
        return $"Nome: {Nome} - Descrição: {Descricao}";
    }
}
