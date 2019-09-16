using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;

namespace SWAM.Controls.Templates
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseTechnicalDataTemplate.xaml
    /// </summary>
    public partial class WarehouseTechnicalDataTemplate : UserControl
    {
        public WarehouseTechnicalDataTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starting storyboard after which the data is in the edit mode.
        /// </summary>
        public void ShowEditControls() => BeginStoryboard(FindResource("ShowEditStory") as Storyboard);
        /// <summary>
        /// Starting storyboard after which the data is in the read only mode.
        /// </summary>
        public void HideEditControls() => BeginStoryboard(FindResource("HideEditStory") as Storyboard);

        #region TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void TextChanged(object sender, TextChangedEventArgs e)
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

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        protected bool InformationToUser(string message, bool warning = false)
        {
            if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
            {
                mainWindow.InformationForUser(message, warning);
                return true;
            }
            else return false;
        }
        #endregion

        #region ClearControls
        /// <summary>
        /// Clear all TextBlocks.
        /// </summary>
        public void ClearControls()
        {
            this.pHeight.Text = "";
            this.pWidth.Text = "";
            this.pLength.Text = "";
            this.pSurfaceAreaNetto.Text = "";
            this.pSurfaceAreaBrutton.Text = "";
            this.pAcceptableWeight.Text = "";
        }
        #endregion  
    }
}
