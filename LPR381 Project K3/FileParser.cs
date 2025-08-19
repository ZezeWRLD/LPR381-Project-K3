using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project_K3
{
    internal class FileParser
    { 
        public LPmodel InputParser(string[] inputLines)
        {
            //Checking for complete LP Model
            if (inputLines == null || inputLines.Length < 3)
                throw new ArgumentException("Input must have an Objective function, atleast one constraint, and sign restrictions ");
            
            string optType ;
            List<double> objCoef = new List<double>();
            List<List<double>> constCoef = new List<List<double>>();
            List<string> constRel = new List<string>();
            List<double> constRHS = new List<double>();
            List<string> signRes = new List<string>();

            //Parsing objective function (first line)
            var objTokens = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            optType = objTokens[0].ToLower();
            if (optType != "max" && optType != "min")
                throw new ArgumentException("Objective function can only be 'min' or 'max' ");

            //Extracting objective coefficients
            for(int i = 1; i < objTokens.Length; i++)
            {
                string token = objTokens[i];
                if (token.Length < 2)
                    throw new ArgumentException($"Invalid objective coefficient format: {token}");

                char sign = token[0];
                if (sign != '+' && sign != '-')
                    throw new ArgumentException($"Invalid number in objective coefficient: {token}");

                if (!double.TryParse(token.Substring(1), out double coef))
                    throw new ArgumentException($"Invalid number in objective coefficient: {token}");

                objCoef.Add(sign == '+' ? coef : -coef);
            }

            // Parsing constraints except for 1st and last lines 
            for (int y = 1;  y < inputLines.Length -1; y++)
            {
                var constToken = inputLines[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (constToken.Length < objCoef.Count + 1)
                    throw new ArgumentException($"Constraint line {y} has too few tokens(varaibles)");

                var coef = new List<double>();
                for(int i = 0;i < objCoef.Count;i++)
                {
                    string token = constToken[i];
                    if (token.Length < 2)
                        throw new ArgumentException($"Invalid constraint format: {token} ");

                    char sign = token[0];
                    if (sign != '+' && sign != '-')
                        throw new ArgumentException($"Invalid number in constraint coefficient:{token}");

                    if (!double.TryParse(token.Substring(1),out double parsedCoef))
                        throw new ArgumentException($"Invalid number in constraint coefficient:{token}");

                    coef.Add(sign == '+' ? parsedCoef : -parsedCoef);
                }

                //Parse relation and rhs
                string relation = constToken[constToken.Length - 2];
                if(relation != "<=" && relation != ">=" && relation != "=")
                    throw new ArgumentException($"Invalid relation in constraint: {relation}");
                if (!double.TryParse(constToken[constToken.Length -1], out double rhs))
                    throw new ArgumentException($"Invalid rhs value: {constToken[constToken.Length - 1]}");
                
                constCoef.Add(coef);
                constRel.Add(relation);
                constRHS.Add(rhs);
                
                
            }

            //Parsing sign restrictions (last line)
            var restricToken = inputLines[inputLines.Length -1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (restricToken.Length != objCoef.Count)
                throw new ArgumentException($"Number of sign restrictions must match the number of variables.");
            
            foreach(var token in restricToken)
            {
                if (token != "bin" && token != "int" && token != "urs" && token != "+" &&  token !="-")
                    throw new ArgumentException($"Invalid sign restriction: {token}");
                    
                signRes.Add(token);
                
            }
          
            return new LPmodel(optType, objCoef, constCoef, constRel, constRHS, signRes);
   
        }

 
    }
}
