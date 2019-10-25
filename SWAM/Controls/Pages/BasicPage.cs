using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System;
using SWAM.Windows;
using SWAM.Enumerators;
using SWAM.Exceptions;
using System.Collections.Generic;

namespace SWAM.Controls.Pages
{
    public partial class BasicPage : UserControl
    {
        /// <summary>
        /// Main window instance.
        /// </summary>
        /// <returns></returns>
        protected SWAM.MainWindow _mainWindow { get => SWAM.MainWindow.FindParent<SWAM.MainWindow>(this); }
        /// <summary
        /// Confirm window instance.
        /// </summary>
        /// <returns>Confirm window.</returns>
        protected ConfirmWindow _confirmWindow { get => this._mainWindow.Windows.TryGetValue(WindowType.Question, out Window messageWindow) ? messageWindow as ConfirmWindow : null; }


        public Storyboard UnloadStory = new Storyboard();
        public static List<BasicPage> CurrentLoadedBasicPage = new List<BasicPage>();

        public Storyboard CreateStory()
        {
            var delay = 300;

            Storyboard.SetTargetName(UnloadStory, "MainContent");

            AddSlide(delay);
            AddFade(delay);
            this.Visibility = Visibility.Visible;

            return UnloadStory;
        }

        public void SetStoryboardTarget(DependencyObject obj)
        {
            Storyboard.SetTarget(UnloadStory, obj);
        }

        public void StartUnloadStory()
        {
            this.Visibility = Visibility.Visible;
            this.BeginStoryboard(UnloadStory);
        }

        private void AddSlide(double delay)
        {
            var thicknessAnimation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(delay)),
                From = new Thickness(0),
                To = new Thickness(0, 0, 400, 0),
                SpeedRatio = 2
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            UnloadStory.Children.Add(thicknessAnimation);
        }

        private void AddFade(double delay)
        {
            var doubleAnimation = new DoubleAnimation()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(delay)),
                From = 1,
                To = 0
            };
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            UnloadStory.Children.Add(doubleAnimation);
        }

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        protected bool InformationToUser(string message, bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(message, warning);
                    return true;
                }
                else throw new InformationLabelException(message);
            }
            catch (InformationLabelException ex)
            {
                ex.ShowMessage(this);
                return false;
            }
        }
        #endregion
    }
}
