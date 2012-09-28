using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MyImproving.Moderator.ViewModels;
using UpdateControls.XAML;

namespace MyImproving.Moderator.Views
{
    public partial class NewGameControl : UserControl
    {
        private NewGameViewModel _viewModel;

        public NewGameControl()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            InitializeViewModel();
            NewGamePopup.IsOpen = !NewGamePopup.IsOpen;
        }

        private void InitializeViewModel()
        {
            if (_viewModel == null)
            {
                _viewModel = ForView.Unwrap<NewGameViewModel>(DataContext);
                if (_viewModel != null)
                {
                    _viewModel.HideNewGame += HideNewGame;
                }
            }
        }

        private void HideNewGame()
        {
            NewGamePopup.IsOpen = false;
        }
    }
}
