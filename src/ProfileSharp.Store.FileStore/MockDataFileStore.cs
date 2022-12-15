using Newtonsoft.Json;
using ProfileSharp.Execution.Context;
using ProfileSharp.Execution.Scope;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Store.FileStore
{
    internal sealed class MockDataFileStore : IMockDataStore
    {
        private readonly IReadOnlyCollection<IExecutionScopeContext> _mockingScopes;

        public MockDataFileStore(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(directory));
            }

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"The specified Directory ({directory}) could not be found.");
            }

            _mockingScopes = LoadMockData(directory).ToArray();
        }

        public Task<IExecutionScopeContext?> GetExecutionScopeAsync(IExecutionContext executionContext, CancellationToken cancellationToken = default)
        {
            IExecutionScopeContext? scope = _mockingScopes.SingleOrDefault(s => s.Steps.First().ExecutionContext.Equals(executionContext));

            return Task.FromResult(scope ?? null);
        }

        private static IEnumerable<IExecutionScopeContext> LoadMockData(string directory)
        {
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            foreach (string filePath in Directory.GetFiles(directory))
            {
                using StreamReader fileStream = File.OpenText(filePath);

                yield return (IExecutionScopeContext)serializer.Deserialize(fileStream, typeof(IExecutionScopeContext));
            }
        }
    }
}
