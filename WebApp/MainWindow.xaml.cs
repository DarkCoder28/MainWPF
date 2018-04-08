using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace WebApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean hasAttachment = false;
        private String[] attachNames = new string[] { "" };
        private void setAttachment()
        {
            hasAttachment = true;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    if (openFileDialog1.Multiselect)
                    {
                        attachNames = openFileDialog1.FileNames;
                    }
                    else
                    {
                        attachNames = new String[] { openFileDialog1.FileName };
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void send(object sender, RoutedEventArgs e)
        {
            bool ifssl = Convert.ToBoolean(ssl.IsChecked);
            new Email(username.Text, password.Password, from.Text, to.Text, server.Text, port.Text, ifssl, subject.Text, body.Text, hasAttachment, attachNames);
        }

        private void attachment_Click(object sender, RoutedEventArgs e)
        {
            setAttachment();
        }

        enum Operation { NONE, ADD, SUBTRACT, MULTIPLY, DIVIDE }
        Operation operation = Operation.NONE;
        String last = "";
        private Boolean isDouble(String m)
        {
            return Regex.IsMatch(m, "[0-9.]");
        }
        private void outLogAdd(String m, String m2)
        {
            ListBoxItem itm = new ListBoxItem();
            itm.Content = m;
            output_log.Items.Add(itm);

            ListBoxItem itm2 = new ListBoxItem();
            itm2.Content = "    " + m2;
            output_log.Items.Add(itm2);
        }


        private void clear_Click(object sender, EventArgs e)
        {
            display.Text = "";
        }

        private void add_Click(object sender, EventArgs e)
        {
            operation = Operation.ADD;
            last = display.Text;
            display.Text = "";
        }

        private void subtract_Click(object sender, EventArgs e)
        {
            operation = Operation.SUBTRACT;
            last = display.Text;
            display.Text = "";
        }

        private void multiply_Click(object sender, EventArgs e)
        {
            operation = Operation.MULTIPLY;
            last = display.Text;
            display.Text = "";
        }

        private void divide_Click(object sender, EventArgs e)
        {
            operation = Operation.DIVIDE;
            last = display.Text;
            display.Text = "";
        }

        private void zero_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "0";
            display.Text = disp;
        }

        private void dot_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            if (disp.Contains(".")) { return; }
            disp = disp + ".";
            display.Text = disp;
        }

        private void one_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "1";
            display.Text = disp;
        }

        private void two_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "2";
            display.Text = disp;
        }

        private void three_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "3";
            display.Text = disp;
        }

        private void four_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "4";
            display.Text = disp;
        }

        private void five_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "5";
            display.Text = disp;
        }

        private void six_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "6";
            display.Text = disp;
        }

        private void seven_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "7";
            display.Text = disp;
        }

        private void eight_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "8";
            display.Text = disp;
        }

        private void nine_Click(object sender, EventArgs e)
        {
            String disp = display.Text;
            disp = disp + "9";
            display.Text = disp;
        }

        private void equals_Click(object sender, EventArgs e)
        {
            if (!isDouble(last) || !isDouble(display.Text))
            {
                MessageBox.Show("Error: All Expressions Must Be Numaric");
                return;
            }
            Double x = Convert.ToDouble(last);
            Double y = Convert.ToDouble(display.Text);
            Double output = y;
            if (operation == Operation.ADD)
            {
                output = x + y;
                outLogAdd(last + "+" + display.Text + "=", output.ToString());
            }
            if (operation == Operation.SUBTRACT)
            {
                output = x - y;
                outLogAdd(last + "-" + display.Text + "=", output.ToString());
            }
            if (operation == Operation.MULTIPLY)
            {
                output = x * y;
                outLogAdd(last + "*" + display.Text + "=", output.ToString());
            }
            if (operation == Operation.DIVIDE)
            {
                output = x / y;
                outLogAdd(last + "/" + display.Text + "=", output.ToString());
            }
            last = Convert.ToString(output);
            display.Text = Convert.ToString(output);
        }

        private void clearLog_Click(object sender, RoutedEventArgs e)
        {
            output_log.Items.Clear();
        }

        private void copyLogItem_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem li = (ListBoxItem)output_log.SelectedItem;
            String m = (String)li.Content;
            if (!m.Contains("="))
            {
                m = m.Substring(4);
            }
            Clipboard.SetText(m);
        }

        private void deleteLogItem_Click(object sender, RoutedEventArgs e)
        {
            object li = output_log.SelectedItem;
            output_log.Items.Remove(li);
        }
    }
}
