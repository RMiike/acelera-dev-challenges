using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
  public class Math
  {
    public List<int> Fibonacci()
    {
      int inicialNumber = 0, auxiliarNumber = 1;
      var fibonacciList = new List<int>();

      return Recursive(inicialNumber, auxiliarNumber, fibonacciList);
    }

    public bool IsFibonacci(int numberToTest)
         => Fibonacci().Contains(numberToTest);


    private static List<int> Recursive(int a, int b, List<int> fibonacciList)
    {
      if (a <= 350)
      {
        fibonacciList.Add(a);
        Recursive(b, a + b, fibonacciList);
      }
      return fibonacciList;
    }
  }
}
