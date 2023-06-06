﻿using System;
using System.Buffers;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using StabilityMatrix.Models;

namespace StabilityMatrix.Helper;

public static class FileHash
{
    public static async Task<string> GetHashAsync(HashAlgorithm hashAlgorithm, Stream stream, byte[] buffer, Action<ulong>? progress = default)
    {
        ulong totalBytesRead = 0;

        using (hashAlgorithm)
        {
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer)) != 0)
            {
                totalBytesRead += (ulong) bytesRead;
                hashAlgorithm.TransformBlock(buffer, 0, bytesRead, null, 0);
                progress?.Invoke(totalBytesRead);
            }
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            var hash = hashAlgorithm.Hash;
            if (hash == null || hash.Length == 0)
            {
                throw new InvalidOperationException("Hash algorithm did not produce a hash.");
            }
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
    
    public static async Task<string> GetSha256Async(string filePath, IProgress<ProgressReport>? progress = default)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Could not find file: {filePath}");
        }

        var totalBytes = Convert.ToUInt64(new FileInfo(filePath).Length);
        var shared = ArrayPool<byte>.Shared;
        var buffer = shared.Rent((int) FileTransfers.GetBufferSize(totalBytes));
        try
        {
            await using var stream = File.OpenRead(filePath);

            var hash = await GetHashAsync(SHA256.Create(), stream, buffer, totalBytesRead =>
            {
                progress?.Report(new ProgressReport(totalBytesRead, totalBytes));
            });
            return hash;
        }
        finally
        {
            shared.Return(buffer);
        }

    }
}
