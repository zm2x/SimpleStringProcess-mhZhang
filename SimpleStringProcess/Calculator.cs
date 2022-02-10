
//program2-another-kind;
namespace SimpleStringProcess
{
    class Calculator {
        //get the operator index and calculation type;
       /*
        private static int IndexAndCalType(string inputstring, ref int calType)
        {
            int index = inputstring.IndexOf('+');
            if (index != -1)
            {
                calType = 0;
            }
            else
            {
                index = inputstring.IndexOf('-');
                if (index != -1)
                {
                    calType = 1;
                }
                else
                {
                    index = inputstring.IndexOf('*');
                    if (index != -1)
                    {
                        calType = 2;
                    }
                    else
                    {
                        index = inputstring.IndexOf('/');
                        if (index != -1)
                            calType = 3;
                    }
                }
            }

            return index;
        }
       */
        //using the data structure ,array,searching for  this  array;
        private static int IndexAndCalType(string inpustring, ref int caltype)
        {
            char [] operation = {'+', '-', '*', '/'};
            int index0 = 0;
            for (int i = 0; i < operation.Length; i++)
            {
                index0 = inpustring.IndexOf(operation[i]);
                if (index0 != -1)
                {
                    caltype = i;
                    break;
                }
            }

            return  index0;
        }
        // separate input string using operation index and calculate result;
        private static void CalResult(string inputstring, int caltype, int index)
        {
            string numStr1 = inputstring.Substring(0, index);
            int strLen = inputstring.Count();
            string numStr2 = inputstring.Substring(index + 1, strLen - index - 1);
            // convert string to float;
            float num1 = float.Parse(numStr1);
            float num2 = float.Parse(numStr2);
            float result = 0;
            // alternative sentence;
            switch (caltype)
            {
                case 0: result = num1 + num2;
                    break;
                case 1: result = num1 - num2;
                    break;
                case 2: result = num1 * num2;
                    break;
                case 3: result = num1 / num2;
                    break;
                default:   break;
                
            }
            Console.WriteLine($"{inputstring} = {result}");
        }

        public static void Cal()
        {
            while (true)
            {
                Console.WriteLine("please enter the calculation expression only supporting the four types of operation:");
                string inputstr = Console.ReadLine();
                int calType = -1;
                int index = IndexAndCalType(inputstr, ref calType);
                if (calType == -1)
                {
                    Console.WriteLine("only support the add,abstraction,multiply,divide!!");
                    //continue and break only used to loop;
                    continue;
                }
                CalResult(inputstr,calType,index);
            }
        }
    }
}