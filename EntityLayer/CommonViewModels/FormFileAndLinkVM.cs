using Microsoft.AspNetCore.Http;

namespace EntityLayer.CommonViewModels
{
    public class FormFileAndLinkVM
    {
        public IFormFile? File { get; set; }
        public string? Link { get; set; }
    }
}
