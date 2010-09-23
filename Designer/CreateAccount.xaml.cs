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
using System.Text.RegularExpressions;

namespace StateMagic.Designer
{
    public enum SignInMode
    { 
        NewAccount,
        ExistingAccount
    }

    public partial class CreateAccount : ChildWindow
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public SignInMode SignInMode 
        {
            get
            {
                switch (tabControl1.SelectedIndex)
                { 
                    case 0:
                        return Designer.SignInMode.ExistingAccount;
                    default:
                        return Designer.SignInMode.NewAccount;
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Validate();


            this.DialogResult = true;
        }

        public string Validate()
        {
            if (this.SignInMode == Designer.SignInMode.NewAccount)
            {
                if (!Regex.IsMatch(textBoxNewUsername.Text, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    return "Email address does not appear to be valid";
                }

                if (textBoxNewPassword1.Password.Length < 6)
                {
                    return "Passwords must be 6 characters or more";
                }

                if (textBoxNewPassword1.Password != textBoxNewPassword2.Password)
                {
                    return "Passwords do not match";
                }
            }
            if (this.SignInMode == Designer.SignInMode.ExistingAccount)
            {
            
            //TODO: finish this off
            }
            return null;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string Username
        {
            get
            {
                throw new NotImplementedException();
            }

        }

    }
}

