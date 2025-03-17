

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.EntityFrameworkCore;

namespace Benchmark;
[MemoryDiagnoser] // Hafıza kullanımını analiz etmek için
public class EfCoreBenchmark
{
    private readonly AppDbContext _context;

    public EfCoreBenchmark()
    {
        _context = new AppDbContext();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Benchmark]
    public async Task InsertUsers()
    {
        var users = Enumerable.Range(1, 1000)
            .Select(i => new User { Name = $"User {i}", Age = i % 100 })
            .ToList();

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task<User> QueryUserById()
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == 500);
    }

    [Benchmark]
    public async Task<int> CountUsers()
    {
        return await _context.Users.CountAsync();
    }
}