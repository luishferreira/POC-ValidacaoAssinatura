using Org.BouncyCastle.X509;
using System;

namespace EstudoBouncyCastle.LCRFactory
{
    public abstract class LcrRepository
    {
        public abstract Task<List<LCR>> GetX509LCR(X509Certificate certificado);
    }
}
