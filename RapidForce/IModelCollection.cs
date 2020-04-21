using System;
using System.Threading.Tasks;

namespace RapidForce
{
    public interface IModelCollection<TModel> where TModel : IModel
    {
        string Name { get; }

        Task<TModel> GetAsync(int handle);

        Task AddAsync(TModel model);

        Task<int> CountAsync();

        Task<bool> ContainsAsync(TModel model);

        Task<bool> RemoveAsync(TModel model);
    }
}
