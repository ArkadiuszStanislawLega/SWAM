using SWAM.Models;
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
    /// Logika interakcji dla klasy PhoneNumbersEditableListTemplate.xaml
    /// </summary>
    public partial class PhoneNumbersEditableListTemplate : UserControl
    {
        /// <summary>
        /// Information about actions to user.
        /// </summary>
        private string _message;

        public PhoneNumbersEditableListTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Action after add new phone button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewPhone_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make this in xaml
            TurnOn(this.AddNewPhoneContainer);
        }

        #region ConfirmNewPhone_Click
        /// <summary>
        /// Action after confirm new phone buttton click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNewPhone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                //TODO: Try - catch
                using (var context = new ApplicationDbContext())
                {
                    var phone = new Phone()
                    {
                        PhoneNumber = this.NewPhone.Text,
                        Note = this.NewPhoneNote.Text,
                        UserId = user.Id
                    };

                    if (phone != null)
                    {
                        context.Phones.Add(phone);
                        context.SaveChanges();

                        PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();
                        this._message = ($"Dodano nowy numer telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.");
                        InformationToUser();

                        ClearEditableFieldsAfterAddNewPhone();
                        //TODO: Make validations and catch exceptions - phones.
                    }
                    else
                    {
                        this._message = ($"Nie udało się dodać nowego numeru telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.");
                        InformationToUser(true);
                    }
                };
            }
        }
        #endregion

        #region ClearEditableFieldsAfterEdit
        /// <summary>
        /// Clear space in editables places after adding new phone,
        /// and shut down this view.
        /// </summary>
        private void ClearEditableFieldsAfterAddNewPhone()
        {
            this.NewPhone.Text = "";
            this.NewPhoneNote.Text = "";
            TurnOff(this.AddNewPhoneContainer);
        }
        #endregion
        #region RefreshPhoneList
        /// <summary>
        /// Refreshing view list of phones.
        /// </summary>
        public void RefreshPhoneList()
        {
            if (DataContext is User user)
            {
                //TODO: try - catch
                using (var context = new ApplicationDbContext())
                    PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();
            }
        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// ake information in MainWindow to user about action.
        /// </summary>
        /// <param name="warning">True - content of information have some worning.</param>
        private void InformationToUser(bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                    mainWindow.InformationForUser(this._message, warning);
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
