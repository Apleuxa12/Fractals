using System.Drawing;

namespace KDZ1
{
    class SCarpet : Fractal
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Length">Длина стороны квадрата</param>
        /// <param name="StartColor">Начальный цвет градиента</param>
        /// <param name="EndColor">Конечный цвет градиента</param>
        /// <param name="MaxRecursion">Глубина рекурсии</param>
        public SCarpet(int Length, Color StartColor, Color EndColor, int MaxRecursion) : base(Constants.CARPET_DEFAULT_LOCATION, Length, StartColor, EndColor, MaxRecursion){}

        /// <summary>
        /// См. базовый класс
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            BuildSerpinsky(g, new RectangleF(new PointF(Location.X - Length / 2, Location.Y - Length / 2), new SizeF(Length, Length)), this.MaxRecursion);
        }

        /// <summary>
        /// Рекурсивный метод, отвечающий за алгоритм отрисовки Ковра Серпинского
        /// </summary>
        /// <param name="g">Объект типа Graphics отвечающий за отрисовку</param>
        /// <param name="carpet">Квадрат для отрисовки на текущей итерации</param>
        /// <param name="iterations">Итерация для алгоритма</param>
        private void BuildSerpinsky(Graphics g, RectangleF carpet, int iterations)
        {
            g.FillRectangle(new SolidBrush(this.List[(MaxRecursion - iterations + 1) % MaxRecursion]), carpet);

            iterations--;

            if (iterations > 0)
            {
                var width = carpet.Width / 3f;
                var height = carpet.Height / 3f;

                var x1 = carpet.Left;
                var x2 = x1 + width;
                var x3 = x1 + 2f * width;

                var y1 = carpet.Top;
                var y2 = y1 + height;
                var y3 = y1 + 2f * height;

                BuildSerpinsky(g, new RectangleF(x1, y1, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x2, y1, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x3, y1, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x1, y2, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x3, y2, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x1, y3, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x2, y3, width, height), iterations);
                BuildSerpinsky(g, new RectangleF(x3, y3, width, height), iterations);
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
            return (x >= Location.X - Length / 2 && x <= Location.X + Length / 2 && y >= Location.Y - Length / 2 && y <= Location.Y + Length / 2);
        }

        /// <summary>
        /// Переопредление ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Ковёр Серпинского с центром в точке {Location.ToString()} и стороной длиной {Length}.";
        }
    }
}
