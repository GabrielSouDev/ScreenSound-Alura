using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ScreenSound.Banco;

public class DAL<T> where T : class
{
    private readonly ScreenSoundContext context;

    public DAL(ScreenSoundContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<T>> ListarAsync()
    {
        return await context.Set<T>().ToListAsync();
    }
    public async Task AdicionarAsync(T objeto)
    {
        await context.Set<T>().AddAsync(objeto);
        await context.SaveChangesAsync();
    }
    public async Task AtualizarAsync(T objeto)
    {
        context.Set<T>().Update(objeto);
        await context.SaveChangesAsync();
    }
    public async Task DeletarAsync(T objeto)
    {
        context.Set<T>().Remove(objeto);
        await context.SaveChangesAsync();
	}
    public async Task<T?> RecuperarPorAsync(Expression<Func<T, bool>> condicao)
    {
        return await context.Set<T>().FirstOrDefaultAsync(condicao);
    }
}
