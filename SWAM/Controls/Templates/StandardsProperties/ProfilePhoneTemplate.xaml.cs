using SWAM.Controls.Templates.AdministratorPage;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.StandardsProperties
{
    /// <summary>
    /// Logika interakcji dla klasy ProfilePhoneTemplate.xaml
    /// </summary>
    public partial class ProfilePhoneTemplate : BasicUserControl
    {
        public ProfilePhoneTemplate()
        {
            InitializeComponent();
        }

        private void CancelChangePhone_Click(object sender, RoutedEventArgs e) => this.EditPhone.Text = string.Empty;

        #region EditPhone_TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void EditPhone_TextChanged(object sender, TextChangedEventArgs e)
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
