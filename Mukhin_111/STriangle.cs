using System.Drawing;

namespace KDZ1
{
    /// <summary>
    /// Класс, реализующимй треугольник Серпинского
    /// </summary>
    class STriangle : Fractal
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Length">Длина стороны треугольника</param>
        /// <param name="StartColor">Начальный цвет градиента</param>
        /// <param name="EndColor">Конечный цвет градиента</param>
        /// <param name="MaxRecursion">Глубина рекурсии</param>
        public STriangle(int Length, Color StartColor, Color EndColor, int MaxRecursion) : base(Constants.TRIANGLE_DEFAULT_LOCATION, Length, StartColor, EndColor, MaxRecursion){}

        /// <summary>
        /// См. базовый класс
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            float h = Constants.TRIANGLE_HEIGHT;

            PointF p1 = new PointF(Location.X - Length / 2, Location.Y + h * Length / 3);
            PointF p2 = new PointF(Location.X, Location.Y - (2 * h * Length) / 3);
            PointF p3 = new PointF(Location.X + Length / 2, Location.Y + h* Length / 3);

            BuildSerpinsky(g, p1,p2, p3, this.MaxRecursion);
        }

        /// <summary>
        /// Рекурсивный метод, отвечающий за алгоритм отрисовки Треугольника Серпинского
        /// </summary>
        /// <param name="g">Объект типа Graphics отвечающий за отрисовку</param>
        /// <param name="p1">Первая точка треугольника на текущей итерации</param>
        /// <param name="p2">Вторая точка треугольника на текущей итерации</param>
        /// <param name="p3">Третья точка треугольника на текущей итерации</param>
        /// <param name="iterations">Итерация для алгоритма</param>
        private void BuildSerpinsky(Graphics g, PointF p1, PointF p2, PointF p3, int iterations)
        {
            g.FillPolygon(new SolidBrush(this.List[(MaxRecursion - iterations + 1) % MaxRecursion]), new PointF[] { p1, p2, p3 });

            iterations--;
            if (iterations > 0)
            {
                var p12 = new PointF(p1.X + (p2.X - p1.X) / 2, p1.Y + (p2.Y - p1.Y) / 2);
                var p23 = new PointF(p2.X + (p3.X - p2.X) / 2, p2.Y + (p3.Y - p2.Y) / 2);
                var p13 = new PointF(p1.X + (p3.X - p1.X) / 2, p1.Y + (p3.Y - p1.Y) / 2);

                BuildSerpinsky(g, p1, p12, p13, iterations);
                BuildSerpinsky(g, p12, p2, p23, iterations);
                BuildSerpinsky(g, p13, p23, p3, iterations);
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
            float h = Constants.TRIANGLE_HEIGHT;

            float x1 = Location.X - Length / 2;
            float y1 = Location.Y + h * Length / 3;
            float x2 = Location.X;
            float y2 = Location.Y - (2 * h * Length) / 3;
            float x3 = Location.X + Length / 2;
            float y3 = Location.Y + h * Length / 3;


            float a = (x - x1) * (y2 - y) - (x - x2) * (y1 - y), b = (x - x2) * (y3 - y) - (x - x3) * (y2 - y), c = (x - x3) * (y1 - y) - (x - x1) * (y3 - y);
            return ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0));
        }

        /// <summary>
        /// Переопредление ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Треугольник Серпиского с центром в точке {Location.ToString()} и стороной длиной {Length}.";
        }
    }
}
