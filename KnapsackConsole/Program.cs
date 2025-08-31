using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        IterationsLog.Clear();
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

    // --- Basic Sensitivity Functions ---
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

    // --- Advanced Sensitivity Analysis ---
    public (int minValue, int maxValue) GetValueRange(int index)
    {
        int originalValue = Items[index].Value;
        var (_, originalSet, _) = Solve();
        int min = originalValue, max = originalValue;

        // Decrease value until solution changes
        while(true)
        {
            Items[index].Value = min - 1;
            var (_, newSet, _) = Solve();
            if(!newSet.SequenceEqual(originalSet)) break;
            min--;
        }

        // Increase value until solution changes
        while(true)
        {
            Items[index].Value = max + 1;
            var (_, newSet, _) = Solve();
            if(!newSet.SequenceEqual(originalSet)) break;
            max++;
        }

        Items[index].Value = originalValue;
        return (min, max);
    }

    public (int minWeight, int maxWeight) GetWeightRange(int index)
    {
        int originalWeight = Items[index].Weight;
        var (_, originalSet, _) = Solve();
        int min = originalWeight, max = originalWeight;

        // Decrease weight until solution changes
        while(true)
        {
            Items[index].Weight = min - 1;
            var (_, newSet, _) = Solve();
            if(!newSet.SequenceEqual(originalSet)) break;
            min--;
        }

        // Increase weight until solution changes
        while(true)
        {
            Items[index].Weight = max + 1;
            var (_, newSet, _) = Solve();
            if(!newSet.SequenceEqual(originalSet)) break;
            max++;
        }

        Items[index].Weight = originalWeight;
        return (min, max);
    }

    public int GetCapacityShadowPrice()
    {
        int originalCapacity = Capacity;
        var (profitOriginal, _, _) = Solve();
        Capacity += 1;
        var (profitNew, _, _) = Solve();
        Capacity = originalCapacity;
        return profitNew - profitOriginal;
    }
}