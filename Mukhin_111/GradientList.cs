using System.Drawing;

namespace KDZ1
{
    /// <summary>
    /// Вспомогательный класс, который представляет собой массив градиента
    /// </summary>
    class GradientList
    {
        private Color[] list;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="start">Начальный цает градиента</param>
        /// <param name="end">Конечный цвет градиента</param>
        /// <param name="size">Количество цветов в градиенте</param>
        public GradientList(Color start, Color end, int size)
        {
            list = new Color[size];

            if(size == 1)
            {
                list[0] = start;
            }
            else
            {
                float rstep = (end.R - start.R) / (size - 1);
                float gstep = (end.G - start.G) / (size - 1);
                float bstep = (end.B - start.B) / (size - 1);
                float astep = (end.A - start.A) / (size - 1);

                for (int i = 0; i < size; i++)
                    list[i] = Color.FromArgb(start.A + (int) astep * i, start.R + (int) rstep * i, start.G + (int) gstep * i, start.B + (int) bstep * i);
            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="i">индекс в массиве цветов</param>
        /// <returns>i-ый элемент в массиве цветов</returns>
        public Color this[int i]
        {
            get
            {
                try
                {
                    return list[i];
                }
                catch
                {
                    return Color.White;
                }
            }
        }
    }
}
