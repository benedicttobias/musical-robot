using System;
using System.Linq;
using System.Linq.Expressions;

namespace expression
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adding two numbers
            var leftParameterExpression = Expression.Parameter(typeof(int), "x");
            var rightParameterExpression = Expression.Parameter(typeof(int), "y");
            var operationExpression = Expression.Add(leftParameterExpression, rightParameterExpression);

            Expression<Func<int, int, int>> addingTwoNumbers = Expression.Lambda<Func<int, int, int>>(
                operationExpression,
                leftParameterExpression,
                rightParameterExpression);

            var result = addingTwoNumbers.Compile()(4,5);

            Console.WriteLine($"addingTwoNumbers: {result}");

            // Create a filter on queryable
            var numbers = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

            var leftParameter = Expression.Parameter(typeof(int), "left");
            var rightParameter = Expression.Constant(10, typeof(int));

            var filterExpression = Expression.Lambda<Func<int, bool>>(Expression.GreaterThan(leftParameter, rightParameter), leftParameter);

            Func<int, bool> filterFunc = i => i > 10; 

            var filterResult = numbers.Where(x => filterExpression.Compile()(x));
            Console.WriteLine($"filterExpression: {string.Join(",",filterResult)}");

            filterResult = numbers.Where(x => filterFunc(x));
            Console.WriteLine($"filterExpression: {string.Join(",", filterResult)}");
        }
    }
}
