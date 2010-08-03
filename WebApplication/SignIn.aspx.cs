using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseTypes;
using System.Text.RegularExpressions;
using DataAccess;

namespace WebApplication
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(EmailAddress.Text,@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                ErrorMessages.Text = "Email address does not appear to be valid";
                return;
            }       
            
            if (Password1.Text.Length < 6)
            {
                ErrorMessages.Text = "Passwords must be 6 characters or more";
                return;
            }
        
            if (Password1.Text != Password2.Text)
            {
                ErrorMessages.Text = "Passwords do not match";
                return;
            }    
        
            /* turn off reCAPTCHA for development
            if (!this.IsValid)
            {
                this.ErrorMessages.Text = "The form is not valid, please review and try again";
                return;
            }
            */

            DataAccess.DatabaseWrapper.Init();

            CredentialData[] matches = CredentialData.FindAllByProperty("Username", EmailAddress.Text);
            if (matches.Length > 0)
            {
                ErrorMessages.Text = "Email address already registered";
                return; 
            }

            // success - create an account
            CredentialData cd = new CredentialData();
            cd.Username = EmailAddress.Text;
            cd.Password = Password1.Text;
            cd.Authentications = 0;
            cd.LastAuthentication = DateTime.Now;
            cd.ApiKey = Guid.NewGuid();
            cd.TransactionBalance = 1000; // this is the starting balance.
            cd.Save();

            Session["Credentials"] = cd;

            Response.Redirect("UserDetails.aspx");

        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            try
            {
                CredentialData cd = WebServices.LogIn(SignInEmailAddress.Text, SignInPassword.Text);
                Session["Credentials"] = cd;
                Response.Redirect("UserDetails.aspx");
            }
            catch (Exception ex)
            {
                ErrorMessages.Text = ex.Message;
            }

        }
    }
}
