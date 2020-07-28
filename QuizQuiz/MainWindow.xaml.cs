using Quiz;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace QuizQuiz
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Quiz();
        }

        private Random r = new Random();
        private void Quiz()
        {
            Questions.Instance.Load();
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                new SelectableQuestionWindow().ShowDialog();
                //if (r.Next(2) == 1) new QuestionWindow().ShowDialog();
                //else new SelectableQuestionWindow().ShowDialog();
                Task.Factory.StartNew(delegate
                {
                    try
                    {
                        var DelayRange = File.ReadAllText("./DelayRange.txt").Split(',');
                        Thread.Sleep(1000 * 60 * r.Next(
                            int.Parse(DelayRange[0].Trim()),
                            int.Parse(DelayRange[1].Trim())));
                    }
                    catch
                    {
                        Thread.Sleep(1000 * 60 * r.Next(1, 10));
                    }
                    finally
                    {
                        Quiz();
                    }
                });
            }));
        }
    }
}
