using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class Timestamp
    {

    }

    public class TimestampOperator
    {
        public Timestamp Timestamp { get; set; }

        public void Validate(byte[] content, byte[] timestamp, byte[] hash)
        {
            TimeStampToken timeStampToken = new(new CmsSignedData(timestamp));
            CmsSignedData signedData = timeStampToken.ToCmsSignedData();

            int verified = 0;


            var certificados = signedData.GetCertificates();
            var assinadoresInfo = signedData.GetSignerInfos();
            var assinadores = assinadoresInfo.GetSigners();

            foreach (var assinador in assinadores)
            {
                var certCollection = certificados.EnumerateMatches(assinador.SignerID);
                X509Certificate cert = certCollection.First();
                if (assinador.Verify(cert))
                {
                    verified++;
                }
                cert.GetExtensionValue(new Org.BouncyCastle.Asn1.DerObjectIdentifier("2.5.29.31"));
                timeStampToken.Validate(cert);
            }

            Console.WriteLine("signature verified");

            //Valida o hash  incluso no carimbo de tempo com hash do arquivo carimbado
            byte[] calculatedHash = null;
            if(content != null)
            {
                
            }
        }
    }
}
