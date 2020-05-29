namespace Recognizer
{
	public static class My_GrayscaleTask
	{
        /* 
		 * Переведите изображение в серую гамму.
		 * 
		 * original[x, y] - массив пикселей с координатами x, y. 
		 * Каждый канал R,G,B лежит в диапазоне от 0 до 255.
		 * 
		 * Получившийся массив должен иметь те же размеры, 
		 * grayscale[x, y] - яркость пикселя (x,y) в диапазоне от 0.0 до 1.0   
		 *
		 * Используйте формулу:
		 * Яркость = (0.299*R + 0.587*G + 0.114*B) / 255
		 * 
		 * Почему формула именно такая — читайте в википедии 
		 * http://ru.wikipedia.org/wiki/Оттенки_серого
		 */
       
		public static double[,] ToGrayscale(Pixel[,] original)
             
		{
            int lengthOforiginal0 = original.GetLength(0);
            int lengthOforiginal1 = original.GetLength(1);
            double[,] grayscale = new double[lengthOforiginal0, lengthOforiginal1];
            for (int i = 0; i < lengthOforiginal0; i++)
            {
                for (int j = 0; j < lengthOforiginal1; j++)
                {
                     
                    var y = (0.299 * original[i, j].R + 0.587 * original[i, j].G + 0.114 * original[i, j].B) / 255;
                    grayscale.SetValue(y,i,j);
                }
            }

            return grayscale; 
            
		}
	}
}