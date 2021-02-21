using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backprop;
namespace Gutomlearning
{
    public partial class Form1 : Form
    {
        NeuralNet neuralNetwork = new NeuralNet(40000, 100, 1);
        

        public Form1()
        {
            InitializeComponent();
            neuralNetwork.loadWeights("C:\\Users\\amore\\Desktop\\3rd");
        }

        //for training
        private void button2_Click(object sender, EventArgs e)
        {
            for (int epoch = 0; epoch < Convert.ToInt32(textBox4.Text); epoch++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 20; i++)
                {
                    int count = 0;
                    Bitmap image = new Bitmap("C:\\Users\\amore\\Documents\\BackPropagation\\BackPropagation\\Images\\old\\" + i.ToString() + ".png");
                    
                    for (int width = 0; width < image.Width; width++)
                    {
                        for (int height = 0; height < image.Height; height++)
                        {
                            Color pixel = image.GetPixel(wid    th, height);
                            neuralNetwork.setInputs(count, ((pixel.R + pixel.G + pixel.B) / 3));
                            count++;
                        }
                    }
                    neuralNetwork.setDesiredOutput(0, 1.0);
                    neuralNetwork.learn();
                }
                for (int i = 0; i < 20; i++)
                {
                    int count = 0;
                    Bitmap image = new Bitmap("C:\\Users\\amore\\Documents\\BackPropagation\\BackPropagation\\Images\\young\\" + i.ToString() + ".png");

                    for (int width = 0; width < image.Width; width++)
                    {
                        for (int height = 0; height < image.Height; height++)
                        {
                            Color pixel = image.GetPixel(width, height);
                            neuralNetwork.setInputs(count, ((pixel.R + pixel.G + pixel.B) / 3));
                            count++;
                        }
                    }
                    neuralNetwork.setDesiredOutput(0, 0.0);
                    neuralNetwork.learn();
                }
                stopwatch.Stop();
                Console.WriteLine("Epoch " + epoch.ToString() + " time elapsed: " + stopwatch.Elapsed);

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            
        }

        //for identifying image
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap image = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = image;
            int count = 0;
            for (int w = 0; w < image.Width; w++)
            {
                for (int h = 0; h < image.Height; h++)
                {
                    Color pixel = image.GetPixel(w, h);
                    double grey = (pixel.R + pixel.G + pixel.B) / 3;
                    neuralNetwork.setInputs(count, grey);
                    count++;
                }
            }
            neuralNetwork.run();
           // textBox3.Text = "" + dukasico.getOuputData(0);
            double f = neuralNetwork.getOuputData(0);
            if (f < 0.5)
                label1.Text = "Result: Not a Senior Citizen";
            else
                label1.Text = "Result: Senior Citizen";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            neuralNetwork.saveWeights(saveFileDialog1.FileName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            neuralNetwork.loadWeights(openFileDialog2.FileName);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
