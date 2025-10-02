using System;
using System.Collections.Generic;
using System.Linq;

class Process
{
    public int PID { get; set; }
    public int BT { get; set; } // Burst Time
    public int WT { get; set; } // Waiting Time
    public int TAT { get; set; } // Turnaround Time
    public int Priority { get; set; }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("CPU Scheduling Algorithms in C#");
        Console.WriteLine("1. First Come First Serve (FCFS)");
        Console.WriteLine("2. Shortest Job First (SJF)");
        Console.WriteLine("3. Priority Scheduling");
        Console.WriteLine("4. Round Robin");
        Console.Write("Choose an algorithm (1-4): ");
        int choice = int.Parse(Console.ReadLine() ?? "1");

        switch (choice)
        {
            case 1:
                FCFS();
                break;
            case 2:
                SJF();
                break;
            case 3:
                PriorityScheduling();
                break;
            case 4:
                RoundRobin();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    // ---------- 1. First Come First Serve ----------
    static void FCFS()
    {
        Console.Write("Enter number of processes: ");
        int n = int.Parse(Console.ReadLine() ?? "0");
        List<Process> processes = new();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter burst time for process {i + 1}: ");
            int bt = int.Parse(Console.ReadLine() ?? "0");
            processes.Add(new Process { PID = i + 1, BT = bt });
        }

        processes[0].WT = 0;
        processes[0].TAT = processes[0].BT;

        for (int i = 1; i < n; i++)
        {
            processes[i].WT = processes[i - 1].WT + processes[i - 1].BT;
            processes[i].TAT = processes[i].WT + processes[i].BT;
        }

        PrintResults(processes);
    }

    // ---------- 2. Shortest Job First ----------
    static void SJF()
    {
        Console.Write("Enter number of processes: ");
        int n = int.Parse(Console.ReadLine() ?? "0");
        List<Process> processes = new();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter burst time for process {i + 1}: ");
            int bt = int.Parse(Console.ReadLine() ?? "0");
            processes.Add(new Process { PID = i + 1, BT = bt });
        }

        // sort by burst time
        processes = processes.OrderBy(p => p.BT).ToList();

        processes[0].WT = 0;
        processes[0].TAT = processes[0].BT;

        for (int i = 1; i < n; i++)
        {
            processes[i].WT = processes[i - 1].WT + processes[i - 1].BT;
            processes[i].TAT = processes[i].WT + processes[i].BT;
        }

        PrintResults(processes);
    }

    // ---------- 3. Priority Scheduling ----------
    static void PriorityScheduling()
    {
        Console.Write("Enter number of processes: ");
        int n = int.Parse(Console.ReadLine() ?? "0");
        List<Process> processes = new();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter burst time for process {i + 1}: ");
            int bt = int.Parse(Console.ReadLine() ?? "0");
            Console.Write($"Enter priority for process {i + 1} (lower = higher priority): ");
            int pr = int.Parse(Console.ReadLine() ?? "0");
            processes.Add(new Process { PID = i + 1, BT = bt, Priority = pr });
        }

        // sort by priority (ascending = highest first)
        processes = processes.OrderBy(p => p.Priority).ToList();

        processes[0].WT = 0;
        processes[0].TAT = processes[0].BT;

        for (int i = 1; i < n; i++)
        {
            processes[i].WT = processes[i - 1].WT + processes[i - 1].BT;
            processes[i].TAT = processes[i].WT + processes[i].BT;
        }

        PrintResults(processes);
    }

    // ---------- 4. Round Robin ----------
    static void RoundRobin()
    {
        Console.Write("Enter number of processes: ");
        int n = int.Parse(Console.ReadLine() ?? "0");
        List<Process> processes = new();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter burst time for process {i + 1}: ");
            int bt = int.Parse(Console.ReadLine() ?? "0");
            processes.Add(new Process { PID = i + 1, BT = bt });
        }

        Console.Write("Enter time quantum: ");
        int quantum = int.Parse(Console.ReadLine() ?? "1");

        int[] remainingBT = processes.Select(p => p.BT).ToArray();
        int time = 0;
        Queue<int> q = new Queue<int>(Enumerable.Range(0, n));

        while (q.Count > 0)
        {
            int i = q.Dequeue();
            if (remainingBT[i] > 0)
            {
                if (remainingBT[i] > quantum)
                {
                    time += quantum;
                    remainingBT[i] -= quantum;
                    q.Enqueue(i);
                }
                else
                {
                    time += remainingBT[i];
                    processes[i].TAT = time;
                    processes[i].WT = processes[i].TAT - processes[i].BT;
                    remainingBT[i] = 0;
                }
            }
        }

        PrintResults(processes);
    }

    // ---------- Helper to Print ----------
    static void PrintResults(List<Process> processes)
    {
        Console.WriteLine("\nPID\tBT\tWT\tTAT");
        foreach (var p in processes)
            Console.WriteLine($"{p.PID}\t{p.BT}\t{p.WT}\t{p.TAT}");

        double avgWT = processes.Average(p => p.WT);
        double avgTAT = processes.Average(p => p.TAT);
        Console.WriteLine($"\nAverage Waiting Time = {avgWT:F2}");
        Console.WriteLine($"Average Turnaround Time = {avgTAT:F2}");
    }
}
