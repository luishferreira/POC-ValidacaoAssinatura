using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class CAManager
    {
        private static CAManager instance;

        public static CAManager Instance
        {
            get
            {
                instance ??= new CAManager();
                return instance;
            }
        }

        public LinkedList<X509Certificate2> GetCertificateChain(Org.BouncyCastle.X509.X509Certificate certBC, X509Certificate2 cert)
        {
            // o Demoiselle pega de outro jeito, bem mais complexo, porem retornou a mesma coisa desse jeito. (Stael fez assim).
            //if(IsRootCA(certBC))
            //{
            //    return retorno;
            //}

            LinkedList<X509Certificate2> retorno = new();
            X509Chain chain = new();

            chain.Build(cert);

            if (chain.ChainElements != null && chain.ChainElements.Count > 0)
            {
                foreach (var elementos in chain.ChainElements)
                {
                    if (elementos.Certificate.Thumbprint != cert.Thumbprint)
                    {
                        //TODO ?? validador stael;
                    }
                    retorno.AddLast(elementos.Certificate);
                }
            }

            return retorno;
        }
    }
}
