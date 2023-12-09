using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.IdentityController
{
    public class GetUserVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Roles { get; set; }
    }
}
