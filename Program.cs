using System;

namespace MatrixOfIntegersWithQuery
{
    class Program
    {
        static void Main(string[] args)
        {

            int[][] matrixInput = new int[5][]
            {
                new int[3]{2, 0, 4 },
                new int[3]{ 2, 8, 5 },
                new int[3]{ 6, 0, 9},
                new int[3]{ 2, 7, 10 },
                new int[3]{ 4, 3, 4}
            };

            int[][] queries = new int[2][]
            {
                new int[2]{ 0,0},
                new int[2]{ 1,3}
            };

            int[][] matrixInput2 = new int[2][]
            {
                new int[4]{1, 9, 10, 8},
                new int[4]{ 3, 4, 4, 4 }
            };

            int[][] queries2 = new int[2][]
            {
                new int[2]{ 2,3},
                new int[2]{ 3,2}
            };

           
            ExecuteQueriesOnMatrix(matrixInput, queries);
            ExecuteQueriesOnMatrix(matrixInput2, queries2);
        }

        public static int[][] ExecuteQueriesOnMatrix(int[][] matrixInput, int[][] queries)
        {
            int totalDataCount = matrixInput.Length * matrixInput[0].Length;
            int[] whiteList = (totalDataCount % 2 == 0)? new int[totalDataCount/2] : new int[(totalDataCount/2)+1];
            int[] blackList = new int[totalDataCount/2];
            UpdateArrayList(whiteList, blackList, matrixInput);
            int a = 0;
            int b = 0;

            for (int i = 0; i < queries.Length; i++)
            {
                int test = 0;
                for (int j = 0; j < queries[0].Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        b = blackList[queries[i][j]];
                    }
                    else
                    {
                        a = whiteList[queries[i][j]];
                    }
                    test++;
                }
                if (test == 2)
                {
                    var averageData = (a + (float)b) / 2;

                    FindLocationOfUpdate(matrixInput, a, b, averageData);
                    UpdateArrayList(whiteList, blackList, matrixInput);
                }
            }
            return matrixInput;
        }

        // update the matrix after finding the location of the number based on the query
        public static int[][] FindLocationOfUpdate(int[][] matrixInput, int a, int b, float averageData)
        {
            var whiteKeyLocation = new int[2];
            var blackKeyLocation = new int[2];
            var countWhite = 0;
            var countBlack = 0;
            var totalCount = 0;

            // loop to get location of the number based on query location
            for (int i = 0; i < matrixInput.Length; i++)
            {
                for (int j = 0; j < matrixInput[0].Length; j++)
                {
                    if (a == matrixInput[i][j] && totalCount % 2 == 0)
                    {
                        if (countWhite % 2 == 0)
                        {
                            whiteKeyLocation = new int[2] { i, j };
                        }
                        countWhite++;
                    }
                    else if (b == matrixInput[i][j] && totalCount % 2 != 0)
                    {
                        if (countBlack % 2 == 0)
                        {
                            blackKeyLocation = new int[2] { i, j };
                        }
                        countBlack++;
                    }
                    totalCount++;
                }
            }

            // if average of the number returned from the query is interger replace with int
            if (averageData == (int)averageData)
            {
                matrixInput[whiteKeyLocation[0]][whiteKeyLocation[1]] = (int)averageData;
                matrixInput[blackKeyLocation[0]][blackKeyLocation[1]] = (int)averageData;
            }
            else
            {
                if (a > b)
                {
                    matrixInput[whiteKeyLocation[0]][whiteKeyLocation[1]] = (int)Math.Ceiling(averageData);
                    matrixInput[blackKeyLocation[0]][blackKeyLocation[1]] = (int)Math.Floor(averageData);
                }
                else
                {
                    matrixInput[whiteKeyLocation[0]][whiteKeyLocation[1]] = (int)Math.Floor(averageData);
                    matrixInput[blackKeyLocation[0]][blackKeyLocation[1]] = (int)Math.Ceiling(averageData);
                }
            }

            return matrixInput;
        }

        // update the array list for white number and black numbers every time a query is triggered
        public static void UpdateArrayList(int[] whiteList, int[] blackList, int[][] matrixInput)
        {
            int count = 0;
            int whiteInput = 0;
            int blackInput = 0;
            for (int i = 0; i < matrixInput.Length; i++)
            {
                for (int j = 0; j < matrixInput[0].Length; j++)
                {
                    if (count % 2 == 0)
                    {
                        whiteList[whiteInput] = matrixInput[i][j];
                        whiteInput++;
                    }
                    else
                    {
                        blackList[blackInput] = matrixInput[i][j];
                        blackInput++;
                    }
                    count++;
                }
            }
            Array.Sort(whiteList);
            Array.Sort(blackList);
        }
    }
}
