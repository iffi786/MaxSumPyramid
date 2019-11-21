using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxSum
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //get input
            var input = GetInput();

            string[] rows = input.Split('\n');

            var table = ConvertTheTriangleIntoTable(rows);

            List<int> result = GetMaxSum(rows, table);

            Console.WriteLine($"The Maximum Total Sum From Top To Bottom Is:  {result.Sum(s => s)}");

            Console.ReadKey();
        }

        private static string GetInput()
        {
            const string input = @"   215
                                   192 124
                                  117 269 442
                                218 836 347 235
                              320 805 522 417 345
                            229 601 728 835 133 124
                          248 202 277 433 207 263 257
                        359 464 504 528 516 716 871 182
                      461 441 426 656 863 560 380 171 923
                     381 348 573 533 448 632 387 176 975 449
                   223 711 445 645 245 543 931 532 937 541 444
                 330 131 333 928 376 733 017 778 839 168 197 197
                131 171 522 137 217 224 291 413 528 520 227 229 928
              223 626 034 683 839 053 627 310 713 999 629 817 410 121
            924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
            return input;
        }

        private static List<int> GetMaxSum(string[] arrayOfRowsByNewlines, int[,] tableHolder)
        {
            bool isFirst = true;
            List<int> sumList = new List<int>();// This would be used to save the whole path followed
            bool isLastNumEven = false;
            int nextIndex = 0;
            for (int i = 0; i < arrayOfRowsByNewlines.Length; i++)
            {
                for (int j = nextIndex; j < arrayOfRowsByNewlines.Length;)
                {
                    int num1 = tableHolder[i, j];
                    int num2 = tableHolder[i, j + 1];
                    bool isNum1Even = IsEven(num1);
                    bool isNum2Even = IsEven(num2);

                    if (isFirst)
                    {
                        isLastNumEven = isNum1Even;
                        sumList.Add(num1);
                        isFirst = false;
                        nextIndex = j;
                        break;
                    }
                    else if (isLastNumEven)
                    {
                        //If the last number is even than choose the next number is odd
                        if (!isNum1Even && !isNum2Even)
                        {
                            if (num1 > num2)
                            {
                                nextIndex = j;
                                sumList.Add(num1);
                            }
                            else
                            {
                                nextIndex = j + 1;
                                sumList.Add(num2);
                            }

                            isLastNumEven = isNum1Even; //Both numbers are odd, take first one
                        }
                        else if (!isNum1Even && isNum2Even)
                        {
                            //The first number is odd and second is even
                            sumList.Add(num1);
                            isLastNumEven = isNum1Even;
                            nextIndex = j;
                        }
                        else if (isNum1Even && !isNum2Even)
                        {
                            //The first number is even and second is odd
                            nextIndex = j + 1;
                            sumList.Add(num2);
                            isLastNumEven = isNum2Even;
                        }
                        break;
                    }
                    else //odd
                    {
                        if (isNum1Even && isNum2Even)
                        {
                            if (num1 > num2)
                                nextIndex = j;
                            else
                                nextIndex = j + 1;

                            sumList.Add(Math.Max(num1, num2));
                            isLastNumEven = isNum1Even;
                        }
                        else if (isNum1Even && !isNum2Even)
                        {
                            nextIndex = j;
                            sumList.Add(num1);
                            isLastNumEven = isNum1Even;
                        }
                        else if (!isNum1Even && isNum2Even)
                        {
                            nextIndex = j + 1;
                            sumList.Add(num2);
                            isLastNumEven = isNum2Even;
                        }
                        break;
                    }
                }
            }
            return sumList;
        }

        private static int[,] ConvertTheTriangleIntoTable(string[] rows)
        {
            int[,] tableHolder = new int[rows.Length, rows.Length + 1];

            for (int row = 0; row < rows.Length; row++)
            {
                var eachCharactersInRow = rows[row].Trim().Split(' ');

                for (int column = 0; column < eachCharactersInRow.Length; column++)
                {
                    int number;
                    int.TryParse(eachCharactersInRow[column], out number);
                    tableHolder[row, column] = number;
                }
            }
            return tableHolder;
        }

        public static bool IsEven(int number)
        {
            return (number % 2 == 0);
        }
    }
}