﻿using System;
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
    /// AnswerView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AnswerView : UserControl
    {
        public AnswerView(int index, string text)
        {
            InitializeComponent();
            Label_Text.Text = $"{index + 1}. {text}";
        }

        public event EventHandler OnClick;

        internal void CallOnClick()
        {
            OnClick(null, null);
        }
    }
}
