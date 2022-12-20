using Newtonsoft.Json;
using ProfileSharp.Execution.Scope;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Store.FileStore
{
    internal sealed class ProfilingFileStore : FileStore, IProfilingStore
    {
        private readonly JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        public ProfilingFileStore(string directory) : base(directory)
        {
        }

        public async Task StoreAsync(IExecutionScopeContext scopeContext, CancellationToken cancellationToken = default)
        {
            string filePath = GetFilePath(scopeContext.Steps.First().ExecutionContext);

            if (File.Exists(filePath))
            {
                return;
            }

            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            StreamWriter writer = new StreamWriter(fileStream);

            try
            {
                _serializer.Serialize(writer, scopeContext);

                await writer.FlushAsync();
            }
            finally
            {
                bool deleteFile = fileStream.Length == 0;

                await fileStream.FlushAsync(cancellationToken);
                await writer.DisposeAsync();

                if (deleteFile)
                {
                    SafeDelete(filePath);
                }
            }
        }

        private static void SafeDelete(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                // ignored
            }
        }
    }
}
