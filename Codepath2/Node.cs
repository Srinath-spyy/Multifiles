//-----------------------------------------------------------------------
// <auto-generated />
//-----------------------------------------------------------------------
{
using System;
using System.Collections.Generic;

    /// <summary>
    /// Represents a task node with a name, dependencies, and execution status.
    /// </summary>
public classNode
    {
        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the list of dependencies for this task.
        /// </summary>
    public List<Node> Dependencies { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the task has been executed.
        /// </summary>
    public bool IsExecuted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> struct.
        /// </summary>
        /// <param name="name">The name of the task.</param>
    public Node(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Dependencies = new List<Node>();
            this.IsExecuted = false;
        }

        /// <summary>
        /// Adds a dependency to the task.
        /// </summary>
        /// <param name="dependency">The task to add as a dependency.</param>
    public void AddDependency(Node dependency)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            this.Dependencies.Add(dependency);
        }
    }
}