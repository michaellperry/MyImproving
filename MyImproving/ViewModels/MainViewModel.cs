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
        private readonly CompanySelectionViewModel _companySelectionVM;
        private readonly CompanySelectionModel _companySelection;
        private readonly GameSelectionModel _gameSelection;

        public MainViewModel(Community community, SynchronizationService synhronizationService, CompanySelectionModel companySelection, GameSelectionModel gameSelection)
        {
            _gameSelection = gameSelection;
            _community = community;
            _synhronizationService = synhronizationService;
            _companySelection = companySelection;

            if (synhronizationService.Individual != null)
            {
                _companySelectionVM = new CompanySelectionViewModel(
                    synhronizationService.Individual,
                    _companySelection);
            }
        }

        public CompanySelectionViewModel CompanySelection
        {
            get { return _companySelectionVM; }
        }

        public GameSelectionViewModel GameSelection
        {
            get
            {
                return _companySelectionVM.SelectedCompany == null
                    ? null
                    : new GameSelectionViewModel(_companySelection.SelectedCompany, _gameSelection);
            }
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }
    }
}
