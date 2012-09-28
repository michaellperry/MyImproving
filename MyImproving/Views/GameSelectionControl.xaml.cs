using System.Windows;
using System.Windows.Controls;

namespace MyImproving.Views
{
    public partial class GameSelectionControl : UserControl
    {
        public GameSelectionControl()
        {
            InitializeComponent();
        }

        private void Selection_Click(object sender, RoutedEventArgs e)
        {
            GameSelectionPopup.IsOpen = !GameSelectionPopup.IsOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameSelectionPopup.IsOpen = false;
        }
    }
}
