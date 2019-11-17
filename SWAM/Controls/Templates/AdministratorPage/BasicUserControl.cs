using SWAM.Enumerators;
using SWAM.Exceptions;
using SWAM.Windows;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy BasicUserControl.xaml
    /// </summary>
    public partial class BasicUserControl : UserControl
    {
        private readonly List<CancellationTokenSource> _cancellationTokens = new List<CancellationTokenSource>();
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

        public Storyboard CreateStory()
        {
            var delay = 6000;

            Storyboard.SetTargetName(UnloadStory, "MainContent");

            AddSlide(delay);
            AddFade(delay);

            this.Visibility = Visibility.Visible;

            UnloadStory.Begin(this);

            return UnloadStory;
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
                    HideInformation();
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

        #region HideInformation
        /// <summary>
        /// Hides the information bar after a specified time.
        /// </summary>
        /// <param name="mainWindow">Main window of application.</param>
        /// <returns>Hiding Task</returns>
        private async void HideInformation()
        {
            //When new task is added old task should be finished.
            if (this._cancellationTokens.Count > 0)
            {
                this._cancellationTokens[_cancellationTokens.Count - 1].Cancel();
                this._cancellationTokens.RemoveAt(_cancellationTokens.Count - 1);
            }

            await GetTask();
        }
        #endregion
        #region GetTask
        /// <summary>
        /// A task that measures the time to hide the last information for the user that has been shown.
        /// </summary>
        /// <returns></returns>
        private async Task GetTask()
        {
            int counter = 0;
            //Creating new cancellation token source is needed to cancel a new task that will be added.
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //Add new cancelation token to list.
            this._cancellationTokens.Add(cts);

            await Task.Run(async () =>
            {
                //Counting seconds to hide information
                while (counter < SWAM.MainWindow.TIME_LEFT_TO_HIDE_INFORMATION)
                {
                    //if the job cancellation token is activated - break this loop
                    if (token.IsCancellationRequested) break;
                    //Await 1 second
                    await Task.Delay(1000);
                    counter++;

                    //If number of second is equal property TIME_LEFT_TO_HIDE_INFORMATION then start storyboard to hide information.
                    if (counter == SWAM.MainWindow.TIME_LEFT_TO_HIDE_INFORMATION)
                    {
                        Dispatcher.Invoke(new Action(() => {
                            SWAM.MainWindow.Instance.HideInformationForUser();
                        }))
                       ;
                    }
                }
            }, token);
        }
        #endregion
        #region UserProfileRefresh
        /// <summary>
        /// Refresh current user profile.
        /// </summary>
        protected void UserProfileRefresh()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this) is UserProfileTemplate userProfileTemplate)
                    userProfileTemplate.RefreshData();
                else throw new RefreshUserProfileException();
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion
        #region UserListRefresh
        /// <summary>
        /// Refreshing user list.
        /// </summary>
        protected void UserListRefresh()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) is UsersControlPanelTemplate usersControlPanelTemplate)
                    usersControlPanelTemplate.RefreshUsersList();
                else throw new RefreshUserListException();
            }
            catch (RefreshUserListException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}

