using System;
using System.Collections.Generic;

public class TaskNode
{
    public string Name { get; }
    public List<TaskNode> Dependencies { get; }
    public bool IsExecuted { get; set; }

    public TaskNode(string name)
    {
        Name = name;
        Dependencies = new List<TaskNode>();
        IsExecuted = false;
    }

    public void AddDependency(TaskNode dependency)
    {
        Dependencies.Add(dependency);
    }
}

public class TaskScheduler
{
    private readonly Dictionary<string, TaskNode> _tasks;

    public TaskScheduler()
    {
        _tasks = new Dictionary<string, TaskNode>();
    }

    public void AddTask(string taskName)
    {
        if (!_tasks.ContainsKey(taskName))
        {
            _tasks[taskName] = new TaskNode(taskName);
        }
    }

    public void AddDependency(string taskName, string dependencyName)
    {
        if (!_tasks.ContainsKey(taskName))
        {
            throw new ArgumentException($"Task '{taskName}' does not exist.");
        }

        if (!_tasks.ContainsKey(dependencyName))
        {
            throw new ArgumentException($"Dependency '{dependencyName}' does not exist.");
        }

        _tasks[taskName].AddDependency(_tasks[dependencyName]);
    }

    public void ExecuteTasks()
    {
        var executedTasks = new HashSet<string>();
        var executionOrder = new List<string>();

        foreach (var task in _tasks.Values)
        {
            if (!executedTasks.Contains(task.Name))
            {
                ExecuteTask(task, executedTasks, executionOrder);
            }
        }

        Console.WriteLine("Execution Order: " + string.Join(", ", executionOrder));
    }

    private void ExecuteTask(TaskNode task, HashSet<string> executedTasks, List<string> executionOrder)
    {
        if (task.IsExecuted)
        {
            return;
        }

        foreach (var dependency in task.Dependencies)
        {
            if (!executedTasks.Contains(dependency.Name))
            {
                ExecuteTask(dependency, executedTasks, executionOrder);
            }
        }

        task.IsExecuted = true;
        executedTasks.Add(task.Name);
        executionOrder.Add(task.Name);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var scheduler = new TaskScheduler();

        // Adding tasks
        scheduler.AddTask("Compile");
        scheduler.AddTask("Test");
        scheduler.AddTask("Deploy");

        // Adding dependencies
        scheduler.AddDependency("Test", "Compile");
        scheduler.AddDependency("Deploy", "Test");

        // Executing tasks
        scheduler.ExecuteTasks();
    }
}