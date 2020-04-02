using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static double[,] EnterArray(int n)
        {
            double[,] arr = new double[n, n];         
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                 arr[i, j] = int.Parse(Console.ReadLine().ToString());
                }
            }
            return arr;
        }
        static double[,] GenerationArray(int n)
        {
            double[,] arr = new double[n, n];
            var r = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i,j] = r.Next(30);
                }
            }
            return arr;
        }
        static double[] GenerationVector(int n)
        {
            double[] arr = new double[n];
            var r = new Random();
            for (int i = 0; i < n; i++)
            {
                
                    arr[i] = r.Next(30);
                
            }
            return arr;
        }
        public static double[] MulVectorNumber(double [] vector,int number)
        {
            for (int i = 0; i < vector.GetLength(0); i++)
                vector[i] *= number;
            return vector;
        }
        public static double[] MulArrVector(double[,] A, double[] B)
        {
            double[] res = new double[B.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    res[col] += A[row, col] * B[row];
                }
            }
            return res;
        }
        public static double[,] MulArrs(double[,] A, double[,] B)
        {
            
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int i = 0; i < B.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(0); j++)
                {
                    res[i, j] = 0;
                    for (int k = 0; k < B.GetLength(0); k++)
                        res[i, j] += A[i, k] * B[k, j];

                    
                }
            return res;
        }
        public static double[,] MinArrs(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    res[row, col] = A[row, col] - B[row,col];
                }
            }
            return res;
        }
        public static double[] AddVectors(double[] A, double[] B)
        {
            double[] res = new double[B.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
               
                
                    res[i] = A[i] + B[i];
                
            }
            return res;
        }

        public static void RandomFunction(int n)
        {

            double[,] A = null;
            Thread thread_createA = new Thread(() => A = GenerationArray(n));
            List<double> b = new List<double>();
            double[,] C2 = new double[n, n];
            thread_createA.Start();

            Thread create_b = new Thread(() => {
                for (int i = 1; i <= n; i++)
                {
                    double item = 0;
                    if (i % 2 == 0)
                    {
                        item = ((3.0) / ((i * i) + 3.0));

                    }
                    else
                    {
                        item = 3.0 / (double)i;
                    }

                    b.Add(item);

                }
            });
            create_b.Start();


            thread_createA.Join();

            double[] y1 = null;
            create_b.Join();
            Thread createY1 = new Thread(() => {
                y1 = MulArrVector(A, b.ToArray()); foreach (var item in y1)
                {
                    Console.WriteLine(item);
                }
            });
            createY1.Start();
            //foreach (var item in b)
            //{
            //    Console.WriteLine(item);
            //}
            /////////
            double[,] A1 = null;
            Thread thread_createA1 = new Thread(() => A1 = GenerationArray(n));
            thread_createA1.Start();
            double[] b1 = null;
            double[] c1 = null;
            Thread thread_create_b1 = new Thread(() => b1 = GenerationVector(n));
            Thread thread_create_c1 = new Thread(() => c1 = GenerationVector(n));
            thread_create_b1.Start();
            thread_create_c1.Start();

            double[] y2 = null;
            thread_create_b1.Join();
            thread_create_c1.Join();
            thread_createA1.Join();
            Thread thread_create_y2 = new Thread(() => {
                y2 = MulVectorNumber(b1, 3);
                y2 = AddVectors(y2, c1);
                y2 = MulArrVector(A1, y2);
            });
            thread_create_y2.Start();
            ////////////////////
            double[,] A2 = null;
            Thread thread_createA2 = new Thread(() => A2 = GenerationArray(n));
            thread_createA2.Start();

            double[,] B2 = null;
            Thread thread_createB2 = new Thread(() => B2 = GenerationArray(n));
            thread_createB2.Start();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = (1.0 / (i + 1 + j + 1)) * 2.0;
                }

            }
            thread_createB2.Join();

            double[,] Y3 = MinArrs(B2, C2);
            Thread thread_createY3 = new Thread(() => {
                Y3 = MinArrs(B2, C2);
                Y3 = MulArrs(A2, Y3);
            });
            thread_createY3.Start();
            thread_createA2.Join();
            double? K = null;
            Thread thread_create_K = new Thread(() => {
                var r = new Random();
                K = 0.001 * r.Next(10);
            });
            thread_create_K.Start();
            thread_create_K.Join();
            Console.WriteLine();
            Console.WriteLine(K);
        }
        public static void WithoutRandomFunction(int n)
        {

            double[,] A = null;
            Thread thread_createA = new Thread(() => A = EnterArray(n));
            List<double> b = new List<double>();
            double[,] C2 = new double[n, n];
            thread_createA.Start();

            Thread create_b = new Thread(() => {
                for (int i = 1; i <= n; i++)
                {
                    double item = 0;
                    if (i % 2 == 0)
                    {
                        item = ((3.0) / ((i * i) + 3.0));

                    }
                    else
                    {
                        item = 3.0 / (double)i;
                    }

                    b.Add(item);

                }
            });
            create_b.Start();


            thread_createA.Join();

            double[] y1 = null;
            create_b.Join();
            Thread createY1 = new Thread(() => {
                y1 = MulArrVector(A, b.ToArray()); foreach (var item in y1)
                {
                    Console.WriteLine(item);
                }
            });
            createY1.Start();
            //foreach (var item in b)
            //{
            //    Console.WriteLine(item);
            //}
            /////////
            double[,] A1 = null;
            Thread thread_createA1 = new Thread(() => A1 = GenerationArray(n));
            thread_createA1.Start();
            double[] b1 = null;
            double[] c1 = null;
            Thread thread_create_b1 = new Thread(() => b1 = GenerationVector(n));
            Thread thread_create_c1 = new Thread(() => c1 = GenerationVector(n));
            thread_create_b1.Start();
            thread_create_c1.Start();

            double[] y2 = null;
            thread_create_b1.Join();
            thread_create_c1.Join();
            thread_createA1.Join();
            Thread thread_create_y2 = new Thread(() => {
                y2 = MulVectorNumber(b1, 3);
                y2 = AddVectors(y2, c1);
                y2 = MulArrVector(A1, y2);
            });
            thread_create_y2.Start();
            ////////////////////
            double[,] A2 = null;
            Thread thread_createA2 = new Thread(() => A2 = GenerationArray(n));
            thread_createA2.Start();

            double[,] B2 = null;
            Thread thread_createB2 = new Thread(() => B2 = GenerationArray(n));
            thread_createB2.Start();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = (1.0 / (i + 1 + j + 1)) * 2.0;
                }

            }
            thread_createB2.Join();

            double[,] Y3 = MinArrs(B2, C2);
            Thread thread_createY3 = new Thread(() => {
                Y3 = MinArrs(B2, C2);
                Y3 = MulArrs(A2, Y3);
            });
            thread_createY3.Start();
            thread_createA2.Join();
            double? K = null;
            Thread thread_create_K = new Thread(() => {
                var r = new Random();
                K = 0.001 * r.Next(10);
            });
            thread_create_K.Start();
            thread_create_K.Join();
            Console.WriteLine();
            Console.WriteLine(K);
        }

        static void Main(string[] args)
        {
            Console.Write("n=");
            int n =int.Parse(Console.ReadLine().ToString());
            Console.WriteLine("Random?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            int random = int.Parse(Console.ReadLine().ToString());



            //double[,] A = null;
            //Thread thread_createA = new Thread(() => A = GenerationArray(n));
            //List<double> b = new List<double>();
            //double[,] C2 = new double[n, n];
            //thread_createA.Start();

            //Thread create_b = new Thread(() => {
            //    for (int i = 1; i <= n; i++)
            //    {
            //        double item = 0;
            //        if (i % 2 == 0)
            //        {
            //            item = ((3.0) / ((i * i) + 3.0));

            //        }
            //        else
            //        {
            //            item = 3.0 / (double)i;
            //        }

            //        b.Add(item);

            //    }
            //});
            //create_b.Start();


            //thread_createA.Join();

            //double[] y1 = null;
            //create_b.Join();
            //Thread createY1 = new Thread(() => {
            //    y1 = MulArrVector(A, b.ToArray()); foreach (var item in y1)
            //    {
            //        Console.WriteLine(item);
            //    }
            //});
            //createY1.Start();
            ////foreach (var item in b)
            ////{
            ////    Console.WriteLine(item);
            ////}
            ///////////
            //double[,] A1 = null;
            //Thread thread_createA1 = new Thread(() => A1 = GenerationArray(n));
            //thread_createA1.Start();
            //double[] b1 = null;
            //double[] c1 = null;
            //Thread thread_create_b1 = new Thread(() => b1 = GenerationVector(n));
            //Thread thread_create_c1 = new Thread(() => c1 = GenerationVector(n));
            //thread_create_b1.Start();
            //thread_create_c1.Start();

            //double[] y2 = null;
            //thread_create_b1.Join();
            //thread_create_c1.Join();
            //thread_createA1.Join();
            //Thread thread_create_y2 = new Thread(() => {
            //    y2 = MulVectorNumber(b1, 3);
            //    y2 = AddVectors(y2, c1);
            //    y2 = MulArrVector(A1, y2);
            //});
            //thread_create_y2.Start();
            //////////////////////
            //double[,] A2 = null;
            //Thread thread_createA2 = new Thread(() => A2 = GenerationArray(n));
            //thread_createA2.Start();

            //double[,] B2 = null;
            //Thread thread_createB2 = new Thread(() => B2 = GenerationArray(n));
            //thread_createB2.Start();

            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //    {
            //        C2[i, j] = (1.0 / (i + 1 + j + 1)) * 2.0;
            //    }

            //}
            //thread_createB2.Join();

            //double[,] Y3 = MinArrs(B2, C2);
            //Thread thread_createY3 = new Thread(() => {
            //    Y3 = MinArrs(B2, C2);
            //    Y3 = MulArrs(A2, Y3);
            //});
            //thread_createY3.Start();
            //thread_createA2.Join();
            //double? K = null;
            //Thread thread_create_K = new Thread(() => {
            //    var r = new Random();
            //    K = 0.001 * r.Next(10);
            //});
            //thread_create_K.Start();
            //thread_create_K.Join();
            //Console.WriteLine();
            //Console.WriteLine(K);
        }
    }
}
