using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.CommonRulesFolder
{
    /**
* The TimeStampTrustCondition field identifies trust conditions for
* certificate path processing used to authenticate the timstamping
* authority and constraints on the name of the timestamping authority.
* This applies to the timestamp that shall be present in every ES-T.
*
* <pre>
*     TimestampTrustCondition ::= SEQUENCE {
*     ttsCertificateTrustTrees [0] {@link CertificateTrustTrees} OPTIONAL,
*     ttsRevReq [1] {@link CertRevReq} OPTIONAL,
*     ttsNameConstraints [2] {@link NameConstraints} OPTIONAL,
*     cautionPeriod [3] {@link DeltaTime} OPTIONAL,
*     signatureTimestampDelay [4] {@link DeltaTime} OPTIONAL
*     }
* </pre>
*/
    public class TimestampTrustCondition
    {
        public CertificateTrustTrees TtsCertificateTrustTrees { get; set; }
        public CertRevReq TtsRevReq { get; set; }
        public DeltaTime CautionPeriod { get; set; }
        public DeltaTime SignatureTimestampDelay { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            foreach (Asn1Encodable value in derSequence)
            {
                Asn1Object asn1 = value.ToAsn1Object();

                if (asn1 is DerTaggedObject derTaggedObject)
                {
                    TimestampEnum ttsEnum = (TimestampEnum)derTaggedObject.TagNo;

                    switch (ttsEnum)
                    {
                        case TimestampEnum.TtsCertificateTrustTrees:
                            TtsCertificateTrustTrees = new();
                            TtsCertificateTrustTrees.Parse(asn1);
                            break;
                        case TimestampEnum.TtsRevReq:
                            TtsRevReq = new();
                            TtsRevReq.Parse(asn1);
                            break;
                        case TimestampEnum.TtsNameConstraints:
                            Console.WriteLine("not implemented");
                            break;
                        case TimestampEnum.CautionPeriod:
                            CautionPeriod = new();
                            CautionPeriod.Parse();
                            break;
                        case TimestampEnum.SignatureTimestampDelay:
                            SignatureTimestampDelay = new();
                            SignatureTimestampDelay.Parse();
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }

    public class DeltaTime
    {
        public int DeltaSeconds { get; set; }
        public int DeltaMinutes { get; set; }
        public int DeltaHours { get; set; }
        public int DeltaDays { get; set; }

        public void Parse()
        {
            Console.WriteLine("not implemented");
        }
    }

    public enum TimestampEnum
    {
        TtsCertificateTrustTrees,
        TtsRevReq,
        TtsNameConstraints,
        CautionPeriod,
        SignatureTimestampDelay,
    }
}
