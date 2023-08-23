using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EstudoBouncyCastle
{
    public class AssinaturaDocumento
    {
        [JsonProperty("IdSolicitation")]
        public int SolicitacaoId { get; set; }
        [JsonProperty("DocumentType")]
        public TipoDocumento TipoDocumento { get; set; }
        [JsonProperty("SignatoryType")]
        public TipoSignatario TipoSignatario { get; set; }
        [JsonProperty("Signature")]
        public byte[]? Assinatura { get; set; }
        [JsonProperty("HashDocument")]
        public byte[]? HashDocumento { get; set; }
        [JsonProperty("InclusionDate")]
        public DateTime CriadoEm { get; set; }
    }

    public enum TipoSignatario
    {
        Agente = 1,
        Cliente,
        CmsFinal,
    }

    public enum TipoDocumento
    {
        TermoTitularidade = 3,
    }
}
