using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace RapidForce
{
    internal class ModelCollection<TModel> : IModelCollection<TModel> where TModel : IModel
    {
        /// <summary>
        /// The unique name of the collection.
        /// </summary>
        public string Name { get; }

        private readonly IDictionary<int, TModel> models = new Dictionary<int, TModel>();

        public ModelCollection(Script script, string name)
        {
            Name = name;

            script.AddHandler($"{Event.PrefixServer}:models:{name}:Get", new Func<int, CallbackDelegate, Task>(HandleGet));
            script.AddHandler($"{Event.PrefixServer}:models:{name}:Count", new Func<CallbackDelegate, Task>(HandleCount));
            script.AddHandler($"{Event.PrefixServer}:models:{name}:Add", new Func<dynamic, CallbackDelegate, Task>(HandleAdd));
            script.AddHandler($"{Event.PrefixServer}:models:{name}:Contains", new Func<int, CallbackDelegate, Task>(HandleContains));
            script.AddHandler($"{Event.PrefixServer}:models:{name}:Remove", new Func<int, CallbackDelegate, Task>(HandleRemove));
            script.AddHandler($"{Event.PrefixServer}:models:{name}:List", new Func<CallbackDelegate, Task>(HandleList));
        }
        
        private async Task HandleGet(int handle, CallbackDelegate ret)
        {
            TModel model = await GetAsync(handle);
            if (model == null)
            {
                ret?.Invoke(null);
                return;
            }
            var obj = model.Pack();
            ret?.Invoke(obj);
        }

        private async Task HandleCount(CallbackDelegate count)
        {
            count?.Invoke(models.Count);
            await Task.FromResult(0);
        }

        private async Task HandleAdd(dynamic data, CallbackDelegate err)
        {
            var model = Activator.CreateInstance<TModel>();
            try
            {
                model.Unpack(data);
                await AddAsync(model);
                err?.Invoke(string.Empty);
            }
            catch (Exception ex)
            {
                err?.Invoke(ex.Message);
            }
        }

        private async Task HandleContains(int handle, CallbackDelegate contains)
        {
            contains?.Invoke(models.ContainsKey(handle));
            await Task.FromResult(0);
        }

        private async Task HandleRemove(int handle, CallbackDelegate removed)
        {
            removed?.Invoke(models.Remove(handle));
            await Task.FromResult(0);
        }

        private async Task HandleList(CallbackDelegate list)
        {
            int[] handles = new int[models.Count];
            models.Keys.CopyTo(handles, 0);
            list?.Invoke(handles);
            await Task.FromResult(0);
        }

        public async Task<TModel> GetAsync(int handle)
        {
            return await Task.FromResult(models[handle]);
        }

        public async Task AddAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            models.Add(model.Handle, model);
            await Task.FromResult(0);
        }

        public async Task<int> CountAsync()
        {
            return await Task.FromResult(models.Count);
        }

        public async Task<bool> ContainsAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            bool exists = models.ContainsKey(model.Handle);
            return await Task.FromResult(exists);
        }

        public async Task<bool> RemoveAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            bool ok = models.Remove(model.Handle);
            return await Task.FromResult(ok);
        }
    }
}
