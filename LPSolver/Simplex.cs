using System;

public class Simplex
{
    //PRIMAL SIMPLEX
    public List<double[,]> PrimalSimplex()
    {
        List<double[,]> tables = new List<double[,]>();

        tables.Add(InitialTable);
        bool simplexOptimal = false;

        do
        {
            double[,] table = tables[tables.Count - 1];

            int rows = table.GetLength(0);
            int columns = table.GetLength(1);

            int pivotRowIndex = -1;

            double pivotCol = 1000000;
            int pivotColIndex = -1;

            // Find the minimum value in the last column
            double minValue = 0;
            for (int i = 1; i < rows; i++)
            {
                double currentValue = table[i, columns - 1];

                if (double.IsNegative(currentValue) && currentValue < minValue)
                {
                    pivotRowIndex = i;
                    minValue = currentValue;
                }
            }

            //No negative in the rhs
            if (minValue >= 0)
            {
                //Pivot selection for Max
                if (ProblemType == "max")
                {
                    minValue = 0;
                    for (int i = 0; i < table.GetLength(1) - 1; i++)
                    {
                        double currentValue = table[0, i];
                        if (double.IsNegative(currentValue) && currentValue < minValue)
                        {
                            pivotColIndex = i;
                            minValue = currentValue;
                        }
                    }

                }
                //Pivot selection for Min
                else
                {
                    minValue = 0;
                    for (int i = 0; i < table.GetLength(1) - 1; i++)
                    {
                        double currentValue = table[0, i];
                        if (!double.IsNegative(currentValue) && currentValue > minValue)
                        {
                            pivotColIndex = i;
                            minValue = currentValue;
                        }
                    }
                }

                if (pivotColIndex == -1)
                {
                    simplexOptimal = true;
                    break;
                }

                //pivot row selection
                minValue = 100000;
                for (int i = 1; i < table.GetLength(0); i++)
                {
                    double currentValue = table[i, table.GetLength(1) - 1] / table[i, pivotColIndex];

                    if (currentValue < minValue && !double.IsNegative(currentValue) && !double.IsNaN(currentValue))
                    {
                        pivotRowIndex = i;
                        minValue = currentValue;
                    }
                }

                if (pivotRowIndex == -1)
                {
                    simplexOptimal = true;
                    break;
                }

                //Adds table to solution
                tables.Add(PivotTable(table, pivotColIndex, pivotRowIndex));
            }
            //Negative was found in the rhs following dual
            else
            {

                tables.Add(new double[table.GetLength(0), table.GetLength(1)]);
                break;
            }

        } while (!simplexOptimal);

        return tables;
    }

    //Revised
    public static RevisedSimplex()
    {
        var (A, b, c, sense, varNames, basicIdx, nonBasicIdx, hasArtificial) =
            Canonicalizer.ToStandardForm(rawModel);

        if (hasArtificial)
        {
            var phase1 = RunRevisedPhaseI(A, b, c, varNames, ref basicIdx, ref nonBasicIdx);
            if (phase1.Status != SolveStatus.Optimal || Math.Round(phase1.ObjectiveValue, 6) != 0)
            {
                phase1.Status = SolveStatus.Infeasible;
                return phase1;
            }

            (A, b, c, _, varNames, basicIdx, nonBasicIdx, _) =
                Canonicalizer.ToStandardForm(rawModel, assumeFeasible: true);
        }

        return RunRevisedPhaseII(A, b, c, varNames, ref basicIdx, ref nonBasicIdx, sense);
    }

    private static SimplexResult RunRevisedPhaseI(double[,] A, double[] b, double[] c,
        List<string> varNames, ref List<int> basicIdx, ref List<int> nonBasicIdx)
    {
        var rs = new RevisedModel(A, b, c, varNames, basicIdx, nonBasicIdx, phase: 1);
        return RevisedLoop(rs, ref basicIdx, ref nonBasicIdx, phase: 1);
    }

    private static SimplexResult RunRevisedPhaseII(double[,] A, double[] b, double[] c,
        List<string> varNames, ref List<int> basicIdx, ref List<int> nonBasicIdx, int sense)
    {
        var rs = new RevisedModel(A, b, c, varNames, basicIdx, nonBasicIdx, phase: 2, sense: sense);
        return RevisedLoop(rs, ref basicIdx, ref nonBasicIdx, phase: 2);
    }

    private static SimplexResult RevisedLoop(RevisedModel rs, ref List<int> basicIdx, ref List<int> nonBasicIdx, int phase)
    {
        var res = new SimplexResult();
        int iter = 0;

        while (iter++ < ITER_LIMIT)
        {
            rs.AppendIterationTo(res.Iterations);

            var (enterIdx, enterCol) = rs.SelectEntering();
            if (enterIdx == -1 || enterCol == null)
            {
                double z = rs.CurrentObjective();
                if (phase == 1 && Math.Round(z, 6) != 0) { res.Status = SolveStatus.Infeasible; return res; }

                res.Status = SolveStatus.Optimal;
                res.ObjectiveValue = z;
                res.PrimalSolution = rs.CurrentPrimal();
                res.DualPrices = rs.CurrentDual();
                basicIdx = rs.BasicIdx;
                nonBasicIdx = rs.NonBasicIdx;
                return res;
            }

            double[] d = rs.Direction(enterCol);
            int leavePos = rs.SelectLeaving(d);
            if (leavePos == -1)
            {
                res.Status = SolveStatus.Unbounded;
                return res;
            }

            rs.Pivot(leavePos, enterIdx, d);
        }

        res.Status = SolveStatus.IterationLimit;
        return res;
    }
}



