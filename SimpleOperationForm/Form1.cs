using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleOperationForm
{
    public delegate double Operacion(double n1, double n2);
    public partial class Form1 : Form
    {
        Hashtable operaciones = new Hashtable();
        Operacion op;
        double num1;
        double num2;
        DateTime start;

        public Form1()
        {
            InitializeComponent();
            
            operaciones.Add("&Suma", op = (n1, n2) => n1 + n2);           
            operaciones.Add("&Resta", op = (n1, n2) => n1 - n2);      
            operaciones.Add("&Multiplicación", op = (n1, n2) => n1 * n2);            
            operaciones.Add("&División", op = (n1, n2) => n1 / n2);
            op = (n1, n2) => n1 + n2; //por defecto suma check

            start = DateTime.Now;
            timer1.Start();            
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "";
            label4.Text = "";
            RadioButton rb = (RadioButton)sender;
            op = (Operacion)operaciones[rb.Text];            
            label1.Text = rb.Tag.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            label4.Text = "";
            try
            {
                num1 = Convert.ToDouble(textBox1.Text);
                num2 = Convert.ToDouble(textBox2.Text);
                if (num2 == 0 && radioButton4.Checked)
                {
                    throw new ArgumentException();
                }
                label3.Text = op(num1, num2).ToString();
            }
            catch (FormatException)
            {
                label4.Text = "Error. Introduce números";
            }
            catch (ArgumentException)
            {
                label4.Text = "El divisor no puede ser 0";
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
            label4.Text = "";            

            //radioButton4.Enabled = textBox2.Text ==  "0"? false : true; //--- Problema: necesaria conversión aquí tamén             
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan temp = DateTime.Now - start;   //timespan: intervalo de tiempo
            this.Text = string.Format("{0:mm\\:ss}", temp);
        }
    }
}
