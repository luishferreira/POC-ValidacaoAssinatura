using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.CommonRulesFolder
{
    /**
* The SignerAndVerifierRules consists of signer rule and
* verification rules as defined below:
*
* <pre>
* SignerAndVerifierRules ::= SEQUENCE {
*     signerRules {@link SignerRules},
*     verifierRules {@link VerifierRules}
* }
* </pre>
*
* @see ASN1Sequence
* @see ASN1Primitive
*/
    public class SignerAndVerifierRules
    {
        public SignerRules SignerRules { get; set; } = new();
        public VerifierRules VerifierRules { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            SignerRules.Parse(derSequence[0].ToAsn1Object());

            VerifierRules.Parse(derSequence[1].ToAsn1Object());
        }
    }
    public class SignerRules
    {
        public bool? ExternalSignedData { get; set; } = null;

        public CMSAttrs MandatedSignedAttr { get; set; } = new();

        public CMSAttrs MandatedUnsignedAttr { get; set; } = new();

        public CertRefReqEnum MandatedCertificateRef { get; set; } = CertRefReqEnum.SignerOnly;

        public CertInfoReqEnum MandatedCertificateInfo { get; set; } = CertInfoReqEnum.None;

        public SignPolExtensions SignPolExtensions { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable obj in derSequence)
            {
                Asn1Object asn1Object = obj.ToAsn1Object();

                if (asn1Object is DerTaggedObject derTaggedObject)
                {
                    SignerRulesTag tag = (SignerRulesTag)derTaggedObject.TagNo;
                    switch (tag)
                    {
                        case SignerRulesTag.MandatedCertificateRef:
                            MandatedCertificateRef = (CertRefReqEnum)CustomAsn1Object.GetDerEnumerated(asn1Object).Value.IntValue;
                            break;
                        case SignerRulesTag.MandatedCertificateInfo:
                            MandatedCertificateInfo = (CertInfoReqEnum)CustomAsn1Object.GetDerEnumerated(asn1Object).Value.IntValue;
                            break;
                        case SignerRulesTag.SignPolExtensions:
                            SignPolExtensions = new();
                            SignPolExtensions.Parse(derTaggedObject.GetObject());
                            break;
                        default:
                            break;
                    }
                }
            }

            int i = 0;
            Asn1Encodable asn1 = derSequence[i];
            if (asn1 is not DerSequence)
            {
                if (asn1 is DerBoolean asn1Boolean)
                {
                    ExternalSignedData = asn1Boolean.IsTrue;
                }
                i++;
            }

            MandatedSignedAttr.Parse(derSequence[i].ToAsn1Object());
            i++;
            MandatedUnsignedAttr.Parse(derSequence[i].ToAsn1Object());
        }
    }
    public class VerifierRules
    {
        public CMSAttrs MandatedUnsignedAttr { get; set; } = new();
        public SignPolExtensions SignPolExtensions { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            MandatedUnsignedAttr.Parse(derSequence[0].ToAsn1Object());

            if (derSequence.Count == 2)
            {
                SignPolExtensions = new();
                SignPolExtensions.Parse(derSequence[1].ToAsn1Object());
            }
        }
    }
    public class CMSAttrs
    {
        public List<ObjectIdentifier> ObjectIdentifiers { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                ObjectIdentifier oid = new();
                oid.Parse(value.ToAsn1Object());
                ObjectIdentifiers.Add(oid);
            }
        }
    }

    public enum CertRefReqEnum
    {
        SignerOnly,
        FullPath
    }
    public enum CertInfoReqEnum
    {
        None,
        SignerOnly,
        FullPath,
    }
    public enum SignerRulesTag
    {
        MandatedCertificateRef,
        MandatedCertificateInfo,
        SignPolExtensions
    }
}
