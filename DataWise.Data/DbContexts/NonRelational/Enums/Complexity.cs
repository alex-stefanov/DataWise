namespace DataWise.Data.DbContexts.NonRelational.Enums;

/// <summary>
/// Represents the different types of time and space complexities commonly encountered in algorithms.
/// </summary>
public enum Complexity
{
    /// <summary>
    /// Constant time complexity: O(1)
    /// </summary>
    Constant = 0,

    /// <summary>
    /// Logarithmic time complexity: O(log n)
    /// </summary>
    Logarithmic = 1,

    /// <summary>
    /// Linear time complexity: O(n)
    /// </summary>
    Linear = 2,

    /// <summary>
    /// Linearithmic time complexity: O(n log n)
    /// </summary>
    Linearithmic = 3,

    /// <summary>
    /// Quadratic time complexity: O(n^2)
    /// </summary>
    Quadratic = 4,

    /// <summary>
    /// Cubic time complexity: O(n^3)
    /// </summary>
    Cubic = 5,

    /// <summary>
    /// Exponential time complexity: O(2^n)
    /// </summary>
    Exponential = 6,

    /// <summary>
    /// Factorial time complexity: O(n!)
    /// </summary>
    Factorial = 7,

    /// <summary>
    /// Graph traversal time complexity: O(V + E)
    /// Where V is the number of vertices and E is the number of edges.
    /// </summary>
    GraphTraversal = 8,

    /// <summary>
    /// Pseudo-polynomial time complexity: O(n * capacity)
    /// Typically applies to algorithms for problems like knapsack where n is the size of the input and capacity is a parameter.
    /// </summary>
    PseudoPolynomial = 9,

    /// <summary>
    /// Unknown time complexity.
    /// </summary>
    Unknown = 10
}