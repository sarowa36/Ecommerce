using EntityLayer.Entities;
using ServiceLayer.Base.ServiceResults;

namespace ServiceLayer.ServiceResults.ShoppingCartService
{
    public class GetListResponse:AbstractResponseWithErrorsAndValue<List<ShoppingCartItem>>
    {
    }
}
