using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NEURON
{
    internal class NeuronInputPoint
    {
        public double X { get; set; } // координата x
        public double Y { get; set; } // координата y
        public int Class { get; set; } // выбор соот. плоскости d

        // конструктор инициализируются свойства `X`, `Y` и `Class`
        public NeuronInputPoint(Point point, int Class)
        {
            X = point.X;
            Y = point.Y;
            this.Class = Class;
        }
    }
}