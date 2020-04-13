using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAllCategories<T>();

        IEnumerable<T> GetSubCategoriesOfCategoryId<T>(int id);

        T GetCategoryByName<T>(string name);

        T GetSubCategoryByName<T>(string name);
    }
}
