using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class InformacoesAssinatura
    {
        public LinkedList<X509Certificate2> Cadeia { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public Timestamp TimeStampSigner { get; set; } = null;
        public PoliticaAssinatura PoliticaAssinatura { get; set; }
        public DateTime DataLimite { get; set; }
        public bool AssinaturaInvalida { get; set; } = false;
        public Certificado CertificadoIcpBrasil { get; set; } = null;

    }
}
