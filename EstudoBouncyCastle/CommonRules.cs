using EstudoBouncyCastle.CommonRulesFolder;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;

namespace EstudoBouncyCastle
{
    /**
* The signer rules identify:
* <ul>
* <li>if the eContent is empty and the signature is calculated using
* a hash of signed data external to CMS structure;</li>
* <li>the CMS signed attributes that shall be provided by the signer
* under this policy;</li>
* <li>the CMS unsigned attribute that shall be provided by the signer
* under this policy;</li>
* <li>whether the certificate identifiers from the full certification
* path up to the trust point shall be provided by the signer in the
* SigningCertificate attribute;</li>
* <li>whether a signer's certificate, or all certificates in the
* certification path to the trust point shall be provided by the signer
* in the certificates field of SignedData.</li>
* </ul>
*
* <pre>
* SignerRules ::= SEQUENCE {
*     externalSignedData BOOLEAN OPTIONAL,
*     -- True if signed data is external to CMS structure
*     -- False if signed data part of CMS structure
*     -- not present if either allowed
*     mandatedSignedAttr {@link CMSAttrs},
*     -- Mandated CMS signed attributes
*     mandatedUnsignedAttr {@link CMSAttrs},
*     -- Mandated CMS unsigned attributed
*     mandatedCertificateRef [0] {@link CertRefReq} DEFAULT signerOnly,
*     -- Mandated Certificate Reference
*     mandatedCertificateInfo [1] {@link CertInfoReq} DEFAULT none,
*     -- Mandated Certificate Info
*     signPolExtensions [2]{@link SignPolExtensions} OPTIONAL
*     }
*
* CMSAttrs ::= SEQUENCE OF OBJECT IDENTIFIER *
* </pre>
*
* @see ASN1Boolean
* @see ASN1Encodable
* @see ASN1Sequence
* @see ASN1Primitive
* @see DERSequence
* @see DERTaggedObject
*/

    public class CommonRules
    {
        public SignerAndVerifierRules SignerAndVerifierRules { get; set; }

        public SigningCertTrustCondition SigningCertTrustCondition { get; set; }

        public TimestampTrustCondition TimestampTrustCondition { get; set; }

        public AttributeTrustCondition AttributeTrustCondition { get; set; }

        public AlgorithmConstraintSet AlgorithmConstraintSet { get; set; }

        public SignPolExtensions SignPolExtensions { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            if (derSequence.Count <= 0)
                return;

            foreach (Asn1Encodable asn1 in derSequence)
            {
                Asn1Object asn1Object = asn1.ToAsn1Object();

                if (asn1Object is DerTaggedObject derTaggedObject)
                {
                    TAG tag = (TAG)derTaggedObject.TagNo;

                    switch (tag)
                    {
                        case TAG.SignerAndVerifierRules:
                            SignerAndVerifierRules = new();
                            SignerAndVerifierRules.Parse(asn1Object);
                            break;
                        case TAG.SigningCertTrustCondition:
                            SigningCertTrustCondition = new();
                            SigningCertTrustCondition.Parse(asn1Object);
                            break;
                        case TAG.TimeStampTrustCondition:
                            TimestampTrustCondition = new();
                            TimestampTrustCondition.Parse(asn1Object);
                            break;
                        case TAG.AttributeTrustCondition:
                            AttributeTrustCondition = new();
                            AttributeTrustCondition.Parse(asn1Object);
                            break;
                        case TAG.AlgorithmConstraintSet:
                            AlgorithmConstraintSet = new();
                            AlgorithmConstraintSet.Parse(asn1Object);
                            break;
                        case TAG.SignPolExtension:
                            SignPolExtensions = new();
                            SignPolExtensions.Parse(asn1Object);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public enum TAG
    {
        SignerAndVerifierRules,
        SigningCertTrustCondition,
        TimeStampTrustCondition,
        AttributeTrustCondition,
        AlgorithmConstraintSet,
        SignPolExtension
    }

}
