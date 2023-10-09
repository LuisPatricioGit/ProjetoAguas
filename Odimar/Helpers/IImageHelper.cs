using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Odimar.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
