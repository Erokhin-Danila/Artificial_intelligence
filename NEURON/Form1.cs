using System.Drawing;

namespace NEURON
{
    public partial class Form1 : Form
    {
        // Создание списка точек NeuronInputPoint
        List<NeuronInputPoint> dots = new List<NeuronInputPoint>();
        public Form1()
        {
            InitializeComponent();
            //Устанавливает изображение для pictureBox1 как новый Bitmap определенного размера
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Вызов метода для создания координатных осей на pictureBox1.
            Create_Coordinate_Axis();
            pictureBox1.MouseClick += pictureBox1_MouseClick; // метод добавления точек на форму
        }
        // Метод для создания координатных осей на pictureBox1
        private void Create_Coordinate_Axis()
        {
            //Создается перо (Pen) с черным цветом и толщиной 1
            var pen = new Pen(Color.Black, 1f);
            //Создается объект `Graphics` для рисования на изображении `pictureBox1`
            var graphics = Graphics.FromImage(pictureBox1.Image);
            // Создается точка, представляющая центр изображения
            var center = new Point(pictureBox1.Width, pictureBox1.Height);
            var pen_axis = new Pen(Color.Black, 2f);
            // Горизонтальная и вертикальная ось
            graphics.DrawLine(pen_axis, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
            graphics.DrawLine(pen_axis, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            int xcount = 10; // устанавливаем кол. делений по осям
            int ycount = 10;
            // С помощью циклов прорисовываем наши деления 
            for (int i = 0; i < xcount; i++)
            {
                graphics.DrawLine(pen, new Point(i * pictureBox1.Width / xcount, 0), new Point(i * pictureBox1.Width / xcount, pictureBox1.Height));
            }
            for (int i = 0; i < ycount; i++)
            {
                graphics.DrawLine(pen, new Point(0, i * pictureBox1.Height / ycount), new Point(pictureBox1.Width, i * pictureBox1.Height / ycount));
            }
        }
        // Обработчик события клика мыши на pictureBox1
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var graphics = pictureBox1.CreateGraphics(); // объект для рисования на `pictureBox1`
            var center = e.Location;    // координаты точек по клику мыши
            var pointSize = 5;  // размер точки
            SolidBrush brush;
            var pointClass = 1; // d - определяет плоскость для каждой группы точек5
            if (radioButton1.Checked) // Выбор цвета точек в зависимости от radioButton
            {
                brush = new SolidBrush(Color.Orange);
            }
            else
            {
                pointClass = -1;
                brush = new SolidBrush(Color.Purple);
            }
            // рисуется точка выбраного цвета и заливаятся тем же цветом
            graphics.DrawEllipse(new Pen(brush, 4f), new Rectangle(center.X - pointSize, center.Y - pointSize, pointSize * 2, pointSize * 2));
            graphics.FillEllipse(brush, new Rectangle(center.X - pointSize, center.Y - pointSize, pointSize * 2, pointSize * 2));
            // добавление новой точки по клику мыши с соответств. координатами
            dots.Add(new NeuronInputPoint(e.Location, pointClass));
            textBox5.Text = Math.Round((double)e.Location.X / 56 - 5, 1).ToString(); // указываются координаты точки
            textBox6.Text = Math.Round((5 - (double)e.Location.Y / 49), 1).ToString();
        }
        // Обработчик нажатия на кнопку button1
        private void button1_Click_1(object sender, EventArgs e)
        {
            // Создание объекта нейрона с параметрами из текстовых полей формы
            var neuron = new Neuron(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));
            // Создание объекта Graphics для рисования на pictureBox1
            var graphics = pictureBox1.CreateGraphics();
            //Создание пера (Pen) для рисования с черным цветом и толщиной 2
            var pen = new Pen(Color.Black, 2f);
            // Активация нейрона для каждой точки из списка и передача ему необ. параметров
            for (int i = 0; i < 10; i++)
            {
                foreach (var item in dots)
                {
                    neuron.Activate(item.X / 56.0 - 5, 5 - item.Y / 49.0, item.Class, comboBox1.Text, textBox4.Text);
                }
            }
            //расчёт коэффициэнтов наклон и смещение
            var k = -(neuron.W1 / neuron.W2);
            var b = -(neuron.Theta / neuron.W2);
            var x1 = -5.0;
            var x2 = 5.0;
            var y1 = (int)((5 - (k * x1 + b)) * 49);
            var y2 = (int)((5 - (k * x2 + b)) * 49);

            neuron.FixWeights(); // Коррекция весов нейрона
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height); // новый BitMap
            Create_Coordinate_Axis(); // новая координатная ось
            graphics = Graphics.FromImage(pictureBox1.Image); //новая прямая с вычесленными коэффициэнтами
            // отрисовка точек с их заливкой
            graphics.DrawLine(new Pen(Color.Purple, 3f), new Point(0, y1), new Point(pictureBox1.Width, y2)); 
            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].Class == 1)
                {
                    graphics.DrawEllipse(new Pen(Color.Orange, 4f), new Rectangle(Convert.ToInt32(dots[i].X) - 5, Convert.ToInt32(dots[i].Y) - 5, 10, 10));
                    graphics.FillEllipse(new SolidBrush(Color.Orange), new Rectangle(Convert.ToInt32(dots[i].X) - 5, Convert.ToInt32(dots[i].Y) - 5, 10, 10));
                }
                else
                {
                    graphics.DrawEllipse(new Pen(Color.Purple, 4f), new Rectangle(Convert.ToInt32(dots[i].X) - 5, Convert.ToInt32(dots[i].Y) - 5, 10, 10));
                    graphics.FillEllipse(new SolidBrush(Color.Purple), new Rectangle(Convert.ToInt32(dots[i].X) - 5, Convert.ToInt32(dots[i].Y) - 5, 10, 10));
                }
            }

            var result = MessageBox.Show("Обучение завершено!");
        }
        //Обработчик нажатия на кнопку button2
        private void button2_Click_1(object sender, EventArgs e)
        {
            // новый BitMap
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            // список для новых точек
            dots = new List<NeuronInputPoint>();
            // метод создания координатных осей
            Create_Coordinate_Axis();
        }
    }
}