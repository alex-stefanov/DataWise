namespace DataWise.Data.DbContexts.NonReleational.Enums;

public enum Complexity
{
    Constant = 0,    
    
    Logarithmic = 1, 
    
    Linear = 2,    
    
    Linearithmic = 3,  
    
    Quadratic = 4,    
    
    Cubic = 5,    
    
    Exponential = 6,  
    
    Factorial = 7,

    //O(V+E)
    GraphTraversal = 8,

    //O(n * capacity)
    PseudoPolynomial = 9,

    Unknown = 10
}