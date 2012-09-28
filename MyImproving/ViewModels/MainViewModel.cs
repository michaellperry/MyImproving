using System;
using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using MyImproving.Models;

namespace MyImproving.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly SynchronizationService _synhronizationService;
        private readonly CompanySelectionViewModel _companySelection;

        public MainViewModel(Community community, SynchronizationService synhronizationService, CompanySelectionModel companySelection)
        {
            _community = community;
            _synhronizationService = synhronizationService;

            if (synhronizationService.Individual != null)
                _companySelection = new CompanySelectionViewModel(
                    synhronizationService.Individual,
                    companySelection);
        }

        public CompanySelectionViewModel CompanySelection
        {
            get { return _companySelection; }
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }
    }
}
