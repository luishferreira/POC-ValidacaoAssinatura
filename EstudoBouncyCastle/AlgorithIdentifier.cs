using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{

    public class ObjectIdentifier
    {
        public string Value;

        public void Parse(Asn1Object derObject)
        {
            DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)derObject;
            Value = derObjectIdentifier.ToString();
        }
    }

    public class AlgorithmIdentifier
    {
        private ObjectIdentifier Algorithm;
        private object Parameters;

        public void Parse(Asn1Object derObject)
        {
            Algorithm = new();
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);
            Algorithm.Parse(derSequence[0].ToAsn1Object());
        }
    }

    public class GeneralizedTime
    {
        public DateTime Date { get; set; }

        public void Parse(Asn1Object derObject)
        {
            if (derObject is Asn1GeneralizedTime derGeneralizedTime)
            {
                try
                {
                    Date = derGeneralizedTime.ToDateTime();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }

    public class PolicyIssuerName
    {
        public Dictionary<ObjectIdentifier, string> IssuerNames { get; set; }
        public string IssuerName { get; set; }

        public void Parse(Asn1Object primitive)
        {
            if (primitive is DerSequence sequence)
            {
                Asn1Encodable asn1Encodable = sequence[0];
                if (asn1Encodable is DerTaggedObject derTaggedObject)
                {
                    Asn1Object asn1 = derTaggedObject.GetObject();
                    if (asn1 is DerOctetString)
                    {
                        OctetString octetString = new();
                        octetString.Parse(asn1);
                        IssuerName = octetString.GetValueUTF8();
                    }
                    else if (asn1 is DerSequence derSequence)
                    {
                        foreach (Asn1Encodable obj in derSequence)
                        {
                            if (obj is DerSet derSet)
                            {
                                Asn1Encodable obj2 = derSet[0];
                                if (obj2 is DerSequence derSequence2)
                                {
                                    ObjectIdentifier oid = new();
                                    oid.Parse(derSequence2[0].ToAsn1Object());
                                    string name = null;
                                    Asn1Encodable obj3 = derSequence2[1];
                                    if (obj3 is DerPrintableString derPrintableString)
                                    {
                                        name = derPrintableString.GetString();
                                    }
                                    else if (obj3 is DerUtf8String derUtf8String)
                                    {
                                        name = derUtf8String.GetString();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not recognized object");
                                    }
                                    if (IssuerNames == null)
                                    {
                                        IssuerNames = new();
                                    }
                                    IssuerNames.Add(oid, name);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            if (IssuerName != null)
            {
                return IssuerName;
            }

            string result = "";

            if (IssuerNames != null && IssuerNames.Count > 0)
            {
                foreach (ObjectIdentifier oid in IssuerNames.Keys)
                {
                    result = result + oid.Value + '=' + IssuerNames[oid] + ',';
                }
                return result.Substring(0, result.Length - 1);
            }

            return null;
        }
    }

    public class FieldOfApplication
    {
        public string Value { get; set; }

        public void Parse(Asn1Object derObject)
        {
            if (derObject is DerUtf8String derUtf8)
            {
                Value = derUtf8.GetString();
            }
            else
            {
                Value = derObject.ToString();
            }
        }
    }

    public class SigningPeriod : CustomAsn1Object
    {
        public GeneralizedTime NotBefore { get; set; } = new();
        public GeneralizedTime NotAfter { get; set; }

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = GetDerSequence(derObject);

            NotBefore.Parse(derSequence[0].ToAsn1Object());

            if (derSequence.Count == 2)
            {
                NotAfter = new();
                NotAfter.Parse(derSequence[1].ToAsn1Object());
            }
        }
    }

    public class SignPolExtensions
    {
        List<SignPolExtn> Extensions { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            for (int i = 0; i < derSequence.Count; i++)
            {
                SignPolExtn signPolExtn = new();
                signPolExtn.Parse(derSequence[i].ToAsn1Object());
                Extensions.Add(signPolExtn);
            }
        }
    }

    public class SignPolExtn
    {
        public ObjectIdentifier ExtnID { get; set; } = new();
        public OctetString ExtnValue { get; set; } = new();

        public void Parse(Asn1Object derObject)
        {
            DerSequence derSequence = CustomAsn1Object.GetDerSequence(derObject);

            ExtnID.Parse(derSequence[0].ToAsn1Object());
            ExtnValue.Parse(derSequence[1].ToAsn1Object());
        }
    }

    public class OctetString
    {
        public string Value { get; set; }

        public DerOctetString DerOctetString { get; set; }

        public string GetValueUTF8()
        {
            string result;
            try
            {
                result = Encoding.UTF8.GetString(DerOctetString.GetOctets());
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void Parse(Asn1Object derObject)
        {
            if (derObject is DerOctetString derOctet)
            {
                DerOctetString = derOctet;
                string octetString = derOctet.ToString()[1..];
                Value = octetString;
            }
        }
    }

}
