using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace 梁若为_3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnstop.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnstart.IsEnabled = false;
            btnstop.IsEnabled = true;
            Myclass.IsStop = false;
            textBox.Text = "";
            Myclass c = new Myclass(textBox);
            MyData state = new MyData { Message = "a", Info = "\n线程1已终止" };
            Thread thread1 = new Thread(c.MyMethod);
            thread1.IsBackground = true;
            thread1.Start(state);
             state = new MyData { Message = "b", Info = "\n线程2已终止" };
            Thread thread2 = new Thread(c.MyMethod);
            thread2.IsBackground = true;
            thread2.Start(state);
            state = new MyData { Message = "c", Info = "\n线程3已终止" };
            ThreadPool.QueueUserWorkItem(new WaitCallback(c.MyMethod),state);
            state = new MyData { Message = "d", Info = "\n线程4已终止" };
            ThreadPool.QueueUserWorkItem(new WaitCallback(c.MyMethod), state);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            btnstart.IsEnabled = true;
            btnstop.IsEnabled = false;
            Myclass.IsStop = true;
        }
        public class Myclass
        {
            public static volatile bool IsStop;
            TextBox textBox;

            public Myclass(TextBox textBox)
            {
                this.textBox = textBox;
            }
            public void MyMethod(object obj)
            {
                MyData state = obj as MyData;
                while (IsStop == false) 
                {
                    AddMessage(state.Message);
                    Thread.Sleep(100);

                }
                AddMessage(state.Info);
            }
            private void AddMessage(string s)
            {
                textBox.Dispatcher.Invoke(() =>
                {
                    textBox.Text += s;
                }

                );
            }

        }
        public class MyData
        {
            public string Info { get; set; }
            public string Message { get; set;}
        }


    }
}
