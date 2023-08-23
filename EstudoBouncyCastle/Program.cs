using EstudoBouncyCastle;
using EstudoBouncyCastle.LCRFactory;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.X509;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.WebRequestMethods;
using Attribute = Org.BouncyCastle.Asn1.Cms.Attribute;

var base64 = "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";

//List<AssinaturaDocumento> assinaturas = await "https://acsafeweb.safewebpss.com.br/Service/Microservice/Shared/Signature"
//    //.ConfigureRequest(c => c.JsonSerializer = new SerializadorJsonFlurl())
//    .AppendPathSegment($"/api/signature/getlist/1004515689")
//    .GetJsonAsync<List<AssinaturaDocumento>>();

//byte[] bytes = ReadContent("C:\\Users\\luis.ferreira\\Downloads\\pdfTesteAssinatura-raw_assinado.pdf");
//var asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(assinaturas[0].Assinatura);k

#region Primeira tentativa de validação

PoliticaAssinatura signaturePolicy = null;
string policyName = null;


byte[] bytes = Convert.FromBase64String(base64);

CmsSignedData cms = new(bytes);

var certificados = cms.GetCertificates();
var assinadoresInfo = cms.GetSignerInfos();
var assinadores = assinadoresInfo.GetSigners();

InformacoesAssinatura informacoes = new();

SignerInformation signerInfo = assinadores.First();

SignerInformationStore signerInfoStore = signerInfo.GetCounterSignatures();

var certCollection = certificados.EnumerateMatches(signerInfo.SignerID);

Org.BouncyCastle.X509.X509Certificate certificadoBC = certCollection.FirstOrDefault();

X509Certificate2 certificado = new(certificadoBC.GetEncoded());

await ValidarRevogacaoCertificado(certificadoBC);

informacoes.DataLimite = await ValidarPeriodoCertificado(certificadoBC);

try
{
    signerInfo.Verify(certificadoBC);
}
catch (Exception ex)
{
    throw;
}

Console.WriteLine("Começar a buscar os atributos assinados");

string oidPolitica = PkcsObjectIdentifiers.IdAAEtsSigPolicyID.Id;


AttributeTable signedAttributes = signerInfo.SignedAttributes;

if ((signedAttributes == null) || (signedAttributes != null && signedAttributes.Count == 0))
    Console.WriteLine("erro");



Attribute idSigningPolicy = signedAttributes[PkcsObjectIdentifiers.IdAAEtsSigPolicyID];

if (idSigningPolicy == default)
    Console.WriteLine("aaa");

foreach (var atributos in idSigningPolicy.AttrValues)
{
    string policyOnSignature = atributos.ToString();

    foreach (PolicyFactory.Politicas item in Enum.GetValues(typeof(PolicyFactory.Politicas)))
    {
        if (policyOnSignature.Contains(item.GetOid()))
        {
            await SalvarPolitica(item);
            break;
        }
    }
}

DateTime? dataHora = null;

if (signedAttributes == null)
{
    Console.WriteLine("hahaha");
}
else
{
    //Valida o atributo ContentType
    Attribute attributeContentType = signedAttributes[CmsAttributes.ContentType];
    if (attributeContentType == null)
        throw new Exception("erro pkcs7 attribute not found content type");
    if (!attributeContentType.AttrValues[0].Equals(CmsObjectIdentifiers.Data))
    {
        throw new Exception("content not data");
    }

    //Validando o atributo MessageDigest
    Attribute attrributeMessageDigest = signedAttributes[CmsAttributes.MessageDigest];
    if (attrributeMessageDigest == null)
    {
        throw new Exception("attribute not found messagedigest");
    }

    //Mostra a data e hora da assinatura (não é carimbo de tempo)
    Attribute timeAttribute = signedAttributes[CmsAttributes.SigningTime];

    if (timeAttribute != null)
    {
        //data deveria ser em qual timezone? assinador do demoiselle retorna 10:10, aqui ta retornando 13:10, um UTC o outro -3
        Asn1Set attributeValues = timeAttribute.AttrValues;
        Asn1UtcTime asn1UtcTime = Asn1UtcTime.GetInstance(attributeValues[0]);
        dataHora = asn1UtcTime.ToDateTime();
    }
}

if (signaturePolicy == null)
{
    Console.WriteLine("politica nao encontrada");
    throw new Exception("politica nao encontrada");
}
else
{
    if (signaturePolicy.InformacoesPoliticaAssinatura?.SignatureValidationPolicy?.CommonRules?.SignerAndVerifierRules?.SignerRules?.MandatedSignedAttr?.ObjectIdentifiers != null)
    {
        foreach (ObjectIdentifier obbjectIdentifier in signaturePolicy.InformacoesPoliticaAssinatura?.SignatureValidationPolicy?.CommonRules?.SignerAndVerifierRules?.SignerRules?.MandatedSignedAttr?.ObjectIdentifiers)
        {
            string oid = obbjectIdentifier.Value;
            Attribute signedAttr = signedAttributes[new DerObjectIdentifier(oid)];
            if (signedAttr == null)
                Console.WriteLine("Mandated signed attribute não encontrado na assinatura");
        }
    }
}

//Recupera os atributos NÃO assinados

AttributeTable unsignedAttributes = signerInfo.UnsignedAttributes;

if (unsignedAttributes == null || unsignedAttributes.Count == 0)
{
    Console.WriteLine("unsigned attributes not found (ad-rb não tem atributos não assinados)");
}

//Validando os atributos conforme a politica
if (signaturePolicy.InformacoesPoliticaAssinatura?.SignatureValidationPolicy?.CommonRules?.SignerAndVerifierRules?.SignerRules?.MandatedUnsignedAttr?.ObjectIdentifiers != null)
{
    foreach (ObjectIdentifier obbjectIdentifier in signaturePolicy.InformacoesPoliticaAssinatura?.SignatureValidationPolicy?.CommonRules?.SignerAndVerifierRules?.SignerRules?.MandatedUnsignedAttr?.ObjectIdentifiers)
    {
        string oid = obbjectIdentifier.Value;
        Attribute unsignedAttr = unsignedAttributes[new DerObjectIdentifier(oid)];
        if (unsignedAttr == null)
        {
            Console.WriteLine("mandated unsigned attribute não encontrado");
        }
        if (oid.Equals(PkcsObjectIdentifiers.IdAASignatureTimeStampToken.Id))
        {
            //Verificando timestamp
            try
            {
                byte[] varSignature = signerInfo.GetSignature();
                Timestamp timeStampSigner = await ValidateTimestamp(unsignedAttr, varSignature);
                informacoes.TimeStampSigner = timeStampSigner;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


LinkedList<X509Certificate2> varChain = CAManager.Instance.GetCertificateChain(certificadoBC, certificado);

//Menor que 2 = autoAssinado (gerado pela propria empresa)
if (varChain.Count < 2)
{
    Console.WriteLine("autoAssinado");
}

foreach (X509Certificate2 cert in varChain)
{
    Certificado signerCertificate = new(cert);
    if (!signerCertificate.IsCACertificate())
    {
        informacoes.CertificadoIcpBrasil = signerCertificate;
    }
}

informacoes.DataAssinatura = dataHora;
informacoes.Cadeia = varChain;
informacoes.PoliticaAssinatura = signaturePolicy;

#endregion

#region Métodos Internos

async Task<Timestamp> ValidateTimestamp(Attribute attributeTimeStamp, byte[] varSignature)
{
    
    return new Timestamp();
}

async Task<bool> ValidarRevogacaoCertificado(Org.BouncyCastle.X509.X509Certificate x509)
{
    var lcrRepository = LcrRepositoryFactory.CriarLcrRepository();

    if (x509 == null)
        Console.WriteLine("erro");

    var lcrs = await lcrRepository.GetX509LCR(x509);

    if (lcrs == null || lcrs.Count == 0)
    {
        Console.WriteLine("erro");
    }

    foreach (LCR lcr in lcrs)
    {
        if (lcr.Lcr.IsRevoked(x509))
        {
            Console.WriteLine("revogado");
        }
    }

    return true;

}

async Task<DateTime> ValidarPeriodoCertificado(Org.BouncyCastle.X509.X509Certificate x509)
{
    x509.CheckValidity();
    return x509.NotAfter;
}

async Task<bool> SalvarPolitica(PolicyFactory.Politicas politica)
{
    policyName = Enum.GetName(politica);
    PoliticaAssinatura politicaAssinatura = await CarregarPolitica(politica);
    signaturePolicy = politicaAssinatura;
    return true;
}

async Task<PoliticaAssinatura> CarregarPolitica(PolicyFactory.Politicas politica)
{
    Asn1Object asn1Politica = await ObterPolitica(politica.GetUrl());

    PoliticaAssinatura politicaAssinatura = new();

    politicaAssinatura.Parse(asn1Politica);
    politicaAssinatura.UrlPoliticaAssinatura = politica.GetUrl();

    return politicaAssinatura;
}

static async Task<Asn1Object> ObterPolitica(string url)
{
    HttpClient http = new();
    byte[] politicaByte = await http.GetByteArrayAsync(url);
    Asn1Object politica = Asn1Object.FromByteArray(politicaByte);

    return politica;
}


#endregion

Console.Read();

