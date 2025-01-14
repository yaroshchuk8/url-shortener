## Requirements: .NET 9 SDK, SQLite

Update database:
```bash
cd .\url-shortener\
dotnet ef database update -p .\src\UrlShortener.Infrastructure\ -s .\src\UrlShortener.Presentation\
```

Start program from terminal:
```bash
cd .\src\UrlShortener.Presentation\
dotnet run
```
> Terminal log: **Now listening on: http://localhost:port**