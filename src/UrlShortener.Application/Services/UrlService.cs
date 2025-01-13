using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _repository;
    
    public UrlService(IUrlRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Url>> GetAllUrlsAsync()
    {
        var urls = await _repository.GetAllAsync();
        return urls;
    }

    public async Task<Url?> GetUrlByIdAsync(int id)
    {
        var url = await _repository.GetByIdAsync(id);
        return url;
    }

    public async Task<Url> CreateShortUrlAsync(string originalUrl, string createdBy)
    {
        var existing = await _repository.GetByOriginalUrlAsync(originalUrl);
        if (existing != null) throw new InvalidOperationException("URL already exists.");

        var shortUrl = new Url
        {
            OriginalUrl = originalUrl,
            ShortenedUrl = GenerateShortUrl(originalUrl),
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        await _repository.AddAsync(shortUrl);
        return new Url
        {
            Id = shortUrl.Id,
            OriginalUrl = shortUrl.OriginalUrl,
            ShortenedUrl = shortUrl.ShortenedUrl,
            CreatedBy = shortUrl.CreatedBy,
            CreatedDate = shortUrl.CreatedDate
        };
    }

    public async Task DeleteUrlAsync(int id, string currentUser)
    {
        var url = await _repository.GetByIdAsync(id);
        if (url == null) throw new KeyNotFoundException("URL not found.");

        if (url.CreatedBy != currentUser)
            throw new UnauthorizedAccessException("Cannot delete this URL.");

        await _repository.DeleteAsync(url);
    }

    private string GenerateShortUrl(string originalUrl)
    {
        // Simple Base62 encoding
        var hash = Math.Abs(originalUrl.GetHashCode());
        return Convert.ToBase64String(BitConverter.GetBytes(hash))
            .Replace("/", "").Replace("+", "").Substring(0, 8);
    }
}