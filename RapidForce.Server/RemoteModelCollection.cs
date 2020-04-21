using System;
using System.Threading.Tasks;

using CitizenFX.Core;

namespace RapidForce
{
    internal class RemoteModelCollection<TModel> : IModelCollection<TModel> where TModel : IModel
    {
        public RemoteModelCollection(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Task AddAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var tcs = new TaskCompletionSource<string>();
            dynamic data = model.Pack();
            BaseScript.TriggerEvent($"{Event.PrefixServer}:models:{Name}:Add", data, new Action<string>((err) =>
            {
                if (!string.IsNullOrEmpty(err))
                {
                    tcs.SetException(new Exception(err));
                }
            }));
            return tcs.Task;
        }

        public Task<bool> ContainsAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var tcs = new TaskCompletionSource<bool>();
            BaseScript.TriggerEvent($"{Event.PrefixServer}:models:{Name}:Contains", model, new Action<bool>((exists) =>
            {
                tcs.SetResult(exists);
            }));
            return tcs.Task;
        }

        public Task<int> CountAsync()
        {
            var tcs = new TaskCompletionSource<int>();
            BaseScript.TriggerEvent($"{Event.PrefixServer}:models:{Name}:Count", new Action<int>((count) =>
            {
                tcs.SetResult(count);
            }));
            return tcs.Task;
        }

        public Task<TModel> GetAsync(int handle)
        {
            var tcs = new TaskCompletionSource<TModel>();
            BaseScript.TriggerEvent($"{Event.PrefixServer}:models:{Name}:Get", handle, new Action<dynamic>((data) =>
            {
                var model = Activator.CreateInstance<TModel>();
                model.Unpack(data);
                tcs.SetResult(model);
            }));
            return tcs.Task;
        }

        public Task<bool> RemoveAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var tcs = new TaskCompletionSource<bool>();
            BaseScript.TriggerEvent($"{Event.PrefixServer}:models:{Name}:Remove", model.Handle, new Action<dynamic>((ok) =>
            {
                tcs.SetResult(ok);
            }));
            return tcs.Task;
        }
    }
}
