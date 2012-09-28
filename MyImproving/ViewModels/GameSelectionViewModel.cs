using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;
using MyImproving.Models;

namespace MyImproving.ViewModels
{
    public class GameSelectionViewModel
    {
        private readonly Company _company;
        private readonly GameSelectionModel _selection;

        public GameSelectionViewModel(Company company, GameSelectionModel selection)
        {
            _company = company;
            _selection = selection;
        }

        public string GameSelectionHeader
        {
            get
            {
                return _selection.SelectedGame == null
                    ? "<Select a game>"
                    : _selection.SelectedGame.Name.Value;
            }
        }

        public IEnumerable<GameHeaderViewModel> Games
        {
            get
            {
                return
                    from game in _company.Games
                    orderby game.Name.Value
                    select new GameHeaderViewModel(game);
            }
        }

        public GameHeaderViewModel SelectedGame
        {
            get
            {
                return _selection.SelectedGame == null
                    ? null
                    : new GameHeaderViewModel(_selection.SelectedGame);
            }
            set
            {
            	_selection.SelectedGame = value == null
                    ? null
                    : value.Game;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            GameSelectionViewModel that = obj as GameSelectionViewModel;
            if (that == null)
                return false;
            return Object.Equals(_company, that._company);
        }

        public override int GetHashCode()
        {
            return _company.GetHashCode();
        }
    }
}
