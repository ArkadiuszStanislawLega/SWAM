using SWAM.Controls.Templates.AdministratorPage;
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

namespace SWAM.Controls.Templates.MainWindow
{
    /// <summary>
    /// Logika interakcji dla klasy MessagesPageTemplate.xaml
    /// </summary>
    public partial class MessagesPageTemplate : BasicUserControl
    {
        public MessagesPageTemplate()
        {
            InitializeComponent();
            SWAM.MainWindow.LoggedInUser.Messages = new List<Message>()
            {
                new Message()
                {
                    Sender = new User(){Name = "Mietek" },
                    TitleOfMessage = "Witaj w systemie",
                    ContentOfMessage = "Zostałeś poprawnie dodany do systemu. Przestrzegaj zasad i regulaminu systemu.",
                    PostDate = DateTime.Now
                },
                new Message()
                {
                    Sender = new User(){Name = "Czesiek" },
                    TitleOfMessage = "Faktura za gwoździe",
                    ContentOfMessage = "Czy to ty zrobiłeś fakturę numer 8000 na gwoździe, jest w niej błąd.",
                    PostDate = DateTime.Now
                },
                new Message()
                {
                    Sender = new User(){Name = "Monika" },
                    TitleOfMessage = "Potrzebuję numeru faktury",
                    ContentOfMessage = "Potrzebna jest faktura za gwoździe które Jan Kowalski kupił w zeszłym tygodniu. Tam była ilość 10000 gwoździ.",
                    PostDate = DateTime.Now
                }
            };
            DataContext = SWAM.MainWindow.LoggedInUser;
        }
    }
}
