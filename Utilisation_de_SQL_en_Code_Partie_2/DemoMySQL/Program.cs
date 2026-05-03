using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMySQL
{
    class Program
    {
        static void Main()
        {
            MySqlDemo monDemo = new MySqlDemo();

            
           monDemo.InitialiserConnexion();

            monDemo.VerifierConnexionBD();

            //monDemo.EffectuerSelect();

            //monDemo.EffectuerInsert();

            //monDemo.AppelerProcedureStockeeIN();

            //monDemo.AppelerProcedureStockeeINOUT();
        }
    }
}
