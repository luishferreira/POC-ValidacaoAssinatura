
using Org.BouncyCastle.X509;
using System.Net;

namespace EstudoBouncyCastle.LCRFactory
{
    public class OnlineLcrRepository : LcrRepository
    {
        public override async Task<List<LCR>> GetX509LCR(X509Certificate certificado)
        {
            List<LCR> retorno = new();

            Certificado cert = new(certificado);

            List<string> listaUrlLcr = cert.GetLcrDistributionPoint();

            if (listaUrlLcr == default || listaUrlLcr.Count == 0)
                Console.WriteLine("erro");

            foreach (string urlLcr in listaUrlLcr)
            {
                if (await ObterLcr(urlLcr) is LCR lcr)
                {
                    retorno.Add(lcr);
                }
            }

            return retorno;
        }

        private async Task<LCR> ObterLcr(string urlLcr)
        {
            HttpClient http = new();
            LCR lcr;
            try
            {
                byte[] conteudoLcr = await http.GetByteArrayAsync(urlLcr);

                lcr = new(conteudoLcr);
            }
            catch (Exception)
            {
                return null;
                urlLcr = urlLcr.Replace("http://", "https://");

                byte[] conteudoLcr = await http.GetByteArrayAsync(urlLcr);

                lcr = new(conteudoLcr);
            }

            return lcr;
        }

    }
}
