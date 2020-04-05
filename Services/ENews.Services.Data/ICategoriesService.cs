using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetCategoryByName<T>(string name);
    }
}
