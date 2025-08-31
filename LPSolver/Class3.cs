using System;

public class Knapsack
{
    public class Node
    {
        public int profit { get; set; }// Profit accumulated
        public int bound { get; set; }//Upper bound
        public int level { get; set; } //Item considered
        public int weight { get; set; }//Total acummulated

    }
    //Branch & Bound Algorithn
    public class KnapsackSolver
    {
        //calculates upper bound 
        static int calculateBound(Node node, int itemCount, int capacity, int[] weights, int[] profits)
        {

            //If weight's greater,no additional profit is possible
            if (node.weight >= capacity)
            {
                return 0;
            }

            int profitBound = node.profit;
            int totalWeight = node.weight;
            int i = node.level + 1;
            //Add items within capacity
            while (i < itemCount && totalWeight + weights[i] <= capacity)
            {
                totalWeight += weights[i];
                profitBound += profits[i];
                i++;
            }
            //Add fractional item
            if (i < itemCount)
            {
                profitBound += (capacity - totalWeight) * profits[i] / weights[i];
            }

            return profitBound;
        }
        //Main Branch & Bound
        public static int Knapsack(int[] weights, int[] profits, int capacity)
        {
            int itemCount = weights.Length;
            var pq = new PriorityQueue<Node, int>();

            // Starting point
            Node root = new Node { level = -1, profit = 0, weight = 0 };
            root.bound = CalculateBound(root, itemCount, capacity, weights, profits);
            pq.Enqueue(root, -root.bound); // negate to simulate max-heap

            int maxProfit = 0;
            //Goes through nodes while collection isn't empty
            while (pq.Count > 0)
            {
                Node u = pq.Dequeue(); // best node

                // Explores if bound's greater then profit value
                if (u.bound > maxProfit)
                {
                    int nextLevel = u.level + 1;
                    if (nextLevel < itemCount)
                    {
                        //Inclusion of next item
                        Node withChild = new Node
                        {
                            level = nextLevel,
                            weight = u.weight + weights[nextLevel],
                            profit = u.profit + profits[nextLevel]
                        };
                        //If in within capacity, max profit is updated
                        if (withChild.weight <= capacity)
                        {
                            if (withChild.profit > maxProfit)
                                maxProfit = withChild.profit;

                            withChild.bound = CalculateBound(withChild, itemCount, capacity, weights, profits);
                            if (withChild.bound > maxProfit)
                                pq.Enqueue(withChild, -withChild.bound);
                        }

                        //Exclude next item
                        Node withoutChild = new Node
                        {
                            level = nextLevel,
                            weight = u.weight,
                            profit = u.profit
                        };

                        withoutChild.bound = CalculateBound(withoutChild, itemCount, capacity, weights, profits);
                        if (withoutChild.bound > maxProfit)
                            pq.Enqueue(withoutChild, -withoutChild.bound);
                    }
                }
            }
            return maxProfit; //Result of possible max profit
        }
    }

}
