using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.CommonRulesFolder
{
    /**
* The SigningCertTrustCondition field identifies trust conditions
* for certificate path processing used to validate the  signing
* certificate.
*
* <pre>
* SigningCertTrustCondition ::= SEQUENCE {
*     signerTrustTrees {@link CertificateTrustTrees},
*     signerRevReq {@link CertRevReq}
* }
* </pre>
*
* @see ASN1Sequence
* @see ASN1Primitive
*/
    public class SigningCertTrustCondition
    {
        public CertificateTrustTrees SignerTrustTrees { get; set; } = new();

        public CertRevReq SignerRevReq { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            SignerTrustTrees.Parse(derSequence[0].ToAsn1Object());
            SignerRevReq.Parse(derSequence[1].ToAsn1Object());
        }
    }

    public class CertRevReq
    {
        public RevReq EndCertRevReq { get; set; } = new();
        public RevReq CaCerts { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            EndCertRevReq.Parse(derSequence[0].ToAsn1Object());

            CaCerts.Parse(derSequence[1].ToAsn1Object());
        }
    }

    public class RevReq
    {
        public RevReqEnum RevReqEnum { get; set; }

        public SignPolExtensions ExRevReq { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);
            RevReqEnum = (RevReqEnum)CustomAsn1Object.GetDerEnumerated(derSequence[0].ToAsn1Object()).Value.IntValue;

            if (derSequence.Count == 2)
            {
                ExRevReq = new();
                ExRevReq.Parse(derSequence[1].ToAsn1Object());
            }
        }

    }

    /**
* EnuRevReq ::= ENUMERATED {
* <p>
* clrCheck (0), --Checks shall be made against current CRLs (or authority revocation lists)
* ocspCheck (1), -- The revocation status shall be checked
* using the Online Certificate Status Protocol (RFC 2450)
* bothCheck (2), -- Both CRL and OCSP checks shall be carried out
* eitherCheck (3), -- At least one of CRL or OCSP checks shall be carried out
* noCheck (4), -- no check is mandated
* other (5) -- Other mechanism as defined by signature policy extension }
*
* @see ASN1Enumerated
* @see ASN1Primitive
*/

    public enum RevReqEnum
    {
        ClrCheck,
        OcspCheck,
        BothCheck,
        EitherCheck,
        NoCheck,
        Other,
    }

    public class CertificateTrustTrees
    {
        public List<CertificateTrustPoint> CertificateTrustPoints { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                CertificateTrustPoint certificateTrustPoint = new();
                certificateTrustPoint.Parse(value.ToAsn1Object());
                CertificateTrustPoints.Add(certificateTrustPoint);
            }
        }
    }

    /**
* parse an org.bouncycastle.asn1.ASN1Primitive to get
* <p>
* trustpoint Certificate, -- self-signed certificate   @see X509Certificate
* {@link PathLenConstraint } OPTIONAL,
* {@link AcceptablePolicySet } OPTIONAL, -- If not present "any policy"
* {@link NameConstraints } OPTIONAL,
* {@link PolicyConstraints } OPTIONAL
*
* @see ASN1Primitive
* @see ASN1Sequence
*/
    public class CertificateTrustPoint
    {
        public X509Certificate2 TrustPoint { get; set; }
        public PathLenConstraint PathLenConstraint { get; set; }
        public AcceptablePolicySet AcceptablePolicySet { get; set; }

        //?
        //public NameConstraints NameConstraints { get; set; }

        public PolicyConstraints PolicyConstraints { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            DerSequence x509Sequence = CustomAsn1Object.GetDerSequence(derSequence[0].ToAsn1Object());

            try
            {
                X509Certificate2 certificado = new(x509Sequence.GetEncoded());
                TrustPoint = certificado;
            }
            catch (Exception)
            {
                throw;
            }

            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();

                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    CertTrustPointEnum certEnum = (CertTrustPointEnum)derTaggedObject.TagNo;

                    switch (certEnum)
                    {
                        case CertTrustPointEnum.PathLenConstraint:
                            PathLenConstraint = new();
                            PathLenConstraint.Parse(asn1);
                            break;
                        case CertTrustPointEnum.AcceptablePolicySet:
                            AcceptablePolicySet = new();
                            AcceptablePolicySet.Parse(asn1);
                            break;
                        case CertTrustPointEnum.NameConstraint:
                            Console.WriteLine("not implemented");
                            //TODO:?
                            break;
                        case CertTrustPointEnum.PolicyConstraints:
                            PolicyConstraints = new();
                            PolicyConstraints.Parse(asn1);
                            break;
                        default:
                            break;
                    }
                }

            }

        }
    }

    public class PolicyConstraints
    {
        List<int> RequireExplicitPolicy { get; set; }
        List<int> InhibitPolicyMapping { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();
                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    PolicyConstraintsEnum policyEnum = (PolicyConstraintsEnum)derTaggedObject.TagNo;

                    switch (policyEnum)
                    {
                        case PolicyConstraintsEnum.RequireExplicitPolicy:
                            Console.WriteLine("Not implemented");
                            break;
                        case PolicyConstraintsEnum.InhibitPolicyMapping:
                            Console.WriteLine("Not implemented");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public enum PolicyConstraintsEnum
    {
        RequireExplicitPolicy,
        InhibitPolicyMapping
    }

    public class AcceptablePolicySet
    {
        public List<ObjectIdentifier> CertPolicyIds { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                ObjectIdentifier oid = new();
                oid.Parse(value.ToAsn1Object());
                CertPolicyIds.Add(oid);
            }
        }
    }

    /**
* The pathLenConstraint field gives the maximum number of CA certificates
* that may be in a certification path following the trustpoint.
* A value of zero indicates that only the given trustpoint certificate and an end-entity
* certificate may be used.
* If present, the pathLenConstraint field shall be greater than or equal to zero.
* Where pathLenConstraint is not present,
* there is no limit to the allowed length of the certification path.
* <p>
* Collection&lt; @link ObjectIdentifier &gt; PathLenConstraint ::= INTEGER (0..MAX)
*
* @see ASN1Primitive
* @see DERSequence
* @see DERTaggedObject
* @see org.bouncycastle.asn1.ASN1Object
* @see ASN1Object
*/
    public class PathLenConstraint
    {
        public List<ObjectIdentifier> PathLenConstraints { get; set; } = new();

        // TODO: Tem muitos metodos com esse mesmo comportamento, deveria refatorar?
        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                ObjectIdentifier oid = new();
                oid.Parse(value.ToAsn1Object());
                PathLenConstraints.Add(oid);
            }
        }
    }

    public enum CertTrustPointEnum
    {
        PathLenConstraint,
        AcceptablePolicySet,
        NameConstraint,
        PolicyConstraints
    }

}
