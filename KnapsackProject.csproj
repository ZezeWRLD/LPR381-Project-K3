using System;
using System.Collections.Generic;
using System.IO;

class KnapsackItem
{
    public int Weight { get; set; }
    public int Value { get; set; }
    public double Ratio => (double)Value / Weight;
}

class Node
{
    public int Level;
    public int Profit;
    public int Weight;
    public double Bound;
    public List<int> Taken;
}

class KnapsackSolver
{
    private int Capacity;
    private List<KnapsackItem> Items;
    private List<string> IterationsLog;

    public KnapsackSolver(List<KnapsackItem> items, int capacity)
    {
        Items = items;
        Capacity = capacity;
        Items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));
        IterationsLog = new List<string>();
    }

    private double Bound(Node node)
    {
        if (node.Weight >= Capacity) return 0;
        double profitBound = node.Profit;
        int j = node.Level + 1;
        int totWeight = node.Weight;

        while (j < Items.Count && totWeight + Items[j].Weight <= Capacity)
        {
            totWeight += Items[j].Weight;
            profitBound += Items[j].Value;
            j++;
        }

        if (j < Items.Count)
            profitBound += (Capacity - totWeight) * Items[j].Ratio;

        return profitBound;
    }

    public (int, List<int>, List<string>) Solve()
    {
        Queue<Node> Q = new Queue<Node>();
        Node u, v;

        v = new Node { Level = -1, Profit = 0, Weight = 0, Taken = new List<int>() };
        Q.Enqueue(v);

        int maxProfit = 0;
        List<int> bestSet = new List<int>();

        IterationsLog.Add("Level | Profit | Weight | Bound  | Taken");

        while (Q.Count > 0)
        {
            v = Q.Dequeue();
            if (v.Level == Items.Count - 1) continue;

            // Include item
            u = new Node
            {
                Level = v.Level + 1,
                Weight = v.Weight + Items[v.Level + 1].Weight,
                Profit = v.Profit + Items[v.Level + 1].Value,
                Taken = new List<int>(v.Taken) { 1 }
            };

            u.Bound = Bound(u);
            IterationsLog.Add($"{u.Level,5} | {u.Profit,6} | {u.Weight,6} | {u.Bound,6:F2} | {string.Join("", u.Taken)}");

            if (u.Weight <= Capacity && u.Profit > maxProfit)
            {
                maxProfit = u.Profit;
                bestSet = new List<int>(u.Taken);
            }

            if (u.Bound > maxProfit) Q.Enqueue(u);

            // Exclude item
            u = new Node
            {
                Level = v.Level + 1,
                Weight = v.Weight,
                Profit = v.Profit,
                Taken = new List<int>(v.Taken) { 0 }
            };

            u.Bound = Bound(u);
            IterationsLog.Add($"{u.Level,5} | {u.Profit,6} | {u.Weight,6} | {u.Bound,6:F2} | {string.Join("", u.Taken)}");

            if (u.Bound > maxProfit) Q.Enqueue(u);
        }

        return (maxProfit, bestSet, IterationsLog);
    }

    // --- Sensitivity Analysis Functions ---
    public void ChangeItemValue(int index, int newValue)
    {
        Items[index].Value = newValue;
        Items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));
    }

    public void ChangeItemWeight(int index, int newWeight)
    {
        Items[index].Weight = newWeight;
        Items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));
    }

    public void ChangeCapacity(int newCapacity)
    {
        Capacity = newCapacity;
    }

    public void AddNewItem(int value, int weight)
    {
        Items.Add(new KnapsackItem { Value = value, Weight = weight });
        Items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));
    }
}

class Program
{
    static void Main()
    {
        string inputPath = "knapsack_input.txt";
        string outputPath = "knapsack_output.txt";

        string[] lines = File.ReadAllLines(inputPath);

        // Parse objective
        string[] objective = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<int> values = new List<int>();
        for (int i = 1; i < objective.Length; i++)
            values.Add(int.Parse(objective[i].Substring(1)));

        // Parse constraint
        string[] constraintParts = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<int> weights = new List<int>();
        for (int i = 0; i < values.Count; i++)
            weights.Add(int.Parse(constraintParts[i].Substring(1)));
        int capacity = int.Parse(constraintParts[^1]);

        List<KnapsackItem> items = new List<KnapsackItem>();
        for (int i = 0; i < values.Count; i++)
            items.Add(new KnapsackItem { Value = values[i], Weight = weights[i] });

        KnapsackSolver solver = new KnapsackSolver(items, capacity);
        var (maxProfit, bestSet, log) = solver.Solve();

        // Save results
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("Canonical Knapsack Model");
            writer.WriteLine($"Capacity: {capacity}");
            writer.WriteLine($"Values: {string.Join(" ", values)}");
            writer.WriteLine($"Weights: {string.Join(" ", weights)}");
            writer.WriteLine("-----------------------------------");
            writer.WriteLine("Branch & Bound Iterations:");
            foreach (var step in log) writer.WriteLine(step);
            writer.WriteLine("-----------------------------------");
            writer.WriteLine($"Best Profit: {maxProfit}");
            writer.WriteLine($"Items Taken: {string.Join(" ", bestSet)}");
        }

        Console.WriteLine("✅ Base Knapsack Solved. Results saved to file.");
        
        // --- Sensitivity Analysis Demo ---
        Console.WriteLine("\n--- Sensitivity Analysis ---");

        // Example 1: Change item value
        solver.ChangeItemValue(0, 10);
        var (profit1, set1, _) = solver.Solve();
        Console.WriteLine($"If item 1 value = 10 → New Best Profit = {profit1}");

        // Example 2: Change item weight
        solver.ChangeItemWeight(1, 5);
        var (profit2, set2, _) = solver.Solve();
        Console.WriteLine($"If item 2 weight = 5 → New Best Profit = {profit2}");

        // Example 3: Change capacity
        solver.ChangeCapacity(50);
        var (profit3, set3, _) = solver.Solve();
        Console.WriteLine($"If capacity = 50 → New Best Profit = {profit3}");

        // Example 4: Add new item
        solver.AddNewItem(7, 9);
        var (profit4, set4, _) = solver.Solve();
        Console.WriteLine($"If new item (v=7,w=9) is added → New Best Profit = {profit4}");
    }
}