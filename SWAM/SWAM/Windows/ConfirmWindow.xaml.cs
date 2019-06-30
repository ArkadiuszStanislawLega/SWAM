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
using System.Windows.Shapes;

namespace SWAM.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        //TODO: Documentations
        bool _userAnswer = false;
        MainWindow _main;

        public ConfirmWindow(MainWindow main)
        {
            InitializeComponent();
            this._main = main;
        }

        public void Show(string message, out bool answer)
        {
            this.ShowDialog();
            this.Question.Text = message;
            answer = this._userAnswer;

            if(this._main != null)
                this._main.EverythinInWindow.IsEnabled = false;
        }
 
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = true;

            if (this._main != null)
                this._main.EverythinInWindow.IsEnabled = true;
            this.Hide();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = false;

            if (this._main != null)
                this._main.EverythinInWindow.IsEnabled = true;
            this.Hide();
        }

        #region Exit_Click
        /// <summary>
        /// Action after click in top bar right corner button.
        /// Closing application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = false;
            this.Hide();
        }
        #endregion

    }
}
