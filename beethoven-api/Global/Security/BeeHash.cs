using System;
using System.Security.Cryptography;
using System.Text;

namespace beethoven_api.Global.Security;

public class BeeHash
{
    /// <summary>
    /// Computes a hash of the given input string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A SHA256 hash of the given string.</returns>
    public static string GetHash(string input)
    {
        byte[] data = Encoding.UTF8.GetBytes(input);
        byte[] bytes = SHA256.HashData(data);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Validates whether the hash of the given input string matches the specified hash.
    /// </summary>
    /// <param name="input">The string to hash and compare.</param>
    /// <param name="hash">The hash to compare against.</param>
    /// <returns>True if the hash of the input matches the specified hash; otherwise, false.</returns>
    public static bool ValidateHash(string input, string hash)
    {
        return GetHash(input).Equals(hash);
    }
}
