namespace EntityLayer.ViewModels.User.CustomerSaveController
{
    public class CustomerSaveVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomerSaveItemVM> Items { get; set; }
    }
}
