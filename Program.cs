using System;

namespace Homework_Array_No._5
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"C:/Homework/input.txt";
            double [,] ArrayofInput = ReadImageDataFromFile(input); // Ex. 5x5
            
            string convolution = @"C:/Homework/convolution.txt";
            double [,] ArrayofConvolution = ReadImageDataFromFile(convolution); // 3x3

            int RepeatedInputRows = ArrayofInput.GetLength(0) + 2;  //Ex. = 7
            int RepeatedInputColumn = ArrayofInput.GetLength(1) + 2; //Ex. = 7
            
            double[,] RepeatedArray = new double [RepeatedInputRows, RepeatedInputColumn]; // 7x7
            
            int endRows = ArrayofInput.GetLength(0); //Ex. = 5
            int endColumn = ArrayofInput.GetLength(1); //Ex. = 5         

            double [,] CopiedRepeatedArray = CopytoTheBiggerArrayExceptEdge(endRows, endColumn, RepeatedArray, ArrayofInput);

            double[,] PerfectArray = NewPerfectArrayWithEdge(endRows, endColumn, ArrayofInput, RepeatedInputRows, RepeatedInputColumn, CopiedRepeatedArray); // 7x7

            int KernelRows = ArrayofConvolution.GetLength(0); // 3
            int KernelColumn = ArrayofConvolution.GetLength(1); // 3
            double[,] FinishedArray = new double[endRows, endColumn]; // 5x5
            
            int x = 0;
            int z1 = 0;

            for (int b = 1; b <= endRows; b++)
            {
                int z2 = 0;
                int y = 0;
                for (int a = 1; a <= endColumn; a++)
                {
                    FinishedArray[z1, z2] = MultiplyConvolveArray(KernelRows, KernelColumn, PerfectArray, ArrayofConvolution, x, y); // 3, 3, [7,7], [3,3]
                    z2++;
                    y++;
                }
                z1++;
                x++;
            }     
            string output = @"C:/Homework/output.txt";
            WriteImageDataToFile(output, FinishedArray);
        }
        
        
        
        
        static double MultiplyConvolveArray(int KernelRows, int KernelColumn, double[,] PerfectArray, double[,] ArrayofConvolution, int x1, int y1)
        {
            double sum = 0;
            int x = 0;
            x = x + x1;

            for (int b = 0; b < KernelRows; b++) // KernelRows = 3
            {
                int y = 0;
                y = y + y1;
                for (int a = 0; a < KernelColumn; a++) // KernelColumn = 3
                {
                    sum = sum + (PerfectArray[x,y] * ArrayofConvolution[b, a]);
                    y++;
                }
                x++;
            }
            return sum;
        }



        static double[,] CopytoTheBiggerArrayExceptEdge(int endRows, int endColumn, double[,] RepeatedArray, double[,] ArrayofInput)
        {
            int z2 = 0;
            //endRows = 5 , endColumn = 5
            for (int j = 1; j <= endRows; j++)
            {
                int z1 = 0;
                for (int i = 1; i <= endColumn; i++)
                {
                    RepeatedArray[j, i] = ArrayofInput[z2, z1];
                    z1++;
                }
                z2++;
            }
            return RepeatedArray;
        }



        static double [,] NewPerfectArrayWithEdge(int endRows, int endColumn, double [,] ArrayofInput, int RepeatedInputRows, int RepeatedInputColumn, double [,] CopiedRepeatedArray)
        {
            int z1 = 0;
            int z2 = 0;
            int z3 = 0;
            int z4 = 0;

            for (int a = 1; a <= endColumn; a++)
            {
                CopiedRepeatedArray[RepeatedInputColumn - 1, a] = ArrayofInput[0, z1];
                z1++;
            }

            for (int b = 1; b <= endColumn; b++)
            {
                CopiedRepeatedArray[0, b] = ArrayofInput[endColumn - 1, z2];
                z2++;
            }

            for (int c = 1; c <= endRows; c++)
            {
                CopiedRepeatedArray[c, RepeatedInputRows - 1] = ArrayofInput[z3, 0];
                z3++;
            }

            for (int d = 1; d <= endRows; d++)
            {
                CopiedRepeatedArray[d, 0] = ArrayofInput[z4, endRows - 1];
                z4++;
            }

            CopiedRepeatedArray[RepeatedInputRows - 1, RepeatedInputColumn - 1] = ArrayofInput[0, 0];
            CopiedRepeatedArray[RepeatedInputRows - 1, 0] = ArrayofInput[0, endRows - 1];
            CopiedRepeatedArray[0, RepeatedInputColumn - 1] = ArrayofInput[endRows - 1, 0];
            CopiedRepeatedArray[0, 0] = ArrayofInput[endRows - 1, endColumn - 1];

            return CopiedRepeatedArray;
        }



        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }



        static void WriteImageDataToFile(string imageDataFilePath,
                                         double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,
                                                imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }

            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }

    }
}
