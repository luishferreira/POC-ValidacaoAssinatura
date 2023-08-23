using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class PoliticaAssinatura
    {
        public AlgorithmIdentifier SignPolicyHashAlg { get; set; } = new();
        public InformacoesPoliticaAssinatura InformacoesPoliticaAssinatura { get; set; } = new();
        public SignPolicyHash SignPolicyHash { get; set; }
        public string UrlPoliticaAssinatura { get; set; }
        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            SignPolicyHashAlg.Parse(derSequence[0].ToAsn1Object());

            InformacoesPoliticaAssinatura.Parse(derSequence[1].ToAsn1Object());

            if (derSequence.Count == 3)
            {
                SignPolicyHash = new((DerOctetString)derSequence[2]);
            }
        }
    }

    public class InformacoesPoliticaAssinatura
    {
        public ObjectIdentifier SignPolicyIdentifier { get; set; } = new();
        public GeneralizedTime DateOfIssue { get; set; } = new();
        public PolicyIssuerName PolicyIssuerName { get; set; } = new();
        public FieldOfApplication FieldOfApplication { get; set; } = new();
        public SignatureValidationPolicy SignatureValidationPolicy { get; set; } = new();
        public SignPolExtensions SignPolExtensions { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            SignPolicyIdentifier.Parse(derSequence[0].ToAsn1Object());

            DateOfIssue.Parse(derSequence[1].ToAsn1Object());

            PolicyIssuerName.Parse(derSequence[2].ToAsn1Object());

            FieldOfApplication.Parse(derSequence[3].ToAsn1Object());

            SignatureValidationPolicy.Parse(derSequence[4].ToAsn1Object());

            if (derSequence.Count == 6)
            {
                SignPolExtensions = new();
                SignPolExtensions.Parse(derSequence[5].ToAsn1Object());
            }
        }
    }

    public class SignatureValidationPolicy
    {
        public SigningPeriod SigningPeriod { get; set; } = new();

        public CommonRules CommonRules { get; set; } = new();

        public CommitmentRules CommitmentRules { get; set; } = new();

        public SignPolExtensions SignPolExtensions { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            SigningPeriod.Parse(derSequence[0].ToAsn1Object());

            CommonRules.Parse(derSequence[1].ToAsn1Object());

            CommitmentRules.Parse(derSequence[2].ToAsn1Object());

            if (derSequence.Count == 4)
            {
                SignPolExtensions = new();
                SignPolExtensions.Parse(derSequence[3].ToAsn1Object());
            }
        }
    }

    public class SignPolicyHash : OctetString
    {
        public SignPolicyHash(DerOctetString derOctetString)
        {
            DerOctetString = derOctetString;
        }
    }

}
