using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    /**
* The CommitmentRules consists of the validation rules which
* apply to given commitment types:
*
* <pre>
* CommitmentRules ::= SEQUENCE OF CommitmentRule
* </pre>
*
* @see CommitmentRule
* @see ASN1Object
* @see ASN1Sequence
*/
    public class CommitmentRules
    {
        public List<CommitmentRule> CommitmentRulesList { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            int total = derSequence.Count;

            foreach (Asn1Encodable obj in derSequence)
            {
                CommitmentRule commitmentRule = new();
                commitmentRule.Parse(obj.ToAsn1Object());
                CommitmentRulesList.Add(commitmentRule);
            }
        }
    }

    /**
* The CommitmentRule for given commitment types are defined in terms
* of trust conditions for certificates, timestamps and attributes,
* along with any constraints on attributes that may be included in
* the electronic signature.
*
* @see ASN1Primitive
* @see ASN1Sequence
* @see ASN1Object
* @see SelectedCommitmentTypes
*/
    public class CommitmentRule : CommonRules
    {
        public SelectedCommitmentTypes SelCommitmentTypes { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            base.Parse(derObject);
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);
            SelCommitmentTypes.Parse(derSequence[0].ToAsn1Object());
        }
    }

    /**
* If the SelectedCommitmentTypes indicates "empty" then this rule applied when a commitment type
* is not present(i.e. the type of commitment is indicated in the semantics of the message).
* Otherwise, the electronic signature shall contain a commitment type indication
* that shall fit one of the commitments types that are mentioned in CommitmentType
* <pre>
* SelectedCommitmentTypes ::= SEQUENCE OF CHOICE {
*   empty NULL,
*   recognizedCommitmentType {@link CommitmentType}
* }
* </pre>
*
* @see ASN1Primitive
* @see ASN1Sequence
* @see DERNull
* @see DERSequence
*/

    public class SelectedCommitmentTypes
    {
        public CommitmentType RecognizedCommitmentType { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);
            Asn1Object asn1 = derSequence[0].ToAsn1Object();

            if (asn1 is DerNull)
            {
                RecognizedCommitmentType = null;
            }
            else if (asn1 is DerSequence)
            {
                RecognizedCommitmentType = new();
                RecognizedCommitmentType.Parse(asn1);
            }
        }
    }

    /**
* A specific commitment type identifier shall not appear in more than one commitment rule.
* <p>
* CommitmentType ::= SEQUENCE {
* identifier  {@link CommitmentTypeIdentifier},
* fieldOfApplication [0]  {@link FieldOfApplication} OPTIONAL,
* semantics [1] DirectoryString OPTIONAL }
* <p>
* The fieldOfApplication and semantics fields define the specific use
* and meaning of the commitment within the overall field of application defined for the policy.
*
* @see ASN1Object
* @see ASN1Primitive
* @see ASN1Sequence
* @see DERTaggedObject
*/
    public class CommitmentType
    {
        public CommitmentTypeIdentifier Identifier { get; set; } = new();
        public FieldOfApplication FieldOfApplication { get; set; }
        public string Semantics { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            Identifier.Parse(derSequence[0].ToAsn1Object());

            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();
                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    //Field of Application
                    if (derTaggedObject.TagNo == 0)
                    {
                        FieldOfApplication = new();
                        FieldOfApplication.Parse(asn1);
                    }
                }
            }
        }
    }

    public class CommitmentTypeIdentifier
    {
        public void Parse(Asn1Object derObject)
        {
            Console.WriteLine("not implemented");
        }
    }
}
