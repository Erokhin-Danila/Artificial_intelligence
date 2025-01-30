namespace Perceptron
{
    internal class Neurons
    {
        public double[] w; 
        private int limit = 6; 
        private double sum = 0.0;

        public Neurons()
        {
            w = new double[25];
        }
        public void ConfigureWeights(List<double> input, int d)
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i] += d * input[i];
            }
        }
        public bool Acttivate(List<double> input)
        {
            sum = 0.0;
            for (int i = 0; i < w.Length; i++)
            {
                sum += input[i] * w[i];
            }
            return sum >= limit ? true : false;
        }
    }
}
