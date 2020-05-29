using System;

namespace Recognizer
{
    internal static class My_SobelFilterTask
    {/* 
        Разберитесь, как работает нижеследующий код (называемый фильтрацией Собеля), 
        и какое отношение к нему имеют эти матрицы:
        
             | -1 -2 -1 |           | -1  0  1 |
        Sx = |  0  0  0 |      Sy = | -2  0  2 |
             |  1  2  1 |           | -1  0  1 |
        
        [url]https://ru.wikipedia.org/wiki/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80_%D0%A1%D0%BE%D0%B1%D0%B5%D0%BB%D1%8F[/url]
        
        Попробуйте заменить фильтр Собеля 3x3 на фильтр Собеля 5x5 или другой аналогичный фильтр и сравните результаты. 
        Матрицу для фильтра Собеля 5x5 и другие матрицы можно посмотреть в статье SobelScharrGradients5x5.pdf в архиве с проектом.
        Там Sx и Sy названы соответственно ОІ и Оі.
 
        Обобщите код применения фильтра так, чтобы можно было передавать ему любые матрицы, любого нечетного размера.
        Фильтры Собеля размеров 3 и 5 должны быть частным случаем. 
        После такого обобщения менять фильтр Собеля одного размера на другой должно быть легко.
        */
        //public static double[,] SobelFilter(double[,] g, double[,] sx)
        //{
        //    var width = g.GetLength(0);
        //    var height = g.GetLength(1);
        //    var result = new double[width, height];
        //    for (int x = 1; x < width - 1; x++)
        //        for (int y = 1; y < height - 1; y++)
        //        {
        //            // Вместо этого кода должно быть поэлементное умножение матриц sx и полученной транспонированием из неё sy на окрестность точки (x, y)
        //            // Такая операция ещё называется свёрткой (Сonvolution)
        //            var gx = 
        //                -g[x - 1, y - 1] - 2 * g[x, y - 1] - g[x + 1, y - 1] 
        //                + g[x - 1, y + 1] + 2 * g[x, y + 1] + g[x + 1, y + 1];
        //            var gy = 
        //                -g[x - 1, y - 1] - 2 * g[x - 1, y] - g[x - 1, y + 1] 
        //                + g[x + 1, y - 1] + 2 * g[x + 1, y] + g[x + 1, y + 1];
        //            result[x, y] = Math.Sqrt(gx * gx + gy * gy);
        //        }
        //    return result;
        //}
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            var shift = sx.GetLength(0) / 2;

            for (int x = shift; x < width - shift; x++)
                for (int y = shift; y < height - shift; y++)
                {
                    var gx = MultiplyPixelsByMatrix(g, sx, x, y, shift);
                    var gy = MultiplyPixelsByMatrix(g, sy, x, y, shift);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] matrix)
        {
            var sideLength = matrix.GetLength(0);
            var transposedMatrix = new double[sideLength, sideLength];
            for (int x = 0; x < sideLength; x++)
                for (int y = 0; y < sideLength; y++)
                    transposedMatrix[x, y] = matrix[y, x];
            return transposedMatrix;
        }

        public static double MultiplyPixelsByMatrix(double[,] pixels, double[,] matrix, int x, int y, int shift)
        {
            double result = 0;
            int sideLength = matrix.GetLength(0);
            for (int i = 0; i < sideLength; i++)
                for (int j = 0; j < sideLength; j++)
                    result += matrix[i, j] * pixels[x - shift + i, y - shift + j];
            return result;
        }
    }
}