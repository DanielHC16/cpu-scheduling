# Round Robin (RR) CPU Scheduling in Python

class Process:
    def __init__(self, pid, bt):
        self.pid = pid          # Process ID
        self.bt = bt            # Burst Time
        self.rt = bt            # Remaining Time
        self.wt = 0             # Waiting Time
        self.tt = 0             # Turnaround Time


def round_robin(processes, quantum):
    n = len(processes)
    time = 0  # Current time

    # Keep track of completion
    completed = 0
    queue = processes.copy()

    while completed < n:
        for p in queue:
            if p.rt > 0:
                # If process needs more than quantum
                if p.rt > quantum:
                    time += quantum
                    p.rt -= quantum
                else:
                    # Process finishes in this cycle
                    time += p.rt
                    p.wt = time - p.bt
                    p.rt = 0
                    completed += 1

    # Compute turnaround time
    for p in processes:
        p.tt = p.bt + p.wt

    # Averages
    total_wt = sum(p.wt for p in processes)
    total_tt = sum(p.tt for p in processes)
    avg_wt = total_wt / n
    avg_tt = total_tt / n

    # Print results
    print("\nProcessID\tBT\tWT\tTT")
    for p in processes:
        print(f"{p.pid:>8}\t{p.bt:>2}\t{p.wt:>2}\t{p.tt:>2}")

    print(f"\nAverage Waiting Time = {avg_wt:.2f}")
    print(f"Average Turnaround Time = {avg_tt:.2f}")


# ------------------- Main -------------------
if __name__ == "__main__":
    n = int(input("Enter the number of processes: "))
    processes = []

    for i in range(1, n + 1):
        bt = int(input(f"Enter burst time for process {i}: "))
        processes.append(Process(i, bt))

    quantum = int(input("Enter Time Quantum: "))

    round_robin(processes, quantum)
