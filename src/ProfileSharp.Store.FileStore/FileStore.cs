using ProfileSharp.Execution.Context;
using System;
using System.IO;

namespace ProfileSharp.Store.FileStore
{
    internal abstract class FileStore
    {
        private readonly string _directory;

        protected FileStore(string directory)
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
        }

        protected string GetFilePath(IExecutionContext executionContext)
        {
            string fileName = $"{executionContext.ComputeHash()}.json";

            return Path.Join(_directory, fileName);
        }
    }
}
