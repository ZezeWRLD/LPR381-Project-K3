using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPSolver
{
    internal class Algorithms
    {
        public void PSimplexMethod(LPmodel model) //class 2
        {
           
        //Primal Display
        public string PrintPrimal()
        {
            List<double[,]> result = PrimalSimplex();
            string line = "";
            foreach (var item in result)
            {
                for (int i = 0; i < item.GetLength(0); i++)
                {
                    for (int j = 0; j < item.GetLength(1); j++)
                    {
                        line += Math.Round(item[i, j], 4) + "\t";
                    }
                    line += "\n";
                }
                line += "\n";
            }

            return line;
        }
    }

}

        public void RPSimplexMethod(LPmodel model)
        {
            
        }

        public void BandBSimplexMethod(LPmodel model)
        {
            
        }

        public void CPlaneMethod(LPmodel model)
        {
            
        }

   public void BandBKnapsackMethod(LPmodel model)//class3
   {
   }

           

   









