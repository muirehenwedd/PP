using System.Text;
using MathNet.Numerics;

namespace Data
{
    internal static class OutputTools
    {
        internal static string VectorToReadableString(double[] vector)
        {
            if (vector.Length > 4)
            {
                var builder = new StringBuilder();

                builder.Append(String.Join('\t', vector[..2].Select(i => i.Round(2))));
                builder.Append("\t...\t");
                builder.Append(String.Join('\t', vector[^2..].Select(i => i.Round(2))));

                return builder.ToString();
            }
            return String.Join(", ", vector);
        }

        internal static string MatrixToReadableString(double[][] matrix)
        {
            if (matrix.Length > 4)
            {
                var builder = new StringBuilder();

                builder.AppendLine(VectorToReadableString(matrix[0]));
                builder.AppendLine(VectorToReadableString(matrix[1]));

                builder.AppendLine("...\t...\t...\t...\t...");

                builder.AppendLine(VectorToReadableString(matrix[^1]));
                builder.AppendLine(VectorToReadableString(matrix[^2]));

                return builder.ToString();
            }

            return String.Join('\n', matrix.Select(VectorToReadableString));
        }
    }
}