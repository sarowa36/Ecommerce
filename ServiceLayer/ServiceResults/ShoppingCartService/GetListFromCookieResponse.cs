using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;
using ServiceLayer.Base.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceResults.AnonymShoppingCartService
{
    public class GetListFromCookieResponse : AbstractResponseWithErrorsAndValue<List<AnonymShoppingCartListValueVM>>
    {
    }
}
