using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class My_MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
            
                var pictureSize = new int[2] { original.GetLength(0), original.GetLength(1) };
                var afterMedianFilter = new double[pictureSize[0], pictureSize[1]];
                var listMedian = new List<double>();

                for (int x = 0; x < pictureSize[0]; x++)
                    for (int y = 0; y < pictureSize[1]; y++)
                    {
                        var fieldMedian = new List<double>();
                        MedianArea(original, pictureSize, x, y, fieldMedian);
                        afterMedianFilter[x, y] = FindMedian(fieldMedian);
                    }
                return afterMedianFilter;
          }

            private static void MedianArea(double[,] original, int[] sizePicture, int x, int y, List<double> fieldMedian)
            {
                for (int width = x - 1; width < x + 2; width++)
                    for (int height = y - 1; height < y + 2; height++)
                    {
                        if (width >= 0 && height >= 0 && width < sizePicture[0] && height < sizePicture[1])
                            fieldMedian.Add(original[width, height]);
                        fieldMedian.Sort();
                    }
            }

            private static double FindMedian(List<double> listMedian)
            {
                var centerMedian = listMedian.Count / 2;
                if (listMedian.Count == 1)
                    return listMedian[0];
                if (listMedian.Count % 2 == 1)
                    return listMedian[centerMedian];
                else
                    return (listMedian[centerMedian - 1] + listMedian[centerMedian]) / 2;
            }
        }
    }

	