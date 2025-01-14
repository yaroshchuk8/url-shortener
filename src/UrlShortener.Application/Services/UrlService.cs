using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _repository;
    private readonly Random _random = new();
    
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int NumberOfCharsInShortLink = 7;
    
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

    public async Task CreateShortUrlAsync(string originalUrl, string baseUrl, string createdBy)
    {
        var existing = await _repository.GetByOriginalUrlAsync(originalUrl);
        if (existing != null) throw new InvalidOperationException("URL already exists.");

        var code = GenerateShortUrl(originalUrl);
        
        var shortUrl = new Url
        {
            LongUrl = originalUrl,
            ShortUrl = baseUrl + code,
            CreatedBy = createdBy,
            Code = code,
            CreatedOnUtc = DateTime.UtcNow
        };

        await _repository.AddAsync(shortUrl);
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
        var codeChars = new char[NumberOfCharsInShortLink];

        for (var i = 0; i < NumberOfCharsInShortLink; i++)
        {
            int randomIndex = _random.Next(Alphabet.Length - 1);
            
            codeChars[i] = Alphabet[randomIndex];
        }

        var code = new string(codeChars);
        
        return code;
    }
}