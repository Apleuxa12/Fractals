using System;
using System.Drawing;

namespace KDZ1
{
    /// <summary>
    /// Статический класс, в котором хранятся некоторые константы, используемые в программе.
    /// </summary>
    static class Constants
    {
        /// <summary>
        /// Максимальная глубина рекурсии по умолчанию
        /// </summary>
        public static readonly int ITERATIONS_DEFAULT_MAXIMUM = 11;
        /// <summary>
        /// Максимальное увеличение по умолчанию
        /// </summary>
        public static readonly int ZOOM_DEFAULT_MAXIMUM = 7;

        /// <summary>
        /// Максимальная глубина рекурсии для Треугольника Серпинского по умолчанию
        /// </summary>
        public static readonly int ITERATIONS_TRIANGLE_MAXIMUM = ITERATIONS_DEFAULT_MAXIMUM;
        /// <summary>
        /// Максимальная глубина рекурсии для Ковра Серпинского по умолчанию
        /// </summary>
        public static readonly int ITERATIONS_CARPET_MAXIMUM = 6;
        /// <summary>
        /// Максимальная глубина рекурсии для Множества Кантора по умолчанию
        /// </summary>
        public static readonly int ITERATIONS_KANTOR_MAXIMUM = 6;

        /// <summary>
        /// Высота правилльного треугольника, поделённая на дллину
        /// </summary>
        public static readonly float TRIANGLE_HEIGHT = (float)(Math.Sqrt(3) / 2);
        /// <summary>
        /// Длина Треугольника Серпинского по умолчанию
        /// </summary>
        public static readonly int TRIANGLE_DEFAULT_LENGTH = 120;
        /// <summary>
        /// Положение Треугольника Серпинского по умолчанию
        /// </summary>
        public static readonly PointF TRIANGLE_DEFAULT_LOCATION = new PointF(300 + (float)(TRIANGLE_DEFAULT_LENGTH / 2), 300 + (float)((2 * TRIANGLE_HEIGHT * TRIANGLE_DEFAULT_LENGTH) / 3));
        /// <summary>
        /// Длина Ковра Серпинского по умолчанию
        /// </summary>
        public static readonly int CARPET_DEFAULT_LENGTH = 100;
        /// <summary>
        /// Положение Ковра Серпинского по умолчанию
        /// </summary>
        public static readonly PointF CARPET_DEFAULT_LOCATION = new PointF(300f, 300f);
        /// <summary>
        /// Длина Множества Кантора по умолчанию
        /// </summary>
        public static readonly int KANTOR_DEFAULT_LENGTH = 100;
        /// <summary>
        /// Положение Множества Кантора по умолчанию
        /// </summary>
        public static readonly PointF KANTOR_DEFAULT_LOCATION = new PointF(30f, 30f);
        /// <summary>
        /// Высота одной итерации Множества Кантора
        /// </summary>
        public static readonly float KANTOR_HEIGHT = 30f;
        /// <summary>
        /// Подпись для вывода ошибки
        /// </summary>
        public static readonly string ALERT_CAPTION = "ALERT";
        /// <summary>
        /// Цвет ColorDialog'ов по умолчанию
        /// </summary>
        public static readonly Color PICTUREBOX_DEFAULT_COLOR = Color.White;
        /// <summary>
        /// Формат для сохранения фрактала
        /// </summary>
        public static readonly string SAVE_FORMAT = ".bmp";
    }
}
