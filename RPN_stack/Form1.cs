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
using System.IO;

namespace RPN_stack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = RPN.Parse(textBox1.Text);
        }        
    }

    class RPN
    {
        private static bool IsNum(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private static byte Priority(string s)
        {
            if (s == "^")
                return 3;
            else if (s == "*" || s == "/")
                return 2;
            else if (s == "+" || s == "-")
                return 1;
            else
                return 0;
        }

        public static string Parse(string s)
        {
            string[] words = s.Split(' ');
            Stack stack = new Stack();
            string el = "";
            string output = "";
            int i = 0;

            while (i < words.Length)
            {
                el = words[i];

                if (IsNum(el) == true)
                {
                    output += el + " ";
                    i++;
                    continue;
                }
                else if (el == "(")
                {
                    stack.Push("(");
                    i++;
                    continue;
                }
                else if (el == ")")
                {
                    while (stack.Peek().ToString() != "(")
                        output += stack.Pop() + " ";

                    stack.Pop();
                    i++;
                    continue;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        while (Priority(el) <= Priority(stack.Peek().ToString()))
                        {
                            output += stack.Pop() + " ";

                            if (stack.Count == 0)
                                break;
                        }
                    }

                    stack.Push(el);

                    i++;
                    continue;
                }
            }

            while (stack.Count != 0)
            {
                output += stack.Pop() + " ";
            }

            return output;
        }
    }
}
