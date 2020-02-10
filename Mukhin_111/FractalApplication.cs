using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KDZ1
{
    /// <summary>
    /// Класс, реализующий форму и еще много основного функционала программы.
    /// </summary>
    public partial class FractalApplication : Form
    {
        /// <summary>
        /// Объекты Bitmap и Graphics - для рисования
        /// </summary>
        private Bitmap bitmap;
        private Graphics gr;

        /// <summary>
        /// Фрактал, инициализируется при нажатии на соответствующую кнопку
        /// </summary>
        private Fractal fractal;

        /// <summary>
        /// Поля для инициализации фракатала, считываются в форме
        /// </summary>
            private Color startColor;
            private Color endColor;
            private int iters;
            private int zoom;
            private PointF location;

        /// <summary>
        /// Конструктор формы, инициализируем некоторые начальные поля
        /// </summary>
        public FractalApplication()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

            //Для более красивой отрисовки на экране
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            iters = 1;
            zoom = 1;

            startColor = Color.White;
            endColor = Color.White;
        }

        /// <summary>
        /// Метод очистки формы
        /// </summary>
        private void Clear()
        {
            pictureBox1.BackColor = Constants.PICTUREBOX_DEFAULT_COLOR;
            pictureBox2.BackColor = Constants.PICTUREBOX_DEFAULT_COLOR;
            trackBar1.Value = trackBar1.Minimum;
            trackBar1.Maximum = Constants.ITERATIONS_DEFAULT_MAXIMUM;
            trackBar1.Minimum = 1;
            trackBar2.Value = trackBar2.Minimum;
            trackBar2.Maximum = Constants.ITERATIONS_DEFAULT_MAXIMUM;
            trackBar2.Minimum = 1;
            label3.Text = $"Глубина рекурсии - {trackBar1.Value}";
            label4.Text = $"Увеличение рисунка - {trackBar2.Value}";

            startColor = Color.White;
            endColor = Color.White;

            iters = 1;
            zoom = 1;

            fractal = null;
            pictureBox.Image = null;
        }

        /// <summary>
        /// Кнопка "Очистить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// Метод обновления, в нем изменяем положение, глубину итераций, размер фракатала, если пользователь захочет; вызывается перед каждой отрисовкой
        /// </summary>
        private new void Update()
        {
            if (fractal == null)
                return;

            if (Triangle())
            {
                fractal.Length = Constants.TRIANGLE_DEFAULT_LENGTH * zoom;
                trackBar1.Maximum = Constants.ITERATIONS_TRIANGLE_MAXIMUM;
            }
            else if (Carpet())
            {
                fractal.Length = Constants.CARPET_DEFAULT_LENGTH * zoom;
                trackBar1.Maximum = Constants.ITERATIONS_CARPET_MAXIMUM;
            }
            else if (Kantor())
            {
                fractal.Length = Constants.KANTOR_DEFAULT_LENGTH * zoom;
                trackBar1.Maximum = Constants.ITERATIONS_KANTOR_MAXIMUM;
            }
            else
            {
                trackBar1.Maximum = Constants.ITERATIONS_DEFAULT_MAXIMUM;
                trackBar2.Minimum = Constants.ZOOM_DEFAULT_MAXIMUM;
            }

            fractal.MaxRecursion = iters;
            fractal.StartColor = startColor;
            fractal.EndColor = endColor;
            fractal.Location = location;
            fractal.InitList();

            label3.Text = $"Глубина рекурсии - {trackBar1.Value}";
            label4.Text = $"Увеличение рисунка - {trackBar2.Value}";

            if (trackBar1.Value > trackBar1.Maximum)
                trackBar1.Value = trackBar1.Maximum;

            if (trackBar2.Value > trackBar2.Maximum)
                trackBar2.Value = trackBar2.Maximum;

            iters = trackBar1.Value;
            zoom = trackBar2.Value;
        }
        
        /// <summary>
        /// Метод отрисовки фракатала на экране
        /// </summary>
        private void Draw()
        {
            gr = Graphics.FromImage(bitmap);

            gr.FillRectangle(new SolidBrush(pictureBox.BackColor), 0, 0, pictureBox.Width, pictureBox.Height);

            //Сглаживание
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            Update();

            if (fractal != null)
            {
                fractal.Draw(gr);
            }

            pictureBox.Image = bitmap;
        }
        
        /// <summary>
        /// Вызывается при изменении размера экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

                Update();

                if (fractal != null)
                {
                    Draw();
                }
            }
            catch (ArgumentException)
            {
                Clear();
            }
        }

        /// <summary>
        /// ColorDialog для начального цвета градиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox1.BackColor = colorDialog1.Color;
            startColor = colorDialog1.Color;

            if (fractal != null)
            {
                Draw();
            }
        }

        /// <summary>
        /// ColorDialog для конечного цвета градиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox2.BackColor = colorDialog2.Color;
            endColor = colorDialog2.Color;

            if (fractal != null)
            {
                Draw();
            }
        }

        /// <summary>
        /// TrackBar для глубины итераций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int v = ((TrackBar) sender).Value;
            label3.Text = $"Глубина рекурсии - {v}";
            iters = v;

            if(fractal != null)
                Draw();
        }

        /// <summary>
        /// TrackBar для увелечения картинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int v = ((TrackBar)sender).Value;
            label4.Text = $"Увеличение рисунка - {v}";
            zoom = v;

            if (fractal != null)
                Draw();
        }

        /// <summary>
        /// Кнопка "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (fractal == null)
            {
                AlertManager.ShowMessageBox("Чтобы сохранить фрактал, его сначала надо нарисовать");
                return;
            }

            OutputManager.SaveInFile(bitmap, "image");
            AlertManager.ShowMessageBox($"Ваш файл был сохранен под именем {OutputManager.lastSaved}", "OutputManager");
        }

        /// <summary>
        /// Рисуем Треугольник Серпинского
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void triangleButton_Click(object sender, EventArgs e)
        {
            if (Triangle())
                return;

            if (Carpet() || Kantor())
            {
                Clear();
            }

            if (startColor == Color.Empty || endColor == Color.Empty)
            {
                AlertManager.ShowMessageBox("Укажите все цвета градиента!");
                return;
            }

            if (fractal == null)
            {
                fractal = new STriangle(Constants.TRIANGLE_DEFAULT_LENGTH, startColor, endColor, iters);
                location = fractal.Location;
            }

            Draw();
        }

        /// <summary>
        /// Рисуем Ковёр Серпинского
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carpetButton_Click(object sender, EventArgs e)
        {
            if (Carpet())
                return;

            if (Triangle() || Kantor())
            {
                Clear();
            }

            if (startColor == Color.Empty || endColor == Color.Empty)
            {
                AlertManager.ShowMessageBox("Укажите все цвета градиента!");
                return;
            }

            if (fractal == null)
            {
                fractal = new SCarpet(Constants.CARPET_DEFAULT_LENGTH, startColor, endColor, iters);
                location = fractal.Location;
            }

            Draw();
        }

        /// <summary>
        /// Рисуем Множество Кантора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kantorButton_Click(object sender, EventArgs e)
        {
            if (Kantor())
                return;

            if (Triangle() || Carpet())
            {
                Clear();
            }

            if (startColor == Color.Empty || endColor == Color.Empty)
            {
                AlertManager.ShowMessageBox("Укажите все цвета градиента!");
                return;
            }

            if (fractal == null)
            {
                fractal = new KantorArray(Constants.KANTOR_DEFAULT_LENGTH, startColor, endColor, iters);
                location = fractal.Location;
            }

            Draw();
        }

        /// <summary>
        /// </summary>
        /// <returns>Был ли наш фрактал инициализирован как Треугольник Серпиского</returns>
        private bool Triangle()
        {
            if (fractal == null)
                return false;

            return fractal is STriangle;
        }

        /// <summary>
        /// </summary>
        /// <returns>Был ли наш фрактал инициализирован как Ковёр Серпиского</returns>
        private bool Carpet()
        {
            if (fractal == null)
                return false;

            return fractal is SCarpet;
        }

        /// <summary>
        /// </summary>
        /// <returns>Был ли наш фрактал инициализирован как Множество Кантора</returns>
        private bool Kantor()
        {
            if (fractal == null)
                return false;

            return fractal is KantorArray;
        }

        //Блок кода далее и до конца отвечает за перемещение фракатала по экрану с помощью мышки

        /// <summary>
        /// Последний клик по фракталу
        /// </summary>
        private Point last;

        /// <summary>
        /// "Захвачен" ли фрактал мышкой
        /// </summary>
        private bool captured;

        /// <summary>
        /// Если пользователь нажал на экран мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (fractal == null)
                return;

            //Если нажатие было в пределах фрактала, то сохраняем координаты клика и делаем фрактал "захваченнным"
            if (fractal.InBounds(e.X, e.Y))
            {
                last.X = e.X;
                last.Y = e.Y;
                captured = true;
            }
            else
            {
                captured = false;
            }
        }

        /// <summary>
        /// Если пользователь двигает мышкой по экрану
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //Если фрактал "захвачен" и вообще существует
            if (fractal != null && pictureBox.Capture && captured)
            {
                //считаем изменение в движении мышки
                int dx = e.X - last.X;
                int dy = e.Y - last.Y;

                //изменяем положение фрактала
                ChangeFractalLocation(dx, dy);

                last.X = e.X;
                last.Y = e.Y;

                //отрисовываем фрактал заново (из-за этого фрактал при даже незначительной глубине итераций будет двигаться с низкой частотой кадров)
                Draw();
            }
        }

        /// <summary>
        /// Изменяем положение фрактала
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        private void ChangeFractalLocation(int dx, int dy)
        {
            if (fractal == null)
                return;

            PointF newLocation = new PointF(location.X + dx,location.Y + dy);

            int len = fractal.Length;
            
           //Не даем фракаталу выйти за пределы экрана

            if (Triangle())
            {
                if(newLocation.X - len / 2 < 0)
                    newLocation.X = len / 2;
                if (newLocation.X + len / 2 > pictureBox.Width)
                    newLocation.X = pictureBox.Width - len / 2;

                if (newLocation.Y - (2 * Constants.TRIANGLE_HEIGHT * len) / 3 < 0)
                    newLocation.Y = (2 * Constants.TRIANGLE_HEIGHT * len) / 3;
                if (newLocation.Y + Constants.TRIANGLE_HEIGHT * len / 3 > pictureBox.Height)
                    newLocation.Y = pictureBox.Height - Constants.TRIANGLE_HEIGHT * len / 3;
            }
            else if (Carpet())
            {

                if (newLocation.X - len / 2 < 0)
                    newLocation.X = len/ 2;
                if (newLocation.X + len / 2 > pictureBox.Width)
                    newLocation.X = pictureBox.Width - len / 2;

                if (newLocation.Y - len / 2 < 0)
                    newLocation.Y = len / 2;
                if (newLocation.Y + len / 2 > pictureBox.Height)
                    newLocation.Y = pictureBox.Height - len / 2;

            }
            else if (Kantor())
            {
                if (newLocation.X < 0)
                    newLocation.X = 0;
                if (newLocation.X + len > pictureBox.Width)
                    newLocation.X = pictureBox.Width - len;

                if (newLocation.Y < 0)
                    newLocation.Y = 0;
                if (newLocation.Y + Constants.KANTOR_HEIGHT * (2 * fractal.MaxRecursion - 1) > pictureBox.Height)
                    newLocation.Y = pictureBox.Height - Constants.KANTOR_HEIGHT * (2 * fractal.MaxRecursion - 1);
            }

            location = newLocation;
        }
    }
}
