using System.Drawing;
using System.IO;

namespace KDZ1
{
    /// <summary>
    /// Класс отвечающий за создание файлов вне программы.
    /// </summary>
    static class OutputManager
    {
        private static int counter = 0;

        public static string lastSaved = "";

        /// <summary>
        /// Метод, который сохраняет в данной директории картинку .bmp
        /// </summary>
        /// <param name="bmp">Картинка в формате Bitmap, которую надо сохранить.</param>
        /// <param name="filename">Имя, под которым надо сохранить файл.</param>
        public static void SaveInFile(Bitmap bmp, string filename)
        {
            try
            {
                string fname = filename + (++counter).ToString() + Constants.SAVE_FORMAT;
                bmp.Save(fname);
                lastSaved = fname;
            }
            catch (IOException e)
            {
                AlertManager.ShowMessageBox(e.Message);
            }
        }
    }
}
