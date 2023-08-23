using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle
{
    [AttributeUsage(AttributeTargets.Field)]
    class PoliticasAttribute : Attribute
    {
        public string Oid { get; private set; }
        public string Url { get; private set; }
        internal PoliticasAttribute(string oid, string url)
        {
            Oid = oid;
            Url = url;
        }
    }

    public static class PolicyFactory
    {

        public static string GetOid(this Politicas politicas)
        {
            PoliticasAttribute atributo = GetAttr(politicas);
            return atributo.Oid;
        }

        public static string GetUrl(this Politicas politicas)
        {
            PoliticasAttribute atributo = GetAttr(politicas);
            return atributo.Url;
        }

        private static PoliticasAttribute GetAttr(Politicas p)
        {
            return (PoliticasAttribute)Attribute.GetCustomAttribute(ForValue(p), typeof(PoliticasAttribute));
        }

        private static MemberInfo ForValue(Politicas p)
        {
            return typeof(Politicas).GetField(Enum.GetName(typeof(Politicas), p));
        }

        public enum Politicas
        {
            #region CADES

            #region AD_RB

            [Politicas("2.16.76.1.7.1.1.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v1_1.der")]
            AD_RB_CADES_1_1,

            [Politicas("2.16.76.1.7.1.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RB.der")]
            AD_RB_CADES_1_0,

            [Politicas("2.16.76.1.7.1.1.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_1.der")]
            AD_RB_CADES_2_1,

            [Politicas("2.16.76.1.7.1.1.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_2.der")]
            AD_RB_CADES_2_2,

            [Politicas("2.16.76.1.7.1.1.2.3", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.der")]
            AD_RB_CADES_2_3,

            [Politicas("2.16.76.1.7.1.1.2", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_0.der")]
            AD_RB_CADES_2_0,

            #endregion

            #region AD_RT

            [Politicas("2.16.76.1.7.1.2.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v1_1.der")]
            AD_RT_CADES_1_1,
            
            [Politicas("2.16.76.1.7.1.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RT.der")]
            AD_RT_CADES_1_0,

            [Politicas("2.16.76.1.7.1.2.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_1.der")]
            AD_RT_CADES_2_1,

            [Politicas("2.16.76.1.7.1.2.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_2.der")]
            AD_RT_CADES_2_2,

            [Politicas("2.16.76.1.7.1.2.2.3", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_3.der")]
            AD_RT_CADES_2_3,
            
            [Politicas("2.16.76.1.7.1.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_0.der")]
            AD_RT_CADES_2_0,

            #endregion

            #region AD_RV

            [Politicas("2.16.76.1.7.1.3.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v1_1.der")]
            AD_RV_CADES_1_1,

            [Politicas("2.16.76.1.7.1.3.1", "http://politicas.icpbrasil.gov.br/PA_AD_RV.der")]
            AD_RV_CADES_1_0,

            [Politicas("2.16.76.1.7.1.3.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_1.der")]
            AD_RV_CADES_2_1,

            [Politicas("2.16.76.1.7.1.3.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_2.der")]
            AD_RV_CADES_2_2,

            [Politicas("2.16.76.1.7.1.3.2.3", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_3.der")]
            AD_RV_CADES_2_3,
            
            [Politicas("2.16.76.1.7.1.3.2", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_0.der")]
            AD_RV_CADES_2_0,

            #endregion

            #region AD_RC

            [Politicas("2.16.76.1.7.1.4.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v1_1.der")]
            AD_RC_CADES_1_1,

            [Politicas("2.16.76.1.7.1.4.1", "http://politicas.icpbrasil.gov.br/PA_AD_RC.der")]
            AD_RC_CADES_1_0,

            [Politicas("2.16.76.1.7.1.4.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_1.der")]
            AD_RC_CADES_2_1,

            [Politicas("2.16.76.1.7.1.4.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_2.der")]
            AD_RC_CADES_2_2,

            [Politicas("2.16.76.1.7.1.4.2.3", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_3.der")]
            AD_RC_CADES_2_3,

            [Politicas("2.16.76.1.7.1.4.2", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_0.der")]
            AD_RC_CADES_2_0,

            #endregion

            #region AD_RA

            [Politicas("2.16.76.1.7.1.5.1.1", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v1_1.der")]
            AD_RA_CADES_1_1,

            [Politicas("2.16.76.1.7.1.5.1.2", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v1_2.der")]
            AD_RA_CADES_1_2,

            [Politicas("2.16.76.1.7.1.5.1", "http://politicas.icpbrasil.gov.br/PA_AD_RA.der")]
            AD_RA_CADES_1_0,

            [Politicas("2.16.76.1.7.1.5.2.1", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_1.der")]
            AD_RA_CADES_2_1,

            [Politicas("2.16.76.1.7.1.5.2.2", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_2.der")]
            AD_RA_CADES_2_2,

            [Politicas("2.16.76.1.7.1.5.2.3", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_3.der")]
            AD_RA_CADES_2_3,

            [Politicas("2.16.76.1.7.1.5.2.4", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_4.der")]
            AD_RA_CADES_2_4,

            [Politicas("2.16.76.1.7.1.5.2", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_0.der")]
            AD_RA_CADES_2_0,

            #endregion

            #endregion

            #region XADES

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_1.xml")]
            AD_RB_XADES_2_1,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_2.xml")]
            AD_RB_XADES_2_2,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.xml")]
            AD_RB_XADES_2_3,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_4.xml")]
            AD_RB_XADES_2_4,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_1.xml")]
            AD_RT_XADES_2_1,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_2.xml")]
            AD_RT_XADES_2_2,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_3.xml")]
            AD_RT_XADES_2_3,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_4.xml")]
            AD_RT_XADES_2_4,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_2.xml")]
            AD_RV_XADES_2_2,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_3.xml")]
            AD_RV_XADES_2_3,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RV_v2_4.xml")]
            AD_RV_XADES_2_4,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_3.xml")]
            AD_RC_XADES_2_3,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RC_v2_4.xml")]
            AD_RC_XADES_2_4,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_3.xml")]
            AD_RA_XADES_2_3,

            [PoliticasAttribute("lorem ipsum dolor", "http://politicas.icpbrasil.gov.br/PA_AD_RA_v2_4.xml")]
            AD_RA_XADES_2_4,

            #endregion

            #region PADES

            #region AD_RB

            [Politicas("2.16.76.1.7.1.11.1.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_1.der")]
            AD_RB_PADES_1_1,

            [Politicas("2.16.76.1.7.1.11.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_0.der")]
            AD_RB_PADES_1_0,

            #endregion

            #region AD_RT

            [Politicas("2.16.76.1.7.1.12.1.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_1.der")]
            AD_RT_PADES_1_1,

            [Politicas("2.16.76.1.7.1.12.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_0.der")]
            AD_RT_PADES_1_0,

            #endregion

            #region AD_RC

            [Politicas("2.16.76.1.7.1.13.1.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RC_v1_1.der")]
            AD_RC_PADES_1_1,

            [Politicas("2.16.76.1.7.1.13.1.2", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RC_v1_2.der")]
            AD_RC_PADES_1_2,

            [Politicas("2.16.76.1.7.1.13.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RC_v1_0.der")]
            AD_RC_PADES_1_0,

            #endregion

            #region AD_RA

            [Politicas("2.16.76.1.7.1.14.1.1", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RA_v1_1.der")]
            AD_RA_PADES_1_1,

            [Politicas("2.16.76.1.7.1.14.1.2", "http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RA_v1_2.der")]
            AD_RA_PADES_1_2,

            #endregion

            #endregion
        }
    }

}
