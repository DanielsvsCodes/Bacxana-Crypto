using System;
using System.Text;
using Sodium;

namespace Bacxana.Cryptography
{
    public class Wallet
    {
        public string PublicKey { get; private set; } = string.Empty;
        public byte[] PrivateKey { get; private set; } = null!;

        public Wallet()
        {
            GenerateKeys();
        }

        private void GenerateKeys()
        {
            var keyPair = PublicKeyAuth.GenerateKeyPair();

            PrivateKey = keyPair.PrivateKey;
            PublicKey = Convert.ToBase64String(keyPair.PublicKey);
        }

        public string SignData(string data)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var signature = PublicKeyAuth.SignDetached(dataBytes, PrivateKey);

            return Convert.ToBase64String(signature);
        }

        public static bool VerifySignature(string publicKey, string data, string signature)
        {
            try
            {
                var publicKeyBytes = Convert.FromBase64String(publicKey);
                var signatureBytes = Convert.FromBase64String(signature);
                var dataBytes = Encoding.UTF8.GetBytes(data);

                return PublicKeyAuth.VerifyDetached(signatureBytes, dataBytes, publicKeyBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying signature: {ex.Message}");
                return false;
            }
        }
    }
}
