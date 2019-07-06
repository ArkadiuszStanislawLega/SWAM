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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
