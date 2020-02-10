using System.Drawing;

namespace KDZ1
{
    /// <summary>
    /// Базовый класс для всех фракталов
    /// </summary>
    abstract class Fractal
    {
        protected GradientList List { get; set; }

        public int MaxRecursion { get; set; }
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public int Length { get; set; }
        public PointF Location { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Location">Начальная позиция фрактала</param>
        /// <param name="Length">Длина стороны фрактала</param>
        /// <param name="StartColor">Начальный цвет градинета</param>
        /// <param name="EndColor">Конечный цвет градиента</param>
        /// <param name="MaxRecursion">Глубина рекурсии</param>
        public Fractal(PointF Location, int Length, Color StartColor, Color EndColor, int MaxRecursion)
        {
            this.StartColor = StartColor;
            this.EndColor = EndColor;
            this.MaxRecursion = MaxRecursion;
            this.Length = Length;
            this.Location = Location;

            InitList();
        }

        /// <summary>
        /// Инициализация массива градиента
        /// </summary>
        public void InitList()
        {
            List = new GradientList(this.StartColor, this.EndColor, this.MaxRecursion);
        }

        /// <summary>
        /// Метод, отвечающий за отрисовку фракатала на экране
        /// </summary>
        /// <param name="g">Объект типа Graphics отвечающий за отрисовку</param>
        public abstract void Draw(Graphics g);


        /// <summary>
        /// Возвращает присутсвие точки (x;y) в данном фрактале
        /// </summary>
        /// <param name="x">X-координата точки</param>
        /// <param name="y">Y-координата точки</param>
        /// <returns>Присутсвие точки (x;y) в данном фрактале</returns>
        public abstract bool InBounds(float x, float y);
    }
}
