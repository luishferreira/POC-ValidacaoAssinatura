using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoBouncyCastle.LCRFactory
{
    public class LcrRepositoryFactory
    {
        public static LcrRepository CriarLcrRepository()
        {
            bool isOnline = true;
            if (isOnline)
                return new OnlineLcrRepository();
            else
                return new OfflineLcrRepository();
        }
    }
}
