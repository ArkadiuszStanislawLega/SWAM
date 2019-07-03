﻿using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SWAM.Controls.Templates.AdministratorPage
{
    using SWAM.Exceptions;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy EmailEditableTemplate.xaml
    /// </summary>
    public partial class EmailEditableTemplate : BasicUserControl
    {
        public EmailEditableTemplate()
        {
            InitializeComponent();
            
        }

        #region Confirm_Click
        /// <summary>
        /// Action after click Confirm button after edit email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Email email)
            {
                email.UpdateEmail(EditEmail.Text);
                InformationToUser($"Edytowano adress email {email.AddressEmail} użytkownikowi {email.User.Name}.");
            }
            else InformationToUser(ErrorMesages.DURING_EDIT_EMAIL_ERROR, true);
        }
        #endregion
        #region Delete_Click
        /// <summary>
        /// Action after click delete email button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Email email)
            {
                email.Delete();
                InformationToUser($"Usnięto adress email {email.AddressEmail}.");
                try
                {
                    var emailList = FindParent<EmailEditableListTemplate>(this);
                    if (emailList != null) emailList.RefreshEmailsList();
                    else throw new RefreshUserEmailListException();
                }
                catch (RefreshUserEmailListException ex) { ex.ShowMessage(this); };
            }
            else InformationToUser(ErrorMesages.DURIGN_DELETE_EMAIL_ERROR, true);
        }
        #endregion
        #region Cancel_Click
        /// <summary>
        /// Action after click cancel button during edit addres email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Email email)
            {
                Email dbEmail = SWAM.Models.Email.GetEmailById(email.Id);
                if (dbEmail != null)
                {
                    this.Email.Text = dbEmail.AddressEmail;
                    this.EditEmail.Text = dbEmail.AddressEmail;
                }
                else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.CANCEL_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.CANCEL_ERROR}", true);
        }
        #endregion
    }
}
