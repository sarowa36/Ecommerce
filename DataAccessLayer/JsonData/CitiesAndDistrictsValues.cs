using DataAccessLayer.Base.JsonData;
using EntityLayer.JsonEntities.CitiesAndDistrics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace DataAccessLayer.JsonData
{
    public class CitiesAndDistrictsValues : ICitiesAndDistrictsValues
    {
        public List<City> Cities { get; set; }
        public CitiesAndDistrictsValues(IConfiguration configuration)
        {
            var str = "";
            var path =configuration.GetSection("CitiesAndDistrictsPath").Value;
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("'CitiesAndDistrictsPath' is not exist in configuration");
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var fsBytes = new byte[fs.Length];
                fs.Read(fsBytes, 0, fsBytes.Length);
                str = Encoding.UTF8.GetString(fsBytes);
            }
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException(path+" is not usable");
            Cities = JsonConvert.DeserializeObject<List<City>>(str);
        }
        public City GetCity(int id) => Cities.FirstOrDefault(x => x.Id == id);
        public District GetDistrict(int id) => Cities.SelectMany(x => x.Districts).First(x => x.Id == id);
    }
}
