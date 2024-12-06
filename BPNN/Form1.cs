using System;
using System.Windows.Forms;
using Backprop;

namespace BPNN
{
    public partial class Form1 : Form
    {
        NeuralNet nn;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nn = new NeuralNet(4, 100, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] inputs = new double[16, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 },
            };

            double[] outputs = new double[16]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1 
            };

            bool trainingComplete = false;
            int maxIterations = 10000;
            int currentIteration = 0;

            while (!trainingComplete && currentIteration < maxIterations)
            {
                trainingComplete = true;

                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        nn.setInputs(j, inputs[i, j]);
                    }

                    nn.setDesiredOutput(0, outputs[i]);
                    nn.run();
                    nn.learn();

                    double actualOutput = nn.getOuputData(0);
                    if (Math.Abs(actualOutput - outputs[i]) > 0.1)
                    {
                        trainingComplete = false;
                    }
                }

                currentIteration++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nn.setInputs(0, Convert.ToDouble(textBox1.Text));
            nn.setInputs(1, Convert.ToDouble(textBox2.Text));
            nn.setInputs(2, Convert.ToDouble(textBox3.Text));
            nn.setInputs(3, Convert.ToDouble(textBox4.Text));
            nn.run();
            textBox5.Text = "" + nn.getOuputData(0);
        }
    }
}