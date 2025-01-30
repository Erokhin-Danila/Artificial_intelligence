namespace Perceptron
{
    public partial class Perceptron : Form
    {
        public double[,] number = new double[5, 5];
        List<Neurons> neurons;
        public double[][,] numbers =
        {
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,1,0,1,0},
                         {0,1,0,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0}
                            },
             new double[5,5]{
                         {0,0,0,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,1,1,1,0},
                         {0,1,0,0,0},
                         {0,1,1,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,1,1,1,0}
                            },
             new double[5,5]{
                         {0,1,0,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,1,0,0,0},
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,1,1,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,1,0,0,0},
                         {0,1,1,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0},
                         {0,0,0,1,0}
                            },
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0}},
             new double[5,5]{
                         {0,1,1,1,0},
                         {0,1,0,1,0},
                         {0,1,1,1,0},
                         {0,0,0,1,0},
                         {0,1,1,1,0}}
        };
        public double[][,] weight =
        {
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5],
            new double[5, 5]
        };

        public Perceptron()
        {
            neurons = new List<Neurons>();
            for (int i = 0; i < 10; i++)
            {
                neurons.Add(new Neurons());
            }
            InitializeComponent();
        }
        private void ChangeColor(object button)
        {
            var btn = (Button)button;
            var name = btn.Name.Split('_');
            var i = Convert.ToInt32(name[1]);
            var j = Convert.ToInt32(name[2]);
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.Black;
                this.number[i, j] = 1;
            }
            else
            {
                btn.BackColor = Color.White;
                this.number[i, j] = 0;
            }
            button = btn;
        }
        public void secondcalc(double[][,] Weight, double[][,] Number)
        {
            int circle = Convert.ToInt32(textBox1.Text);
            double a, b;
            do
            {
                for (int i = 0; i < 10; i++)
                {
                    a = truecount(number) / truecount(numbers[i]); b = falsecount(number) / falsecount(numbers[i]);
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (number[j, k] != numbers[i][j, k])
                                weight[i][j, k] += a * b * meancount(j, k, number[j, k]);
                            else
                                weight[i][j, k] += a * b * meancount(j, k, number[j, k]);
                        }
                    }
                }
                circle--;
            }
            while (circle > 0);
            int number_of_number = 0;
            double error = 10000;
            for (int i = 0; i < 10; i++)
            {
                double temp_error = 0;
                for (int l = 0; l < 5; l++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (number[l, j] == numbers[i][l, j])
                        {
                            temp_error -= weight[i][l, j];
                        }
                        else
                        {
                            temp_error += weight[i][l, j] * 1.5;
                        }
                    }

                }
                if (error > temp_error)
                {
                    number_of_number = i;
                    error = temp_error;
                }
            }
            label1.Text = Convert.ToString(number_of_number);
        }
        private void button_00_Click(object sender, EventArgs e)
        {
            ChangeColor(sender);
        }
        public double truecount(double[,] matr)
        {
            double count = 0;
            foreach (int exp in matr)
            {
                if (exp == 1) count++;
            }
            return count;
        }
        public double falsecount(double[,] matr)
        {
            double count = 0;
            foreach (int exp in matr)
            {
                if (exp == 0) count++;
            }
            return count;
        }
        public double meancount(int x, int y, double mean)
        {
            double count = 0; double rez;
            foreach (double[,] arr in numbers)
            {
                if (arr[x, y] == mean) count++;
            }
            rez = count / 10;
            return rez;
        }
        public double activationn(double[,] inp, double[,] weightt)
        {
            double n = 0.05;
            double result2 = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    result2 += n * inp[i, j] * weightt[i, j];
                }
            }
            if (result2 > 5) return 1;
            if (result2 < 5) return 0;
            return 0;
        }
        public void increase(double[,] inp, double[,] weightt)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (inp[i, j] == 1)
                        weightt[i, j]++;
                }
            }
        }
        public void decrease(double[,] inp, double[,] weightt)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (inp[i, j] == 1)
                        weightt[i, j]--;
                }
            }
        }
        private List<double> ListFromArray(int k)
        {
            var result = new List<double>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    result.Add(numbers[k][i, j]);
                }
            }
            return result;
        }
        private List<double> ListFromArrayRes()
        {
            var result = new List<double>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    result.Add(number[i, j]);
                }
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Алгоритм Хэбба")
            {
                int circle = 0;
                if (textBox1.Text != "")
                    circle = Convert.ToInt32(textBox1.Text);
                do
                {
                    for (int n = 0; n < 10; n++)
                    {
                        if (activationn(numbers[n], weight[n]) == 1)
                            decrease(numbers[n], weight[n]);
                        else increase(numbers[n], weight[n]);
                    }
                    circle--;
                } while (circle > 0);
                secondcalc(numbers, weight);
            }
            else if (comboBox1.Text == "Алгоритм Розенблатта")
            {
                for (int i = 0; i < 10; i++)
                {
                    var X = ListFromArray(i);
                    var res = neurons[i].Acttivate(X);
                    if (res == true)
                    {
                        neurons[i].ConfigureWeights(X, -1);
                    }
                    else
                    {
                        neurons[i].ConfigureWeights(X, 1);
                    }
                }

                var Y = ListFromArrayRes();
                label1.Text = "";
                for (int i = 0; i < neurons.Count; i++)
                {
                    var res = neurons[i].Acttivate(Y);
                    if (res == true)
                    {
                        label1.Text = i.ToString();
                    }
                }
            }
            else
            {
                int circle = Convert.ToInt32(textBox1.Text);
                double a, b;
                do
                {
                    for (int i = 0; i < 10; i++)
                    {
                        a = truecount(number) / truecount(numbers[i]);
                        b = falsecount(number) / falsecount(numbers[i]);
                        for (int j = 0; j < 5; j++)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                if (number[j, k] != numbers[i][j, k])
                                    weight[i][j, k] += a * b * meancount(j, k, number[j, k]);
                                else
                                    weight[i][j, k] += a * b * meancount(j, k, number[j, k]);
                            }
                        }
                    }
                    circle--;
                } while (circle > 0);
                int number_of_number = 0;
                double error = 10000;
                for (int i = 0; i < 10; i++)
                {
                    double temp_error = 0;
                    for (int l = 0; l < 5; l++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (number[l, j] == numbers[i][l, j])
                            {
                                temp_error -= weight[i][l, j];
                            }
                            else
                            {
                                temp_error += weight[i][l, j] * 1.5;
                            }
                        }

                    }
                    if (error > temp_error)
                    {
                        number_of_number = i; error = temp_error;
                    }
                }
                label1.Text = Convert.ToString(number_of_number);
            }
        }
        // Загрузка в файл
        private void button3_Click(object sender, EventArgs e)
        {
            string data = "";
            foreach (double[,] arr in weight)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        data += Convert.ToString(arr[i, j]);

                        if (!(j == 4 && i == 4)) data += " ";

                    }
                }
                data += "\n";
            }
            string newdata = "";
            for (int i = 0; i < data.Length - 2; i++)
            {
                newdata += data[i];
            }
            string path;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.ShowDialog(); path = dialog.FileName;
                File.WriteAllText(path, newdata);
            }
        }
        // Загрузка из файла
        private void button4_Click(object sender, EventArgs e)
        {
            string path;
            string data;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.ShowDialog();
                path = dialog.FileName;
                data = File.ReadAllText(path);
            }
            string[] arr = data.Split('\n');
            string[][] in_arr = new string[10][];
            for (int i = 0; i < 10; i++)
            {
                in_arr[i] = arr[i].Split(' ');
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        weight[i][j, k] = Convert.ToDouble(in_arr[i][j * 5 + k]);
                    }
                }
            }
        }
    }
}