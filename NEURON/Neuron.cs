using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEURON
{
    // класс содержит формальную модель искусственного нейрона 
    internal class Neuron
    {
        public double X1 { get; set; }  // входные сигналы
        public double X2 { get; set; }
        public double W1 = 0;           // веса нейрона
        public double W2 = 0;
        public double Theta = 0;        // смещение нейрона
        public double tempw1;           // временные переменные для хранения весов и смещения
        public double tempw2;
        public double tempOffset;
        public Neuron(double w1, double w2, double theta) // конструктор - инициализирует веса и смещение
        {
            Theta = theta;
            W1 = w1;
            W2 = w2;
            tempw1 = w1;
            tempw2 = w2;
        }
        public void Activate(double x1, double x2, int d, string type, string koef)
        {
            double thresholdValueMin = 0; 
            double thresholdValueMax = 0;
            if (type == "Линейная") // в зависимости от типа активационной функции устанавливаем значения для смещения
            {
                thresholdValueMin = -0.9;
                thresholdValueMax = 0.9;
            }
            else if (type == "Сигмоидальная")
            {
                thresholdValueMin = 0.1;
                thresholdValueMax = 0.9;
            }
            else
            {
                thresholdValueMin = -1.0;
                thresholdValueMax = 1.0;
            }
            var result = Math.Sign(x1 * W1 + x2 * W2 + Theta); // Результат активации нейрона на основе переданных параметров
            double receivedValue;
            receivedValue = getFunctionValue(type, result, koef);
            if ((d == -1 && receivedValue >= thresholdValueMax) ||
                    (d == 1 && receivedValue <= thresholdValueMin)) ;
            else
            {
                if (type == "Сигмоидальная") // Если тип функции `"Сигмоидальная"`, то веса увеличиваются
                {
                    W1 += receivedValue * x1;
                    W2 += receivedValue * x2;
                }
                else // иначе — уменьшаются в соответствии с полученным значением функции активации
                {
                    W1 -= receivedValue * x1;
                    W2 -= receivedValue * x2;
                }
            }
        }
        // метод для вычисления значения функции в зависимости от типа активации нейрона
        private double getFunctionValue(string function, double result, string koef)
        {
            double result2;
            if (function == "Линейная")
            {
                result2 = Convert.ToDouble(koef) * result;
                if (result2 > 1.0) return 1.0;
                if (result2 < -1.0) return -1.0;
                return result;
            }
            else if (function == "Бип. пороговая")
            {
                return result > 0.0 ? 1.0 : -1.0;
            }
            else
            {
                result2 = 1.0 / (1.0 + Math.Pow(Math.E, -Convert.ToDouble(koef) * result));
                return result2;
            }
        }
        // Метод для восстановления весов нейрона до временных значений
        public void FixWeights()
        {
            W1 = tempw1;
            W2 = tempw2;
            Theta = tempOffset;
        }
    }
}