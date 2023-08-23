using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class DigestImpl
    {
        public string Algorithm { get; set; } = "SHA-256";

        public readonly int BUFSIZE = 256;

        public byte[] Digest(byte[] content)
        {
            byte[] result = null;

            Algorithm ??= "SHA-256";

            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }

    public enum DigestAlgorithm
    {
        //MD5 = "MD5",
        //SHA_1 = "SHA-1",
        //SHA_224("SHA224"),
        //SHA_256("SHA-256"),
        //SHA_384("SHA384"),
        //SHA_512("SHA-512"),
        //SHA3_224("SHA3-224"),
        //SHA3_256("SHA3-256"),
        //SHA3_384("SHA3-384"),
        //SHA3_512("SHA3-512"),
        //SHAKE_128("SHAKE128"),
        //SHAKE_256("SHAKE256");
    }

}
