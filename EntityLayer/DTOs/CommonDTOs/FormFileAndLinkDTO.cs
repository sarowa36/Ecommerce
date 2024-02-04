using Microsoft.AspNetCore.Http;

namespace EntityLayer.DTOs.CommonDTOs
{
    public class FormFileAndLinkDTO
    {
        public IFormFile? File { get; set; }
        public string? Link { get; set; }
    }
}
