namespace P38.Services
{
    public interface IFileStorage
    {
        Task<string> SaveProductImageAsync(IFormFile file, CancellationToken ct = default);
        Task DeleteAsync(string? relativePath, CancellationToken ct = default);
    }
}
