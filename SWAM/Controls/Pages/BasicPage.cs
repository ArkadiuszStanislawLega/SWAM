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
        #region Properties
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
        /// <summary>
        /// A storyboard that is executed when the page is turned off.
        /// </summary>
        public Storyboard UnloadStory = new Storyboard();
        public static List<BasicPage> CurrentLoadedBasicPage { get; set; } = new List<BasicPage>();
        #endregion
        #region CreateStory
        /// <summary>
        /// Prepares storyboard before running it.
        /// </summary>
        /// <returns>A storyboard that is executed when the page is turned off.</returns>
        public Storyboard CreateStory()
        {
            var delay = 300;

            Storyboard.SetTargetName(UnloadStory, "MainContent");

            AddSlide(delay);
            AddFade(delay);

            this.Visibility = Visibility.Visible;

            return UnloadStory;
        }
        #endregion
        #region SetStoryboardTarget
        /// <summary>
        /// Sets the StoryBoard Target, this is usually the Main Content grid.
        /// </summary>
        /// <param name="obj">Main Content grid</param>
        public void SetStoryboardTarget(DependencyObject obj) => Storyboard.SetTarget(UnloadStory, obj);
        #endregion
        #region StartUnloadStory
        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void StartUnloadStory()
        {
            this.Visibility = Visibility.Visible;
            this.BeginStoryboard(UnloadStory);
        }
        #endregion
        #region AddSlide
        /// <summary>
        /// I add movement in the storyboard.
        /// </summary>
        /// <param name="duration">Storyboard duration.</param>
        private void AddSlide(double duration)
        {
            var thicknessAnimation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                From = new Thickness(0),
                To = new Thickness(0, 0, 400, 0),
                SpeedRatio = 2
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            UnloadStory.Children.Add(thicknessAnimation);
        }
        #endregion
        #region AddFade
        /// <summary>
        /// Add fade to storyboard.
        /// </summary>
        /// <param name="delay">Storyboard duration.</param>
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
        #endregion
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
