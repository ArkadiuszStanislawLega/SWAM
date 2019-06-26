using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.AdministratorPage
{
    using SWAM.Exceptions;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumbersEditableListTemplate.xaml
    /// </summary>
    public partial class PhoneNumbersEditableListTemplate : BasicUserControl
    {
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
                var phone = new Phone()
                {
                    PhoneNumber = this.NewPhone.Text,
                    Note = this.NewPhoneNote.Text,
                    UserId = user.Id
                };

                if (phone != null)
                {
                    Phone.AddNewPhone(phone);
                    //TODO: Try - catch
                    using (var context = new ApplicationDbContext())
                        PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();

                    InformationToUser($"Dodano nowy numer telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.");
                    ClearEditableFieldsAfterAddNewPhone();
                }
                else InformationToUser($"Nie udało się dodać nowego numeru telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_ADD_PHONE_ERROR}", true);
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
            try
            {
                if (DataContext is User user)
                {
                    //TODO: try - catch
                    using (var context = new ApplicationDbContext())
                        PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();
                }
                else throw new RefreshUserPhonesListException();
            }
            catch (RefreshUserPhonesListException ex ) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
