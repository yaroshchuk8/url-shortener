using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly AppDbContext _context;

    public UrlRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Url>> GetAllAsync() => await _context.Urls.ToListAsync();

    public async Task<Url?> GetByIdAsync(int id) => await _context.Urls.FindAsync(id);

    public async Task<Url?> GetByOriginalUrlAsync(string originalUrl) =>
        await _context.Urls.FirstOrDefaultAsync(u => u.LongUrl == originalUrl);
    
    public async Task<Url?> GetByCodeAsync(string code) =>
        await _context.Urls.FirstOrDefaultAsync(u => u.Code == code);

    public async Task AddAsync(Url url)
    {
        _context.Urls.Add(url);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Url url)
    {
        _context.Urls.Remove(url);
        await _context.SaveChangesAsync();
    }
}