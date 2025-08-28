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

}
