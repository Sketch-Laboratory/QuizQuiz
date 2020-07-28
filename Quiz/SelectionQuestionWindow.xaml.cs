using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quiz
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectableQuestionWindow : Window
    {
        private SelectableQuestion q;

        public int LeftedLife { get; set; } = int.MaxValue;

        public SelectableQuestionWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
        }
        private void Incorrect()
        {
            if (--LeftedLife <= 0)
            {
                this.Close();
                return;
            }
            MessageBox.Show("오답입니다.");
        }

        private void Correct()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            q = Questions.Instance.GetRandomSelectableQuestion();
            if (q == null)
            {
                this.Close();
                return;
            }
            Label_Question.Text = q.Description;
            Layout_Answers.Children.Clear();
            var selections = q.ShuffledSelections;
            for (int i= 0; i< selections.Count; i++)
            {
                var item = selections[i];
                var v = new AnswerView(i, item);
                v.MouseDown += delegate {
                    if (q.Answer == item) Correct();
                    else Incorrect();
                };
                Layout_Answers.Children.Add(v);
            }
        }
    }
}
