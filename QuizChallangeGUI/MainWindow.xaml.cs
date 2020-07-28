using Quiz;
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

namespace QuizChallangeGUI
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var roundCount = int.Parse(TextBox_RoundCount.Text);
                var lifeCount = int.Parse(TextBox_LifeCount.Text);
                var includeSelection = Checkbox_Include_Selection.IsChecked.Value;
                var includeType = Checkbox_Include_Type.IsChecked.Value;
                var generateSelection = Checkbox_Create_Selection.IsChecked.Value;

                if (!includeSelection && !includeType) throw new Exception();
                Game.InitializeProgram(generateSelection);
                var g = new Game();
                g.IncludeSelection = includeSelection;
                g.IncludeType = includeType;
                this.Hide();
                g.OnGameFinished += delegate
                {
                    this.Show();
                };
                g.StartGame(roundCount, lifeCount);
            }
            catch
            {
                this.Show();
                MessageBox.Show("올바르지 않은 값이 있습니다.");
            }
        }
    }
}
