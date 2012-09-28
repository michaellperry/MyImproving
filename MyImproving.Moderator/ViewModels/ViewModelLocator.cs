using System;
using System.ComponentModel;
using System.Linq;
using UpdateControls.XAML;
using MyImproving.Moderator.Models;

namespace MyImproving.Moderator.ViewModels
{
    public class ViewModelLocator
    {
        private readonly SynchronizationService _synchronizationService;

        private readonly MainViewModel _main;

        public ViewModelLocator()
        {
            _synchronizationService = new SynchronizationService();
            if (!DesignerProperties.IsInDesignTool)
                _synchronizationService.Initialize();

            CompanySelectionModel companySelection = new CompanySelectionModel();
            _main = new MainViewModel(_synchronizationService.Community, _synchronizationService, companySelection);
        }

        public object Main
        {
            get { return ForView.Wrap(_main); }
        }
    }
}
