
using Org.BouncyCastle.X509;

namespace EstudoBouncyCastle.LCRFactory
{
    public class OfflineLcrRepository : LcrRepository
    {
        public override async Task<List<LCR>> GetX509LCR(X509Certificate certificado)
        {
            return new List<LCR>();
        }
    }
}
