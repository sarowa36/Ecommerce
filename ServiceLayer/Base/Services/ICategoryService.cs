using EntityLayer.Entities;

namespace ServiceLayer.Base.Services
{
    public interface ICategoryService
    {
        Task Create(Category category);
        Task<List<Category>> GetAll();
        Task<List<Category>> GetAllWithParentName(int? id = null, bool JustChild = false);
        Task<Category> Get(int id);
        Task Update(Category category);
        Task UpdateSortIndex(Dictionary<int, int> model);
        Task Delete(int id);
    }
}