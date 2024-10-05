﻿namespace ScreenSound.Shared.Modelos;

public class Musica
{
    public Musica() 
    { 
        Nome = string.Empty;
		Generos = new List<Genero>();
	}
    public Musica(string nome)
    {
        Nome = nome;
        Generos = new List<Genero>();
	}
    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public int ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public virtual ICollection<Genero> Generos { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}