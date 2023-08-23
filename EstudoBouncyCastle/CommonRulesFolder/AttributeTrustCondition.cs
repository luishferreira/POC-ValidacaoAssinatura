using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.CommonRulesFolder
{
    /**
* If the attributeTrustCondition field is not present then any
* certified attributes may not be considered to be valid under
* this validation policy.
*
* <pre>
* AttributeTrustCondition ::= SEQUENCE {*
*     attributeMandated BOOLEAN, -- Attribute shall be present
*     HowCertAttribute
*     CertificateTrustTrees OPTIONAL
*     CertRevReq OPTIONAL
*     AttributeConstraints OPTIONAL
* }
* </pre>
*/
    public class AttributeTrustCondition
    {
        public bool AttributeMandated { get; set; }

        public HowCertAttribute HowCertAttribute { get; set; }

        public CertificateTrustTrees AttrCertificateTrustTrees { get; set; }

        public CertRevReq AttrRevReq { get; set; }

        public AttributeConstraints AttributeConstraints { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();
                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    AttributeTrustConditionEnum attrEnum = (AttributeTrustConditionEnum)derTaggedObject.TagNo;

                    switch (attrEnum)
                    {
                        case AttributeTrustConditionEnum.AttrCertificateTrustTrees:
                            AttrCertificateTrustTrees = new();
                            AttrCertificateTrustTrees.Parse(asn1);
                            break;
                        case AttributeTrustConditionEnum.AttrRevReq:
                            AttrRevReq = new();
                            AttrRevReq.Parse(asn1);
                            break;
                        case AttributeTrustConditionEnum.AttributeConstraints:
                            AttributeConstraints = new();
                            AttributeConstraints.Parse();
                            break;
                        default: break;
                    }
                }
            }
        }
    }

    public class AttributeConstraints
    {
        //TODO: Não tem no demoiselle
        public void Parse()
        {
            Console.WriteLine("not implemented");
        }
    }

    public enum AttributeTrustConditionEnum
    {
        AttrCertificateTrustTrees,
        AttrRevReq,
        AttributeConstraints
    }

    public enum HowCertAttribute
    {
        ClaimedAttribute,
        CertifiedAttributes,
        Either
    }
}
