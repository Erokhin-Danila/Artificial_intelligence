using System.Drawing;

namespace NEURON
{
    public partial class Form1 : Form
    {
        // �������� ������ ����� NeuronInputPoint
        List<NeuronInputPoint> dots = new List<NeuronInputPoint>();
        public Form1()
        {
            InitializeComponent();
            //������������� ����������� ��� pictureBox1 ��� ����� Bitmap ������������� �������
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //����� ������ ��� �������� ������������ ���� �� pictureBox1.
            Create_Coordinate_Axis();
            pictureBox1.MouseClick += pictureBox1_MouseClick; // ����� ���������� ����� �� �����
        }
        // ����� ��� �������� ������������ ���� �� pictureBox1
        private void Create_Coordinate_Axis()
        {
            //��������� ���� (Pen) � ������ ������ � �������� 1
            var pen = new Pen(Color.Black, 1f);
            //��������� ������ `Graphics` ��� ��������� �� ����������� `pictureBox1`
            var graphics = Graphics.FromImage(pictureBox1.Image);
            // ��������� �����, �������������� ����� �����������
            var center = new Point(pictureBox1.Width, pictureBox1.Height);
            var pen_axis = new Pen(Color.Black, 2f);
            // �������������� � ������������ ���
            graphics.DrawLine(pen_axis, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
            graphics.DrawLine(pen_axis, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            int xcount = 10; // ������������� ���. ������� �� ����
            int ycount = 10;
            // � ������� ������ ������������� ���� ������� 
            for (int i = 0; i < xcount; i++)
            {
                graphics.DrawLine(pen, new Point(i * pictureBox1.Width / xcount, 0), new Point(i * pictureBox1.Width / xcount, pictureBox1.Height));
            }
            for (int i = 0; i < ycount; i++)
            {
                graphics.DrawLine(pen, new Point(0, i * pictureBox1.Height / ycount), new Point(pictureBox1.Width, i * pictureBox1.Height / ycount));
            }
        }
        // ���������� ������� ����� ���� �� pictureBox1
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var graphics = pictureBox1.CreateGraphics(); // ������ ��� ��������� �� `pictureBox1`
            var center = e.Location;    // ���������� ����� �� ����� ����
            var pointSize = 5;  // ������ �����
            SolidBrush brush;
            var pointClass = 1; // d - ���������� ��������� ��� ������ ������ �����5
            if (radioButton1.Checked) // ����� ����� ����� � ����������� �� radioButton
            {
                brush = new SolidBrush(Color.Orange);
            }
            else
            {
                pointClass = -1;
                brush = new SolidBrush(Color.Purple);
            }
            // �������� ����� ��������� ����� � ���������� ��� �� ������
            graphics.DrawEllipse(new Pen(brush, 4f), new Rectangle(center.X - pointSize, center.Y - pointSize, pointSize * 2, pointSize * 2));
            graphics.FillEllipse(brush, new Rectangle(center.X - pointSize, center.Y - pointSize, pointSize * 2, pointSize * 2));
            // ���������� ����� ����� �� ����� ���� � ����������. ������������
            dots.Add(new NeuronInputPoint(e.Location, pointClass));
            textBox5.Text = Math.Round((double)e.Location.X / 56 - 5, 1).ToString(); // ����������� ���������� �����
            textBox6.Text = Math.Round((5 - (double)e.Location.Y / 49), 1).ToString();
        }
        // ���������� ������� �� ������ button1
        private void button1_Click_1(object sender, EventArgs e)
        {
            // �������� ������� ������� � ����������� �� ��������� ����� �����
            var neuron = new Neuron(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));
            // �������� ������� Graphics ��� ��������� �� pictureBox1
            var graphics = pictureBox1.CreateGraphics();
            //�������� ���� (Pen) ��� ��������� � ������ ������ � �������� 2
            var pen = new Pen(Color.Black, 2f);
            // ��������� ������� ��� ������ ����� �� ������ � �������� ��� ����. ����������
            for (int i = 0; i < 10; i++)
            {
                foreach (var item in dots)
                {
                    neuron.Activate(item.X / 56.0 - 5, 5 - item.Y / 49.0, item.Class, comboBox1.Text, textBox4.Text);
                }
            }
            //������ ������������� ������ � ��������
            var k = -(neuron.W1 / neuron.W2);
            var b = -(neuron.Theta / neuron.W2);
            var x1 = -5.0;
            var x2 = 5.0;
            var y1 = (int)((5 - (k * x1 + b)) * 49);
            var y2 = (int)((5 - (k * x2 + b)) * 49);

            neuron.FixWeights(); // ��������� ����� �������
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height); // ����� BitMap
            Create_Coordinate_Axis(); // ����� ������������ ���
            graphics = Graphics.FromImage(pictureBox1.Image); //����� ������ � ������������ ��������������
            // ��������� ����� � �� ��������
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

            var result = MessageBox.Show("�������� ���������!");
        }
        //���������� ������� �� ������ button2
        private void button2_Click_1(object sender, EventArgs e)
        {
            // ����� BitMap
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            // ������ ��� ����� �����
            dots = new List<NeuronInputPoint>();
            // ����� �������� ������������ ����
            Create_Coordinate_Axis();
        }
    }
}