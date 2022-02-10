/*  problem target:
only support integer ranging  from 0 to 9;
support '('  and ')'
only support divide to an integer;
input: a string containing four types calculations  of  number conforming above statement 
simple example: 5+6-6*9/8 ;
output:result;*/
// application  of stack: 
namespace SimpleStringProcess
{
    class StrPro
    {
        private static string[] Operator1 { get; } = new[] {"+", "-", "*", "/", "(", ")"};
        private delegate double Calculation(double num1, double num2);
        private static Dictionary<string, int> Priority1 { get; } = new Dictionary<string, int>()
        {
            {"+",0},{"-",0},{"*",1},
            {"/",1},{"(",2},{")",3}
        };
        public static void Mainfunction()
        {
            bool flagg = true;
            Console.Write("please enter the expression:  ");
            var inputstring = Console.ReadLine();
            if (inputstring == null)
            {
                Console.WriteLine("The expression is null !");
            }
            else
            {
                string inputstringfinal = Judgement(inputstring,ref flagg);
                if (flagg)
                {
                    List<string> inputstringlist = StringPreprocess(inputstringfinal);
                    List<string> list00 = Convertfunction(inputstringlist);
                    double result = Calculate(list00);
                    Console.WriteLine($"{inputstring}= {result}");
                }
                else
                {
                    Console.WriteLine("Current expression is valid  !");
                }
            }
        }
        private static string Judgement(string inputstring,ref bool flagg)
        {
            string inputstring0 = inputstring;//值类型copy
            string returnstring = inputstring;//copy
            for (int k = 0; k < inputstring.Length; k++)
            {
                if (inputstring[k] == '-' && k == 0)
                {
                    returnstring = inputstring0.Insert(0, "0");
                }
                else if (inputstring[k] == '-' && inputstring[k - 1] == '(')
                {
                    returnstring = inputstring0.Insert(k, "0");
                }
                if (inputstring[k] == ' ')
                {
                    returnstring = inputstring0.Remove(k);
                }
                if (StringArrayToString(Operator1).IndexOf(inputstring[k]) < 0&&inputstring[k]!='.')
                {
                    if (inputstring[k] < '0' || inputstring[k] > '9')
                    {
                        flagg = false;
                    }
                }
            
            }
            //Console.WriteLine(returnstring);
            return returnstring; 
        }
        private static List<string> StringPreprocess(string inputstring)
        {
            List<string> liststring = new List<string>();
            List<char> temp = new List<char>();
            for(int i=0;i<inputstring.Length;i++)//have a interface (Enumrable)
            {
                if (inputstring[i] >= '0' && inputstring[i] <= '9' || inputstring[i] == '.')
                {
                    temp.Add(inputstring[i]);
                    if (i < inputstring.Length-1)
                    {
                        if (StringArrayToString(Operator1).IndexOf(inputstring[i + 1]) >=
                            0) //make a judgement whether is the last number of double
                        {
                            liststring.Add(CharArrayToString(temp));
                        }
                    }
                    else
                    {
                        liststring.Add(CharArrayToString(temp));
                    }
                }
                else
                {
                        liststring.Add(inputstring[i].ToString());
                        temp.Clear();
                }
            }
            /*foreach (var item in liststring)
            { 
                Console.WriteLine(item);
            }*/
            return liststring;
        }
        //Convert the input-string  to back_expression
        private static List<string> Convertfunction(List<string>stringlist)
        {
            var backexpression = new List<string>();
            var oper = new Stack<string>();
            oper.Push(".");
            foreach (var string0 in stringlist)
            {
                if (!IsInStringArray(string0,Operator1))
                {
                    backexpression.Add(string0);
                }
                else
                {
                    if (oper.Peek() == "." || Priority1[string0] == 2 || Priority1[oper.Peek()] == 2 ||
                        Priority1[string0] >= Priority1[oper.Peek()]&&Priority1[string0]!=3)
                    {
                        oper.Push(string0);
                    }
                    else if (Priority1[string0] < Priority1[oper.Peek()] && Priority1[string0] != 3)
                    {
                        while (oper.Peek() != "." && oper.Peek() != "(")
                        {
                            backexpression.Add(oper.Peek());
                            oper.Pop();
                        }

                        oper.Push(string0);
                    }
                    else if (Priority1[string0] == 3)
                    {
                        while (oper.Peek() != "(")
                        {
                            backexpression.Add(oper.Peek());
                            oper.Pop();
                        }
                        oper.Pop();
                    }
                }
            }
            while (oper.Peek() != ".")
            {
                backexpression.Add(oper.Peek());
                oper.Pop();
            }
            foreach( var item in backexpression){
                Console.WriteLine(item);
            }
            
            return backexpression;
        }
       //  calculate the back_expression
        private static double Calculate(List<string> list0)
        {
            double  result=0.0;
            double  finalresult;
            Stack<double> stack00 = new Stack<double>();
            Calculation[] functionarray =
            {
                (x, y) => x + y, (x, y) => x - y, (x, y) =>x/y,
                (x, y) => x * y//the container of function
            };
            foreach (var string1 in list0)
            {
                if (!IsInStringArray(string1,Operator1))
                {
                    stack00.Push(Convert.ToDouble(string1));//convert char into int must use To-string 
                    /*foreach (var item in stack00)
                    {
                        Console.WriteLine(item);//yield return item;
                    }*/
                }
                else
                {
                    double num1 = stack00.Pop();
                    double num2 = stack00.Pop();
                    switch (string1)
                    {
                        case "+": result = functionarray[0](num1, num2);
                            break;
                        case "-": result = functionarray[1](num2, num1);
                            break;
                        case "*": result = functionarray[3](num1, num2);
                            break;
                        case "/":
                            try
                            {
                                result = functionarray[2](num2, num1);
                            }
                            catch (DivideByZeroException)
                            {
                                Console.WriteLine("Divide by zero !!");
                            }
                            break;
                        default: Console.WriteLine("only support the four types calculation !");
                            break;
                    }
                    stack00.Push(result);
                }
            }
            finalresult = stack00.Peek();
            return finalresult;
        }
        private static bool IsInStringArray(string string0,string[]stringarr)
        {
            bool flag =false;
            foreach (var item in stringarr)
            {
                if (item == string0)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private static string CharArrayToString( List<char>listchar)
        {
            string tempstring = "";
            foreach (var item in listchar)
            {
                tempstring = tempstring + item.ToString();
            }
            return tempstring; 
        }
        private static string StringArrayToString(string[] string0)
        {
            string temp = "";
            foreach (var item in string0)
            {
                temp = temp + item;
            }
            return temp;
        }
        
    }
}

