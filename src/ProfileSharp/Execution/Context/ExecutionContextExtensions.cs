using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ProfileSharp.Execution.Context
{
    public static class ExecutionContextExtensions
    {
        /// <summary>
        /// Computes the MD5 hash for this <see cref="IExecutionContext"/>.
        /// </summary>
        /// <param name="executionContext">The <see cref="IExecutionContext"/> to be hashed.</param>
        /// <returns>The MD5 Hash of the <see cref="IExecutionContext"/>.</returns>
        public static string ComputeHash(this IExecutionContext executionContext)
        {
            using MD5 md5Algorithm = MD5.Create();

            return ComputeHash(executionContext, md5Algorithm);
        }

        /// <summary>
        /// Computes the hash for this <see cref="IExecutionContext"/> using the specified <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <param name="executionContext">The <see cref="IExecutionContext"/> to be hashed.</param>
        /// <param name="hashingAlgorithm">The hashing algorithm to be used.</param>
        /// <returns>The computed hash of the <see cref="IExecutionContext"/>.</returns>
        public static string ComputeHash(this IExecutionContext executionContext, HashAlgorithm hashingAlgorithm)
        {
            byte[] contextBytes = JsonSerializer.SerializeToUtf8Bytes(executionContext);
            byte[] hashBytes = hashingAlgorithm.ComputeHash(contextBytes);

            StringBuilder sOutput = new StringBuilder(hashBytes.Length);

            for (int i = 0; i < hashBytes.Length - 1; i++)
            {
                sOutput.Append(hashBytes[i].ToString("X2"));
            }

            return sOutput.ToString();
        }
    }
}
