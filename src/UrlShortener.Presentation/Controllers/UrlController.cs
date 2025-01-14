using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Presentation.Controllers;

public class UrlController : Controller
{
    private readonly IUrlRepository _urlRepository;
    private readonly IUrlService _urlService;

    public UrlController(IUrlRepository urlRepository, IUrlService urlService)
    {
        _urlRepository = urlRepository;
        _urlService = urlService;
    }
    
    // Redirect endpoint
    [Route("{code}")]
    public async Task<IActionResult> RedirectByCode(string code)
    {
        var url = await _urlRepository.GetByCodeAsync(code);

        if (url is null)
        {
            return NotFound();
        }

        return Redirect(url.LongUrl);
    }
    
    public async Task<IActionResult> Table()
    {
        var urls = await _urlRepository.GetAllAsync();
        return View(urls);
    }

    [HttpPost]
    public async Task Create(string url)
    {
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/";
        await _urlService.CreateShortUrlAsync(url, baseUrl,"Vlad");
    }
}