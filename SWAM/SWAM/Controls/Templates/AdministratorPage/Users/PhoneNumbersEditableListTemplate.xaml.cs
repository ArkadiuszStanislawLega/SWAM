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
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumbersEditableListTemplate.xaml
    /// </summary>
    public partial class PhoneNumbersEditableListTemplate : UserControl
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
        private void AddNewPhone_Click(object sender, RoutedEventArgs e) => TurnOn(this.AddNewPhoneContainer);

        #region ConfirmNewPhone_Click
        /// <summary>
        /// Action after confirm new phone buttton click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNewPhone_Click(object sender, RoutedEventArgs e)
        {
            User user = DataContext as User;
         
            using (ApplicationDbContext context = new ApplicationDbContext())
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
                    //TODO: Make validations and catch exceptions - phones.
                    //this.Information.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    //this.Information.Content = "Nie udało się dodać użytkownika " + user.Name;
                    //this.Information.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();

                PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();
                SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).InformationForUser($"Dodano nowy numer telefonu {phone.PhoneNumber} użytkownikowi {user.Name}.");
            };

            ClearEditableFieldsAfterAddNewPhone();
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
            var user = DataContext as User;

            ApplicationDbContext context = new ApplicationDbContext();
            PhoneNumbers.ItemsSource = context.Phones.Where(u => u.UserId == user.Id).ToList();
        }
        #endregion
    }
}
