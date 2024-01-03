using EntityLayer.CommonViewModels;

namespace EntityLayer.ViewModels.Admin.ProductController
{
    public class UpdatePostProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<FormFileAndLinkVM> Images { get; set; }
    }
}
