﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StateMagic.Designer
{
    public partial class RenameState : ChildWindow
    {
        public RenameState()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return textBox1.Text;
            }
            set 
            {
                textBox1.Text = value;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OKButton_Click(sender, e);
            }
        }
    }
}

