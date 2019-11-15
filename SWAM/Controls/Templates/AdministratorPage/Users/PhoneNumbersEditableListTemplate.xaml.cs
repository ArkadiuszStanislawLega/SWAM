using SWAM.Models.User;
using System.Windows;
using SWAM.Strings;
using SWAM.Exceptions;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Linq;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumbersEditableListTemplate.xaml
    /// </summary>
    public partial class PhoneNumbersEditableListTemplate : BasicUserControl
    {
        public PhoneNumbersEditableListTemplate()
        {
            InitializeComponent();
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
                var phone = new UserPhone()
                {
                    PhoneNumber = this.NewPhone.Text,
                    Note = this.NewPhoneNote.Text,
                    User = user
                };

                if (phone != null)
                {
                    user.AddPhone(phone);
                    InformationToUser($"dodano nowy numer telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.");

                    RefreshPhoneList();
                    ClearEditableFieldsAfterAddNewPhone();
                }
                else InformationToUser($"nie udało się dodać nowego numeru telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.", true);
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
                    var phonesList = user.GetPhones();
                    if (phonesList != null)
                        PhoneNumbers.ItemsSource = phonesList;
                    else throw new RefreshUserPhonesListException();
                }
                else throw new RefreshUserPhonesListException();
            }
            catch (RefreshUserPhonesListException ex) { ex.ShowMessage(this); }
        }
        #endregion
        #region CancelCreateNewPhone_Click
        /// <summary>
        /// Action after click cancel add new phone number button during creating new contatact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCreateNewPhone_Click(object sender, RoutedEventArgs e) => ClearEditableFieldsAfterAddNewPhone();
        #endregion
        #region NewPhone_TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void NewPhone_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            char[] charArray;

            Regex regex = new Regex("[^0-9]+");
            if (sender is TextBox textBox && regex.IsMatch(textBox.Text))
            {
                var values = textBox.Text.ToList();
                values.RemoveAt(values.Count - 1);

                charArray = values.ToArray();

                textBox.Text = new string(charArray);
                InformationToUser($"Podając tą wartość możesz użyć tylko cyfr.", true);
            }
            else InformationToUser("");
        }
        #endregion
    }
}
