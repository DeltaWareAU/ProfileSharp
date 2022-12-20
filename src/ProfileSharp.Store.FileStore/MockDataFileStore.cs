using Newtonsoft.Json;
using ProfileSharp.Execution.Context;
using ProfileSharp.Execution.Scope;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Store.FileStore
{
    internal sealed class MockDataFileStore : FileStore, IMockDataStore
    {
        private readonly JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        public MockDataFileStore(string directory) : base(directory)
        {
        }

        public Task<IExecutionScopeContext?> GetExecutionScopeAsync(IExecutionContext executionContext, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() =>
            {
                string filePath = GetFilePath(executionContext);

                if (!File.Exists(filePath))
                {
                    return null;
                }

                using StreamReader fileStream = File.OpenText(filePath);

                return (IExecutionScopeContext)_serializer.Deserialize(fileStream, typeof(IExecutionScopeContext));
            }, cancellationToken);
        }
    }
}
