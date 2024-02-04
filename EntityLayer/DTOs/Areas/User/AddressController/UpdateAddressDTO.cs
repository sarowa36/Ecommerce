namespace EntityLayer.DTOs.Areas.User.AddressController
{
    public class UpdateAddressDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Zip { get; set; }
        public string Detail { get; set; }
    }
}
