﻿using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    public class ChangeUserBlockDate : CalendarWithButton
    {
        public ChangeUserBlockDate()
        {
            InitializeComponent();
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var user = DataContext as User;
            if (user != null)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                this.Calendar.SelectedDate = (context.Users.FirstOrDefault(u => u.Id == user.Id).ExpiryDateOfTheBlockade);
            }
        }

        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO: Create validation.
            if (this.Calendar.SelectedDate != null)
            {
                var user = DataContext as User;

                ApplicationDbContext context = new ApplicationDbContext();
                context.Users.FirstOrDefault(u => u.Id == user.Id).ExpiryDateOfTheBlockade = this.Calendar.SelectedDate;
                context.SaveChanges();

                SWAM.MainWindow.FindParent<UserProfileTemplate>(this).RefreshData();

                SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                    InformationForUser($"Data blokady użytkownika {user.Name} została zmieniona na {this.Calendar.SelectedDate}.");
            }
        }
    }
}