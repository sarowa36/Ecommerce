namespace EntityLayer.ViewModels.User.AddressController
{
    public class CreateAddressVM
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Zip { get; set; }
        public string Detail { get; set; }
    }
}
