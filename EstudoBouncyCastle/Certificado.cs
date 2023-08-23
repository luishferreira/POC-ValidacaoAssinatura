using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class Certificado
    {
        public static readonly string OID_A1_CERTIFICATE = "2.16.76.1.2.1";
        public static readonly string OID_A2_CERTIFICATE = "2.16.76.1.2.2";
        public static readonly string OID_A3_CERTIFICATE = "2.16.76.1.2.3";
        public static readonly string OID_A4_CERTIFICATE = "2.16.76.1.2.4";
        public static readonly string OID_S1_CERTIFICATE = "2.16.76.1.2.101";
        public static readonly string OID_S2_CERTIFICATE = "2.16.76.1.2.102";
        public static readonly string OID_S3_CERTIFICATE = "2.16.76.1.2.103";
        public static readonly string OID_S4_CERTIFICATE = "2.16.76.1.2.104";

        public X509Certificate2 CertificadoDotNet { get; set; }
        public Org.BouncyCastle.X509.X509Certificate CertificadoBouncy { get; set; }

        public Certificado(Org.BouncyCastle.X509.X509Certificate certificate)
        {
            CertificadoDotNet = new(certificate.GetEncoded());
            CertificadoBouncy = certificate;
        }

        public Certificado(X509Certificate2 certificate)
        {
            CertificadoDotNet = certificate;
            CertificadoBouncy = new X509CertificateParser().ReadCertificate(certificate.RawData);
        }

        public List<string> GetLcrDistributionPoint()
        {
            List<string> lcrUrl = new();

            DerObjectIdentifier identificadorLcr = new("2.5.29.31");

            Asn1OctetString asn1OctetString = CertificadoBouncy.GetExtensionValue(identificadorLcr);
            byte[] hahaha = asn1OctetString.GetOctets();
            CrlDistPoint crlDistPoint = CrlDistPoint.GetInstance(hahaha);

            DistributionPoint[] distributionPoints = crlDistPoint.GetDistributionPoints();

            foreach (DistributionPoint distributionPoint in distributionPoints)
            {
                DistributionPointName dpn = distributionPoint.DistributionPointName;
                if (dpn != null)
                {
                    GeneralName[] generalNames = GeneralNames.GetInstance(dpn.Name).GetNames();

                    foreach (GeneralName generalName in generalNames)
                    {
                        if (generalName.TagNo == GeneralName.UniformResourceIdentifier)
                        {
                            string url = DerIA5String.GetInstance(generalName.Name).GetString();
                            lcrUrl.Add(url);
                        }
                    }
                }
            }

            return lcrUrl;
        }

        public bool IsCACertificate()
        {
            return CertificadoBouncy.GetBasicConstraints() >= 0;
        }
    }
}
