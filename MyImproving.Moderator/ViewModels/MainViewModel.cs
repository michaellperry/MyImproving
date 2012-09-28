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

        public MainViewModel(Community community, SynchronizationService synhronizationService, NewGameSelectionModel newGameSelection)
        {
            _community = community;
            _synhronizationService = synhronizationService;

            _newGame = new NewGameViewModel(synhronizationService.Domain, newGameSelection);
        }

        public NewGameViewModel NewGame
        {
            get { return _newGame; }
        }

        public IEnumerable<GameHeaderViewModel> Games
        {
            get
            {
                return
                    from game in _synhronizationService.Domain.Games
                    orderby game.Name.Value
                    select new GameHeaderViewModel(game);
            }
        }

        public bool Synchronizing
        {
            get { return _synhronizationService.Synchronizing; }
        }
    }
}
