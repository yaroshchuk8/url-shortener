using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces;

public interface IUrlRepository
{
    Task<IEnumerable<Url>> GetAllAsync();
    Task<Url?> GetByIdAsync(int id);
    Task<Url?> GetByOriginalUrlAsync(string originalUrl);
    Task<Url?> GetByCodeAsync(string code);
    Task AddAsync(Url url);
    Task DeleteAsync(Url url);
}