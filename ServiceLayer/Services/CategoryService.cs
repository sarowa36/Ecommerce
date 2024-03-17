using DataAccessLayer;
using DataAccessLayer.Base.Repositories.CategoryRepositories;
using DataAccessLayer.Migrations;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using DataAccessLayer.Helpers;

namespace ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryReadRepository _categoryReadRepository;
        readonly ICategoryWriteRepository _categoryWriteRepository;
        readonly IServiceErrorContainer _errorContainer;
        readonly ADC _db;
        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IServiceErrorContainer errorContainer, ADC db)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _errorContainer = errorContainer;
            _db = db;
        }
        public async Task<List<Category>> GetAll() 
        {
            var allCategories = _categoryReadRepository.GetAll().Include(x => x.Childrens).OrderBy(x => x.SortIndex).ToList();
            allCategories.ForEach(x => x.Childrens = x.Childrens.OrderBy(y => y.SortIndex).ToList());
            return allCategories.ToList(); 
        }
        /// <summary>
        /// Returns list with categories with parent category names like child1.name="child1" instead child1.name="parent1/parent2/child1". 
        /// <paramref name="id"/> paramater for removing from list which category have this id. In short id paramater prevents recursive confusion like upper category cannot refer to their children.
        /// </summary>
        public async Task<List<Category>> GetAllWithParentName(int? id = null, bool JustChild = false)
        {
            var allCategories = _categoryReadRepository.GetAll().Include(x => x.Childrens).OrderBy(x => x.SortIndex).AsNoTracking().ToList();
            if (id != null && id > 0)
            {
                allCategories = allCategories.Where(x => x.ParentId == null).ToList().FindRecursiveValueAndRemoveFromList((int)id).RecursiveListToDefaultListParentToChild();
            }
            List<Category> categories = new List<Category>(JustChild ? allCategories.Where(x => x.Childrens.Count == 0).ToList() : allCategories);

            foreach (var item in categories)
            {
                List<string> parentNames = new List<string>();
                if (item.ParentId != null)
                {
                    foreach (var item2 in RecursiveHelpers.RecursiveListToDefaultListChildToParent((int)item.Id, allCategories))
                    {
                        parentNames.Add(item2.Name);
                    }
                    if (parentNames.Count > 4)
                    {
                        List<string> vs = new List<string>();
                        vs.AddRange(parentNames.GetRange(0, 2));
                        vs.Add("...");
                        vs.AddRange(parentNames.GetRange(vs.Count - 2, 2));
                        parentNames = vs;
                    }
                    parentNames.Reverse();
                    item.Name = string.Join(" / ", parentNames);
                    //item.Data.FirstOrDefault(x => x.LangId == 3).
                }
            }
            return categories;
        }
        public async Task<Category> Get(int id)
        {
            var returnValue= _categoryReadRepository.Get(id);
            if (returnValue != null) {
                returnValue.Childrens?.OrderBy(x => x.SortIndex);
                return returnValue;
            }
            _errorContainer.AddModelOnlyError("Category not found");
            return default;
            
        }
        public async Task Create(Category category)
        {
            await _categoryWriteRepository.CreateAsync(category);
            await _categoryWriteRepository.SaveChangesAsync();
        }
        public async Task Update(Category category)
        {
            await _categoryWriteRepository.UpdateAsync(category);
            await _categoryWriteRepository.SaveChangesAsync();
        }
        public async Task UpdateSortIndex(Dictionary<int, int> model)
        {
            _categoryWriteRepository.UpdateSortIndex(model);
            await _categoryWriteRepository.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            await _categoryWriteRepository.DeleteAsync(id);
            await _categoryWriteRepository.SaveChangesAsync();
        }
    }
}
