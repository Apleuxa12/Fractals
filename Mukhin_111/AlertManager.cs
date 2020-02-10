using System.Windows.Forms;

namespace KDZ1
{
   /// <summary>
   /// Класс, отвечающий за вывод ошибок, возникающий в результате работе программы
   /// </summary>
   static  class AlertManager
    {
        /// <summary>
        /// Выводит MessageBox с ошбкой
        /// </summary>
        /// <param name="msg">Текст ошбки</param>
        /// <param name="caption">Подпись MessageBox</param>
        public static void ShowMessageBox(string msg, string caption)
        {
            MessageBox.Show(msg, caption);
        }

        /// <summary>
        /// Перегрузка №1
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMessageBox(string msg)
        {
            ShowMessageBox(msg, Constants.ALERT_CAPTION);
        }

        /// <summary>
        /// Перегрузка №2
        /// </summary>
        public static void ShowMessageBox()
        {
            ShowMessageBox("");
        }

    }
}
