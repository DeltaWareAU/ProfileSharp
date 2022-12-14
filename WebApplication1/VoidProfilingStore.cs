using Newtonsoft.Json;
using ProfileSharp.Execution.Scope;
using ProfileSharp.Profiling.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class FileProfilingStore : IProfilingStore
    {
        private readonly string _path;

        public FileProfilingStore(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"The specified Profiling Store Directory ({path}) could not be found.");
            }

            _path = path;
        }

        public async Task StoreAsync(IExecutionScopeContext scopeContext, CancellationToken cancellationToken = default)
        {
            string filePath = _path + $"\\profile_{Guid.NewGuid()}.json";

            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            StreamWriter writer = new StreamWriter(fileStream);

            try
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                serializer.Serialize(writer, scopeContext);

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
