using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;
using MyImproving.Models;

namespace MyImproving.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            CompanySelectionModel companySelection = new CompanySelectionModel();
            GameSelectionModel gameSelection = new GameSelectionModel(companySelection);
            if (!DesignerProperties.IsInDesignTool)
            {
                _synchronizationService.Initialize();
                companySelection.SelectedCompany = _synchronizationService
                    .Individual.Companies
                    .Ensure()
                    .FirstOrDefault();
            }

            _main = new MainViewModel(
                _synchronizationService.Community,
                _synchronizationService,
                companySelection,
                gameSelection);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }
    }
}
