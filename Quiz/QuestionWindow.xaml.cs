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

namespace QuizQuiz
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private Question q;

        public QuestionWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            q = Questions.Instance.GetRandomQuestion();
            if (q == null)
            {
                this.Close();
                return;
            }
            Label_Question.Text = q.Description;
            TextBox_Answer.Text = "";
            TextBox_Answer.Focus();
        }

        private void Submit()
        {
            if (q.Check(TextBox_Answer.Text)) Correct();
            else Incorrect();
        }

        private void Incorrect()
        {
            MessageBox.Show("오답");
        }

        private void Correct()
        {
            this.Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Submit();
        }
    }
}
