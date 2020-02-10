using System.Drawing;

namespace KDZ1
{
    /// <summary>
    /// Класс, реализующий Множество Кантора
    /// </summary>
    class KantorArray : Fractal
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Length">Длина стороны треугольника</param>
        /// <param name="StartColor">Начальный цвет градиента</param>
        /// <param name="EndColor">Конечный цвет градиента</param>
        /// <param name="MaxRecursion">Глубина рекурсии</param>
        public KantorArray(int Length, Color StartColor, Color EndColor, int MaxRecursion) : base(Constants.KANTOR_DEFAULT_LOCATION, Length, StartColor, EndColor, MaxRecursion){}

        /// <summary>
        /// См. базовый класс
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            BuildKantor(g, (int)Location.X, (int)Location.Y, this.Length, (int)Constants.KANTOR_HEIGHT, MaxRecursion);
        }

        /// <summary>
        /// Рекурсивный метод, отвечающий за алгоритм отрисовки Множества Кантора
        /// </summary>
        /// <param name="g">Объект типа Graphics отвечающий за отрисовку</param>
        /// <param name="x">Начальная X-координата прямой на данной итерации</param>
        /// <param name="y">Начальная Y-координата прямой на данной итерации</param>
        /// <param name="width">Высота прямой на данной итерации</param>
        /// <param name="height">Ширина прямой на данной итерации</param>
        /// <param name="iterations">Итерация для алгоритма</param>
        private void BuildKantor(Graphics g, int x, int y, int width, int height, int iterations)
        {
            float indent = Constants.KANTOR_HEIGHT * 2;

            g.FillRectangle(new SolidBrush(List[this.MaxRecursion - iterations]), new Rectangle(new Point(x, y), new Size(width, height)));

            iterations--;

            if (iterations > 0)
            {
                BuildKantor(g, x + (int) (width * 2 / 3), y + (int)indent, (int)width / 3, height, iterations);
                BuildKantor(g, x, y + (int)(indent), width / 3, height, iterations);
            }
        }

        /// <summary>
        /// См. базовый класс
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool InBounds(float x, float y)
        {
            return (x >= Location.X && x <= Location.X + Length && y >= Location.Y && y <= Location.Y + Constants.KANTOR_HEIGHT * (MaxRecursion + 1));
        }

        /// <summary>
        /// Переопредление ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Множетсво Кантора с началом в точке {Location.ToString()} и стороной длиной {Length}.";
        }
    }
}
