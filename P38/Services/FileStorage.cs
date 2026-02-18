namespace P38.Services
{
    public class FileStorage : IFileStorage
    {
        private static readonly HashSet<string> AllowedExt = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".webp"
        };

        private const long MaxBytes = 3 * 1024 * 1024;

        private IWebHostEnvironment _env;

        public async Task<string> SaveProductImageAsync(IFormFile file, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.Length > MaxBytes)
            {
                throw new InvalidOperationException("Image is too large, must be less then 3 MB");
            }

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext) || !AllowedExt.Contains(ext))
            {
                throw new InvalidOperationException("Invalid image format");
            }

            var webRoot = "wwwroot";
            var dir = Path.Combine(webRoot, "uploads", "products");
            Directory.CreateDirectory(dir);

            var name = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(dir, name);

            await using var fs = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fs, ct);

            return $"/uploads/products/{name}";

        }

        public Task DeleteAsync(string? relativePath, CancellationToken ct = default)
        {

            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return Task.CompletedTask;
            }
            var webRootPath = _env.WebRootPath ?? "wwwroot";
            var trimmed = relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(webRootPath, trimmed);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }
}
