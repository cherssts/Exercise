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

namespace Excercise_project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Declaring variables
        //All the variables will be declared here
        string textbox1Data = "";
        double _data1 = 0;
        string textbox2Data = "";
        double _data2 = 0;
        string _operator = "";
        double _mresult = 0;

        #endregion
        #region constructor
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        #endregion

        #region Start
        public void Start()
        {
            //Reset all datas
            _data1 = 0;
            _data2 = 0;
            _mresult = 0;
            textbox1Data = "";
            textbox2Data = "";
            _operator = "";
            Textbox1.Text = String.Empty;
            Textbox2.Text = String.Empty;
            Textbox3.Text = String.Empty;


            //At the start make sure to let the Textbox1 to be enabled
            Textbox1.Focus();
            Textbox1.IsReadOnly = false;
            Textbox1.IsEnabled = true;

            //Here making sure that the Textboxes2,3 are not going to be available to 
            //be taking inputs before input 1 hits enter
            Textbox2.IsReadOnly = true;
            Textbox2.IsEnabled = false;
            Textbox3.IsReadOnly = true;
            Textbox3.IsEnabled = false;
        }



        #endregion

        #region Event Area
        private void Textbox1_EnterDectect(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Making sure that the input has no spaces
                textbox1Data = Textbox1.Text;
                if (!double.TryParse(textbox1Data, out _data1))
                {
                    Textbox1.Focus();
                    Textbox1.Text = String.Empty;
                }
                else if (double.TryParse(textbox1Data, out _data1))
                {
                    Textbox1.Text.Replace(" ", "");
                    Textbox1.IsReadOnly = true;
                    Textbox1.IsEnabled = false;
                    Textbox2.IsReadOnly = false;
                    Textbox2.IsEnabled = true;
                    Textbox2.Focus();
                }

            }

        }

        private void Textbox2_KeyDown(object sender, KeyEventArgs e)
        
        {
            if (e.Key == Key.Enter && Textbox2.SelectionStart == 0)
            {
                MessageBoxResult _msg = MessageBox.Show("Please input something");
                Textbox2.Focus();
            }


            switch (e.Key)
            {
                case Key.OemPlus://'+'
                    textbox2Data = Textbox2.Text;
                    _operator = "+";
                    TryParseNum(textbox2Data, Textbox2, out _data2);
                    Textbox2.Text = Textbox2.Text.Replace("+", "");
                    SimpleCalc(_data1, _data2, _operator);
                    Textbox3.Text = _mresult.ToString();
                    Textbox3.IsReadOnly = true;
                    break;

                case Key.OemMinus: // '-'
                    textbox2Data = Textbox2.Text;
                    _operator = "-";
                    TryParseNum(textbox2Data, Textbox2, out _data2);
                    Textbox2.Text = Textbox2.Text.Replace("-", "");
                    SimpleCalc(_data1, _data2, _operator);
                    Textbox3.Text = _mresult.ToString();
                    Textbox3.IsReadOnly = true;
                    break;

                case Key.X: //Because '*' CANT BE FOUND
                    textbox2Data = Textbox2.Text;
                    _operator = "*";
                    TryParseNum(textbox2Data, Textbox2, out _data2);
                    Textbox2.Text = Textbox2.Text.Replace("x", "");
                    Textbox2.Text = Textbox2.Text.Replace("X", "");
                    SimpleCalc(_data1, _data2, _operator);
                    Textbox3.Text = _mresult.ToString();
                    Textbox3.IsReadOnly = true;
                    break;

                case Key.Oem2: //This is Divide '/'
                    textbox2Data = Textbox2.Text;
                    _operator = "/";
                    TryParseNum(textbox2Data, Textbox2, out _data2);
                    Textbox2.Text = Textbox2.Text.Replace("/", "");
                    SimpleCalc(_data1, _data2, _operator);
                    Textbox3.Text = _mresult.ToString();
                    Textbox3.IsReadOnly = true;
                    Textbox2.Text = Textbox2.Text.Replace("/", "");
                    break;

            }



            //This might be use less
            //if(e.Key != Key.Enter || e.Key != Key.OemMinus || e.Key != Key.OemPlus || e.Key != Key.Multiply || e.Key != Key.Divide)
            //{
            //    MessageBoxResult _msg = MessageBox.Show("The input is not correct, please try '+','-','*','/' ! ");
            //    Textbox2.Focus();
            //}


        }
        private void Textbox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Start();
                Textbox1.Focus();
            }
        }




        #endregion

        #region Fucntions

        private void Text3UnlockAndLockText2()
        {
            Textbox2.Text.Replace(" ", "");
            Textbox2.IsReadOnly = true;
            Textbox2.IsEnabled = false;
            Textbox3.IsReadOnly = true;
            Textbox3.IsEnabled = true;
            Textbox3.Focus();
        }

        public void TryParseNum(in string a, in TextBox c, out double b)
        {

            bool _ifParseSuc = false;

            if (!double.TryParse(a, out b) || _ifParseSuc == false)
            {
                if (b.GetType() == 1d.GetType())
                {
                    _ifParseSuc = true;
                }
                else
                {
                    c.Text = String.Empty;
                    MessageBoxResult _msg = MessageBox.Show("Please input something");
                    _ifParseSuc = false;
                    c.Focus();
                }

            }

            //If everything is ok, then the 3rd textbox is gonna open
            if (_ifParseSuc == true)
            {
                _ifParseSuc = true;
                Text3UnlockAndLockText2();
            }

        }


        public double SimpleCalc(double a, double b, string _op)
        {
            switch (_op)
            {
                case "+":
                    return _mresult = a + b;
                case "-":
                    return _mresult = a - b;
                case "*":
                    return _mresult = a * b;
                case "/":
                    if (b == 0)
                    {
                        MessageBoxResult _msg = MessageBox.Show("You Cannot Divide things by 0!");
                        Textbox2.IsEnabled = true;
                        Textbox2.IsReadOnly = false;
                        Textbox2.Focus();
                        Textbox2.Text = String.Empty;
                        Textbox3.IsEnabled = false;
                        Textbox3.Text = String.Empty;
                        Textbox2.Text = Textbox2.Text.Replace("/", "");
                        return double.NaN;
                    }
                    else return _mresult = a / b;


                default:
                    throw new InvalidCastException("Nah");
            }
        }





        #endregion


    }

}
