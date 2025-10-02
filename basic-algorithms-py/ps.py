# Priority Scheduling (Non-Preemptive) in Python

class Process:
    def __init__(self, pid, bt, priority):
        self.pid = pid          # Process ID
        self.bt = bt            # Burst Time
        self.priority = priority # Priority (lower number = higher priority)
        self.wt = 0             # Waiting Time
        self.tt = 0             # Turnaround Time


def priority_scheduling(processes):
    # Sort processes by priority (ascending: 1 = highest priority)
    processes.sort(key=lambda p: p.priority)

    n = len(processes)

    # First process waiting time = 0
    processes[0].wt = 0
    processes[0].tt = processes[0].bt

    # Calculate waiting and turnaround times
    for i in range(1, n):
        processes[i].wt = processes[i - 1].wt + processes[i - 1].bt
        processes[i].tt = processes[i].wt + processes[i].bt

    # Calculate totals for averages
    total_wt = sum(p.wt for p in processes)
    total_tt = sum(p.tt for p in processes)
    avg_wt = total_wt / n
    avg_tt = total_tt / n

    # Print the table
    print("\nProcessID\tBT\tPriority\tWT\tTT")
    for p in processes:
        print(f"{p.pid:>8}\t{p.bt:>2}\t{p.priority:>8}\t{p.wt:>2}\t{p.tt:>2}")

    print(f"\nAverage Waiting Time = {avg_wt:.2f}")
    print(f"Average Turnaround Time = {avg_tt:.2f}")


# ------------------- Main -------------------
if __name__ == "__main__":
    n = int(input("Enter the number of processes: "))
    processes = []

    for i in range(1, n + 1):
        bt = int(input(f"Enter burst time for process {i}: "))
        pr = int(input(f"Enter priority for process {i} (lower = higher priority): "))
        processes.append(Process(i, bt, pr))

    priority_scheduling(processes)
