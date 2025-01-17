using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces;

public interface IUrlService
{
    Task<IEnumerable<Url>> GetAllUrlsAsync();
    Task<Url?> GetUrlByIdAsync(int id);
    Task CreateShortUrlAsync(string originalUrl, string baseUrl, string createdBy);
    Task DeleteUrlAsync(int id, string currentUser);
}