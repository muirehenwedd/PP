namespace Data;

internal static class InputTools
{
    private static readonly object Locker = new();

    internal static double[] GetFilledVector(int length, double value)
    {
        var array = new double[length];
        Array.Fill(array, value);

        return array;
    }

    internal static double[,] GetFilledMatrix(int size, double value)
    {
        var matrix = new double[size, size];

        for (var i = 0; i <= matrix.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= matrix.GetUpperBound(1); j++)
            {
                matrix[i, j] = value;
            }
        }

        return matrix;
    }

    internal static double[] GetRandomVector(int length) =>
        Enumerable.Range(0, length).Select(_ => Random.Shared.NextDouble()).ToArray();

    internal static double[,] GetRandomMatrix(int size)
    {
        var matrix = new double[size, size];

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                matrix[i, j] = Random.Shared.NextDouble();
            }
        }
        return matrix;
    }

    private static double[] GetVectorFromInput(int length, string vectorName)
    {
        Console.WriteLine(
            $"Please, enter vector '{vectorName}' of length {length}. Numbers must be separated with whitespaces or commas. Then press 'Enter'.");

        var substrings = Console.ReadLine()!.Trim(' ', ',').Split(' ', ',',
            StringSplitOptions.TrimEntries ^ StringSplitOptions.RemoveEmptyEntries);

        if (substrings.Length != length)
            throw new ArgumentException("Wrong vector length!");

        Console.WriteLine();
        return substrings.Select(Convert.ToDouble).ToArray();
    }

    private static double[,] GetMatrixFromInput(int size, string matrixName)
    {
        var matrix = new double[size, size];

        Console.WriteLine(
            $"Please, enter matrix '{matrixName}' of size {size}X{size}. Numbers must be separated with whitespaces or commas. Press 'Enter' after each row input.");

        for (var i = 0; i < size; i++)
        {
            var substrings = Console.ReadLine()!.Trim(' ', ',').Split(' ', ',',
                StringSplitOptions.TrimEntries ^ StringSplitOptions.RemoveEmptyEntries);

            if (substrings.Length != size)
                throw new ArgumentException("Wrong row length!");

            var numbers = substrings.Select(Convert.ToDouble).ToArray();

            for (var j = 0; j < size; j++)
            {
                matrix[i, j] = numbers[j];
            }
        }

        Console.WriteLine();
        return matrix;
    }

    internal static (double[], double[], double[], double[,], double[,]) GetInputsForT1(int n)
    {
        lock (Locker)
        {
            Console.WriteLine("[Inputs for T1]");

            return (GetVectorFromInput(n, "A"),
                GetVectorFromInput(n, "B"),
                GetVectorFromInput(n, "C"),
                GetMatrixFromInput(n, "MA"),
                GetMatrixFromInput(n, "ME"));
        }
    }

    internal static (double[,], double[,]) GetInputsForT2(int n)
    {
        lock (Locker)
        {
            Console.WriteLine("[Inputs for T1]");

            return (GetMatrixFromInput(n, "MF"), GetMatrixFromInput(n, "MK"));
        }
    }

    internal static (double[], double[,], double[,], double[,]) GetInputsForT3(int n)
    {
        lock (Locker)
        {
            Console.WriteLine("[Inputs for T1]");

            return (GetVectorFromInput(n, "O"),
                GetMatrixFromInput(n, "MO"),
                GetMatrixFromInput(n, "MS"),
                GetMatrixFromInput(n, "MT"));
        }
    }
}