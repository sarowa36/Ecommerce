using EntityLayer.JsonEntities.CitiesAndDistrics;

namespace DataAccessLayer.Base.JsonData
{
    public interface ICitiesAndDistrictsValues
    {
        List<City> Cities { get; set; }

        City GetCity(int id);
        District GetDistrict(int id);
    }
}