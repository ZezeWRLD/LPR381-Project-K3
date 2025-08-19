using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project_K3
{
    internal class LPmodel
    {
        private string optimizationType;
        private List<double> objectiveCoef;
        private List<List<double>> constraintCoef;
        private List<string> constraintRel;
        private List<double> constraintRHS;
        private List<string> signRes;

        public LPmodel(string optimizationType, List<double> objectiveCoef, List<List<double>> constraintCoef, List<string> constraintRel, List<double> constraintRHS, List<string> signRes)
        {
            this.optimizationType = optimizationType;
            this.objectiveCoef = objectiveCoef;
            this.constraintCoef = constraintCoef;
            this.constraintRel = constraintRel;
            this.constraintRHS = constraintRHS;
            this.signRes = signRes;
        }

        public string OptimizationType { get { return optimizationType; } set { optimizationType = value; } }
        public List<double> ObjectiveCoef { get { return objectiveCoef; } set { objectiveCoef = value; } }
        public List<List<double>> ConstraintCoef { get { return constraintCoef; } set { constraintCoef = value; } }
        public List<string> ConstraintRel { get { return constraintRel; } set { constraintRel = value; } }
        public List<double> ConstraintRHS { get { return constraintRHS; } set { constraintRHS = value; } }
        public List<string> SignRes { get { return signRes; } set { signRes = value; } }

    }
}
