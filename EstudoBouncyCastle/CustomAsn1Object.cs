using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    public class CustomAsn1Object
    {
        public static DerSequence GetDerSequence(Asn1Object derObject)
        {
            DerSequence sequence = null;

            if (derObject is DerTaggedObject derTaggedObject)
            {
                Asn1Object asn1Object = derTaggedObject.GetObject();
                if (asn1Object is DerSequence derSequence)
                {
                    sequence = derSequence;
                }
            }
            else if (derObject is DerSequence derSequence)
            {
                sequence = derSequence;
            }

            return sequence;
        }

        public static DerEnumerated GetDerEnumerated(Asn1Object derObject)
        {
            DerEnumerated enumerated = null;
            if (derObject is DerTaggedObject derTaggedObject)
            {
                Asn1Object asn1 = derTaggedObject.GetObject();
                if (asn1 is DerEnumerated derEnumerated)
                {
                    enumerated = derEnumerated;
                }
            }
            else if (derObject is DerEnumerated derEnumerated)
            {
                enumerated = derEnumerated;
            }
            return enumerated;
        }
    }
}
