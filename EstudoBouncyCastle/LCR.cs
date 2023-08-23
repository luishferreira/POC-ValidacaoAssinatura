using Org.BouncyCastle.X509;

namespace EstudoBouncyCastle
{
    /// <summary>
    /// Lista de certificados revogados
    /// </summary>
    public class LCR
    {
        public X509Crl Lcr { get; set; }

        public LCR(byte[] data)
        {
            Lcr = getInstance(data);
        }

        private X509Crl getInstance(byte[] data)
        {
            X509Crl lcr = new(data);

            return lcr;
        }
    }
}
