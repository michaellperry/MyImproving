using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UpdateControls.Correspondence;
using UpdateControls.XAML;
using MyImproving.Moderator.Models;

namespace MyImproving.Moderator.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly SynchronizationService _synhronizationService;
        private readonly NewGameViewModel _newGame;

        public MainViewModel(Community community, SynchronizationService synhronizationService, CompanySelectionModel companySelection)
        {
            _community = community;
            _synhronizationService = synhronizationService;

            _newGame = new NewGameViewModel(synhronizationService.Domain, companySelection);
        }

        public NewGameViewModel NewGame
        {
            get { return _newGame; }
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }
    }
}
