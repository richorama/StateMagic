using System;
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
    public partial class CreateNewModel : ChildWindow
    {
        public CreateNewModel()
        {
            InitializeComponent();
        }

        public string ModelName
        {
            get 
            {
                return this.textBox1.Text;
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
    }
}

