using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{
    //private static readonly byte[] Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("StudentAccommodationKey1")); // 32-byte key
    //private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte IV (Fixed)
    private static readonly string EncryptionKey = "YourSecureEncryptionKey";
    private const int SaltSize = 16;

    // Number of iterations for PBKDF2
    private const int Iterations = 10000;

    // Key size for AES (valid values: 128, 192, 256 bits)
    private const int KeySize = 256;

    // **Encrypt Password (Returns byte[] for VARBINARY storage)**
    public static string EncryptPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // Derive key from password and salt using PBKDF2
        byte[] key = DeriveKeyFromPassword(EncryptionKey, salt);

        // Create AES encryptor
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.Key = key;
            aes.GenerateIV(); // Generate random IV

            // Create encryptor
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (MemoryStream ms = new MemoryStream())
            {
                // First write the salt
                ms.Write(salt, 0, salt.Length);

                // Then write the IV
                ms.Write(aes.IV, 0, aes.IV.Length);

                // Create CryptoStream and encrypt
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(password);
                }

                // Convert everything to Base64 for storage
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // **Decrypt Password (Accepts byte[] from DB)**
    public static string DecryptPassword(string encryptedData)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        // Extract salt (first SaltSize bytes)
        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(encryptedBytes, 0, salt, 0, SaltSize);

        // Extract IV (next 16 bytes for AES)
        byte[] iv = new byte[16]; // AES block size is always 16 bytes
        Buffer.BlockCopy(encryptedBytes, SaltSize, iv, 0, iv.Length);

        // Derive the same key from password and salt
        byte[] key = DeriveKeyFromPassword(EncryptionKey, salt);

        // Decrypt
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.Key = key;
            aes.IV = iv;

            // Create decryptor
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (MemoryStream ms = new MemoryStream())
            {
                // Calculate the offset where the actual encrypted data begins
                int dataOffset = SaltSize + iv.Length;
                int dataLength = encryptedBytes.Length - dataOffset;

                // Create CryptoStream for decryption
                using (CryptoStream cs = new CryptoStream(
                    new MemoryStream(encryptedBytes, dataOffset, dataLength),
                    decryptor,
                    CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    // **Verify Password (For Login)**
    private static byte[] DeriveKeyFromPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(KeySize / 8); // KeySize is in bits, GetBytes takes bytes
        }
    }
}
