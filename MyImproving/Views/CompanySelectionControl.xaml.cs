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
using UpdateControls.XAML;
using MyImproving.ViewModels;

namespace MyImproving.Views
{
    public partial class CompanySelectionControl : UserControl
    {
        private CompanySelectionViewModel _viewModel;

        public CompanySelectionControl()
        {
            InitializeComponent();
        }

        private void DisplayCompanyEdit()
        {
            CompanyEditPopup.IsOpen = true;
            CompanySelectionPopup.IsOpen = false;
            CompanyNameTextBox.Focus();
        }

        private void HideCompanyEdit()
        {
            CompanyEditPopup.IsOpen = false;
        }

        private void Selection_Click(object sender, RoutedEventArgs e)
        {
            InitializeViewModel();
            CompanySelectionPopup.IsOpen = !CompanySelectionPopup.IsOpen;
        }

        private void InitializeViewModel()
        {
            if (_viewModel == null)
            {
                _viewModel = ForView.Unwrap<CompanySelectionViewModel>(DataContext);
                if (_viewModel != null)
                {
                    _viewModel.DisplayCompanyEdit += DisplayCompanyEdit;
                    _viewModel.HideCompanyEdit += HideCompanyEdit;
                }
            }
        }

        private void CompanySelectionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CompanySelectionPopup.IsOpen = false;
        }
    }
}
