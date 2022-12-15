using Newtonsoft.Json;
using ProfileSharp.Execution.Scope;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Store.FileStore
{
    internal sealed class ProfilingFileStore : IProfilingStore
    {
        private readonly JsonSerializer _serializer;

        private readonly string _directory;

        public ProfilingFileStore(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(directory));
            }

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"The specified Directory ({directory}) could not be found.");
            }

            _directory = directory;

            _serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public async Task StoreAsync(IExecutionScopeContext scopeContext, CancellationToken cancellationToken = default)
        {
            string fileName = $"profile_{Guid.NewGuid()}.json";

            string filePath = Path.Join(_directory, fileName);

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
