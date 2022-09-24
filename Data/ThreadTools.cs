using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using static Data.Configuration;
using static Data.InputTools;
using static Data.OutputTools;

namespace Data;

public class ThreadTools
{
    internal static void F1()
    {
        static double Calculate(double[] a, double[] b, double[] c, double[,] ma, double[,] me)
        {
            return b.Concat(c).Max() + (DenseMatrix.OfArray(ma) * DenseMatrix.OfArray(me) * b + a).Min();
        }

        var config = GetConfiguration();
        var n = config.N;

        double result;
        if (n <= 4)
        {
            var (a, b, c, ma, me) = GetInputsForT1(n);
            result = Calculate(a, b, c, ma, me);
        }
        else
        {
            result = config.ValueGeneration switch
            {
                ValueGenerationMode.RandomValues =>
                    Calculate(
                        GetRandomVector(n),
                        GetRandomVector(n),
                        GetRandomVector(n),
                        GetRandomMatrix(n),
                        GetRandomMatrix(n)),

                ValueGenerationMode.FillValues =>
                    Calculate(
                        GetFilledVector(n, config.FillValue),
                        GetFilledVector(n, config.FillValue),
                        GetFilledVector(n, config.FillValue),
                        GetFilledMatrix(n, config.FillValue),
                        GetFilledMatrix(n, config.FillValue))
            };
        }

        Console.WriteLine($"Thread 'T1' finished work and returned result: {result.Round(2)}\n");
    }

    internal static double[][] F2(double[,] mf, double[,] mk) =>
        (DenseMatrix.OfArray(mf).Transpose() * DenseMatrix.OfArray(mk)).ToRowArrays().OrderBy(c => c.Sum()).ToArray();

    internal static double[] F3(double[] o, double[,] mo, double[,] ms, double[,] mt) =>
        (DenseMatrix.OfArray(ms) * DenseMatrix.OfArray(mt) * (DenseMatrix.OfArray(mo) * o).OrderBy(i => i).ToArray())
        .ToArray();

    public static void StartT1()
    {
        var t = new Thread(F1, 100000)
        {
            Priority = ThreadPriority.Highest,
            Name = "T1"
        };

        t.Start();
    }

    public static void StartT2()
    {
        var config = Configuration.GetConfiguration();
        var n = config.N;

        var t = new Thread(() =>
        {
            double[][] result;
            if (n <= 4)
            {
                var (mf, mk) = GetInputsForT2(n);

                result = F2(mf, mk);
            }
            else
            {
                result = config.ValueGeneration switch
                {
                    ValueGenerationMode.RandomValues => F2(GetRandomMatrix(n), GetRandomMatrix(n)),

                    ValueGenerationMode.FillValues =>
                        F2(
                            GetFilledMatrix(n, config.FillValue),
                            GetFilledMatrix(n, config.FillValue))
                };
            }

            Console.WriteLine($"Thread 'T2' finished work and returned result:\n{MatrixToReadableString(result)}\n");
        }, 100000)
        {
            Priority = ThreadPriority.Highest,
            Name = "T2"
        };

        t.Start();
    }

    public static void StartT3()
    {
        var config = Configuration.GetConfiguration();
        var n = config.N;

        var t = new Thread(() =>
        {
            double[] result;
            if (n <= 4)
            {
                var (o, mo, ms, mt) = GetInputsForT3(n);

                result = F3(o, mo, ms, mt);
            }
            else
            {
                result = config.ValueGeneration switch
                {
                    ValueGenerationMode.RandomValues => F3(GetRandomVector(n), GetRandomMatrix(n), GetRandomMatrix(n),
                        GetRandomMatrix(n)),

                    ValueGenerationMode.FillValues =>
                        F3(
                            GetFilledVector(n, config.FillValue),
                            GetFilledMatrix(n, config.FillValue),
                            GetFilledMatrix(n, config.FillValue),
                            GetFilledMatrix(n, config.FillValue))
                };
            }

            Console.WriteLine($"Thread 'T3' finished work and returned result:\n{VectorToReadableString(result)}\n");
        }, 100000)
        {
            Priority = ThreadPriority.Highest,
            Name = "T3"
        };

        t.Start();
    }
}