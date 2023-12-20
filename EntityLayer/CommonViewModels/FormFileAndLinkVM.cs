using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.CommonViewModels
{
    public class FormFileAndLinkVM
    {
        public IFormFile? File { get; set; }
        public string? Link { get; set; }
    }
}
