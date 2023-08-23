using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.CommonRulesFolder
{
    /**
* The algorithmConstrains fields, if present, identifies the signing algorithms
* (hash, public key cryptography, combined hash and public key cryptography)
* that may be used for specific purposes and any minimum length. If this field
* is not present then the policy applies no constraints.
* <p>
* AlgorithmConstraintSet ::= SEQUENCE { -- Algorithm constraints on:
* <p>
* signerAlgorithmConstraints [0] {@link AlgorithmConstraints} OPTIONAL, -- signer
* eeCertAlgorithmConstraints [1] {@link AlgorithmConstraints} OPTIONAL, -- issuer of end entity certs
* caCertAlgorithmConstraints [2] {@link AlgorithmConstraints} OPTIONAL, -- issuer of CA certificates
* aaCertAlgorithmConstraints [3] {@link AlgorithmConstraints} OPTIONAL, -- Attribute Authority
* tsaCertAlgorithmConstraints [4]{@link AlgorithmConstraints} OPTIONAL -- TimeStamping Authority -- }
*/
    public class AlgorithmConstraintSet
    {
        public AlgorithmConstraints Signer { get; set; }
        public AlgorithmConstraints EeCert { get; set; }
        public AlgorithmConstraints CaCert { get; set; }
        public AlgorithmConstraints AaCert { get; set; }
        public AlgorithmConstraints TsaCert { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);
            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();
                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    AlgorithmConstraintSetEnum algEnum = (AlgorithmConstraintSetEnum)derTaggedObject.TagNo;

                    switch (algEnum)
                    {
                        case AlgorithmConstraintSetEnum.Signer:
                            Signer = new();
                            Signer.Parse(asn1);
                            break;
                        case AlgorithmConstraintSetEnum.EeCert:
                            EeCert = new();
                            EeCert.Parse(asn1);
                            break;
                        case AlgorithmConstraintSetEnum.CaCert:
                            CaCert = new();
                            CaCert.Parse(asn1);
                            break;
                        case AlgorithmConstraintSetEnum.AaCert:
                            AaCert = new();
                            AaCert.Parse(asn1);
                            break;
                        case AlgorithmConstraintSetEnum.TsaCert:
                            TsaCert = new();
                            TsaCert.Parse(asn1);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public class AlgorithmConstraints
    {
        public List<AlgAndLength> AlgAndLengths { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                AlgAndLength algAndLength = new();
                algAndLength.Parse(value.ToAsn1Object());
                AlgAndLengths.Add(algAndLength);
            }
        }
    }

    public class AlgAndLength
    {
        public ObjectIdentifier AlgID { get; set; } = new();
        public int MinKeyLength { get; set; }
        public SignPolExtensions Other { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            AlgID.Parse(derSequence[0].ToAsn1Object());

            if (derSequence.Count >= 2)
            {
                MinKeyLength = ((DerInteger)derSequence[1].ToAsn1Object()).Value.IntValue;
            }
            if (derSequence.Count == 3)
            {
                Other = new();
                Other.Parse(derSequence[2].ToAsn1Object());
            }
        }
    }

    public enum AlgorithmConstraintSetEnum
    {
        Signer,
        EeCert,
        CaCert,
        AaCert,
        TsaCert
    }
}
