using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureMobileClient.Helpers
{
    /// <summary>
    /// AzureCloudTable
    /// </summary>
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        private IMobileServiceClient _client { get; }
        private IMobileServiceTable<T> table { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="AzureCloudTable(IMobileServiceClient)" />
        /// </summary>
        public AzureCloudTable(IMobileServiceClient client)
        {
            _client = client;
            this.table = _client.GetTable<T>();
        }

        /// <inheritDoc />
        public async Task<T> CreateItemAsync(T item)
        {
            await table.InsertAsync(item);
            return item;
        }

        /// <inheritDoc />
        public async Task DeleteItemAsync(T item)
        {
            await table.DeleteAsync(item);
        }

        /// <inheritDoc />
        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            return await table.ToListAsync();
        }

        /// <inheritDoc />
        public async Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            return await table.Skip(start).Take(count).ToListAsync();
        }

        /// <inheritDoc />
        public virtual async Task<T> ReadItemAsync(string id)
        {
            return await table.LookupAsync(id);
        }

        /// <inheritDoc />
        public virtual async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
    }
}