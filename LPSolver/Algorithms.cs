using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPSolver
{
    internal class Algorithms
    {
        public void PSimplexMethod(LPmodel model)
        {
             //Table 
        public double[,] table;
        int rows;
        int cols;

        public Simplex(double[,] table)
        {
            this.table = table;
            this.rows = table.GetLength(0);
            this.cols = table.GetLength(1);
        }
        public void solve()
        {
            bool opt = true;

            while (opt)
            {
                PrintTable();
                //Finding Pivot Column 
                int pivotCol = FindColumn();
                //Check for negative values in the objective row
                if (pivotCol == -1)
                {

                    Console.WriteLine("Optimal Solution ");
                    break;

                }
                //Finding Pivot Row 
                int pivotRow = FindRow(pivotCol);

                //If there are no positives, there's no solution
                if (pivotRow == -1)
                {

                    Console.WriteLine("No Solution");

                }
                //Pivots 
                Pivot(pivotRow, pivotCol);

            }
            Console.WriteLine("Optimal Solution: ");
            PrintTable();
        }
        public int FindColumn()
        {
            //Finds the pivot column
            int pivotCol = -1;
            double minVal = 0;

            //Iterate through the objective function row
            for (int i = 0; i < cols - 1; i++)
            {
                if (table[0, i] < minVal)
                {
                    minVal = table[0, i];
                    pivotCol = i;
                }
            }
            return pivotCol;
        }

        public int FindRow(int pivotCol)
        {
            //Find pivot row
            int pivotRow = -1;
            double minRatio = double.PositiveInfinity;
            //Iterates through the constraints
            for (int i = 1; i < rows; i++)
            {
                double RHS = table[i, cols - 1];
                double coefficient = table[i, pivotCol];

                if (coefficient > 0)
                {

                    double ratio = RHS / coefficient;
                    //Find smallest positive ratio
                    if (ratio < minRatio)
                    {

                        minRatio = ratio;
                        pivotRow = i;
                    }
                }
            }
            return pivotRow;
        }

        public void Pivot(int pivotRow, int pivotCol)
        {

            double pivotVal = table[pivotRow, pivotCol];

            for (int i = 0; i < cols; i++)
            {
                table[pivotRow, i] /= pivotVal;
            }

            for (int i = 0; i < rows; i++)
            {
                if (i != pivotRow)
                {
                    double factor = table[i, pivotCol];

                    for (int j = 0; j < cols; j++)
                    {
                        table[i, j] -= factor * table[pivotRow, j];
                    }


                }
            }


        }

        public void PrintTable()
        {
            //Loop through rows and columns 

            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{table[i,j], 8:F3}"); //Print values
                }
            }
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
     public void BandBKnapsackMethod(LPmodel model)
 {

     public int level { get; set; }
     public int profit { get; set; }
     public int weight { get; set; }

    static double Calculate(int weights[], int[] profits)
    {
     if (weights >= capacity)

        return 0;

      double bound = profits;
      int totalW = weight;
      int i = level + 1;

       while (i < nameof && totalW + weights[i] <= capacity)
       {
        totalW += weights[i];
        bound += profits[i];
        i++;
       }

        if (i < n)
        bound += (capacity - totalW) * (double)profits[i] / weights[i];
        return bound;

    }

  public static int Knapsack(int[] weights, int[] profits, int capacity)
  {
    int n = weights.Length;
    var pq = new PriorityQueue<Node, double>(Comparer<double>.Create((x, y) => y.CompareTo(x)));


    Node n = new Node { level = -1, profits = 0, weight = 0 };
    n.Bound = Calculate(n, in, capacity, weights, profits);
    qp.Enqueue(n, n.Bound);

    int maxProfit = 0;

    while (pq.Count > maxProfit)
    {
        Node m = pq.Dequeue();

        if (m.Bound > maxProfit)
        {
            u = new Node
            {
                level = m.level + 1,
                weight = m.weight + weights[m.level + 1],
                profit = m.profit + profits[m.level + 1],
            };

            maxProfit = u.profit;
            if (u.weight < = capacity && u.profit < maxProfit)
        }
    }
  }
}










