enum Operator
{
    Add,
    Subtraction,
    Multiply,
    Divide
}

//function can interact each other in the class without interface;

namespace ConsoleApp1
{
  class Operate
  {
      private static void Process(Operator sign, ref double result, ref double front, ref double back)
    {
        switch (sign)
        {
            case Operator.Add:
                result = front + back;
                Console.WriteLine($"{front}+{back}={result} ");
                break;
            case Operator.Subtraction:
                result = front - back;
                Console.WriteLine($"{front}-{back} ={result} ");
                break;
            case Operator.Multiply:
                result = front * back;
                Console.WriteLine($"{front} *{back} ={result}");
                break;
            case Operator.Divide:
                result = front / back;
                Console.WriteLine($"{front}/{back}= {result}");
                break;
            default:
                Console.WriteLine("can't compute!");
                break;
        }
    }
    private static void Collection(string parameter, int index,ref double front,ref double back)
    {
       front = double.Parse(parameter.Substring(0, index));
       back = double.Parse(parameter.Substring(index + 1, parameter.Length - index - 1));
    }

    public static void Cal()
    {
        double front = 0;
        double back = 0;
        double result = 0;
        int j = 0;
        int i = 0;
        do
        {
            Console.WriteLine("please enter expression: ");
            string val = Console.ReadLine();
            for (; i < val.Length; i++)
            {
                if (val[i] == '+')
                {
                    Collection(val, i, ref front, ref back);
                    Process(Operator.Add, ref result, ref front, ref back);
                }
                else if (val[i] == '-')
                {
                    Collection(val, i, ref front, ref back);
                    Process(Operator.Subtraction, ref result, ref front, ref back);
                }
                else if (val[i] == '*')
                {
                    Collection(val, i, ref front, ref back);
                    Process(Operator.Multiply, ref result, ref front, ref back);
                }
                else if (val[i] == '/')
                {
                    Collection(val, i, ref front, ref back);
                    Process(Operator.Divide, ref result, ref front, ref back);
                }
            }
            i = 0;
            j++;
        } while (true); // non_end loop
    }
  }
}

