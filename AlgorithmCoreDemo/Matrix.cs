using System.Diagnostics;

namespace AlgorithmCoreDemo
{
    /// <summary>
    /// 矩阵相关
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// https://leetcode-cn.com/problems/rotate-image/description/
        /// 
        /// https://www.jianshu.com/p/6541ecb599ea
        /// </summary>
        /// <param name="matrix"></param>
        public static int[,] Rotate90Degree(this int[,] matrix)
        {
            int tmp;
            int n = matrix.GetLength(0);
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i + 1; j < n - i; j++)
                {
                    tmp = matrix[i, j];
                    matrix[i, j] = matrix[n - 1 - j, i];
                    matrix[n - 1 - j, i] = matrix[n - 1 - i, n - 1 - j];
                    matrix[n - 1 - i, n - 1 - j] = matrix[j, n - 1 - i];
                    matrix[j, n - 1 - i] = tmp;
                }
            }

            return matrix;
        }


        public static int[,] Rotate180Degree(this int[,] matrix)
        {
            int tmp;
            int n = matrix.GetLength(0);
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i + 1; j < n - i; j++)
                {
                    tmp = matrix[i, j];
                    matrix[i, j] = matrix[n - i - 1, n - j - 1];
                    matrix[n - i - 1, n - j - 1] = tmp;
                }
            }

            return matrix;
        }


        public static int[,] Print(this int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var temp = string.Empty;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    temp += matrix[i, j] + ",";
                }

                Trace.WriteLine(temp);
            }

            return matrix;
        }

    }
}
