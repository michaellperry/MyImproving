using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;

namespace MyImproving.Moderator.ViewModels
{
    public class GameHeaderViewModel
    {
        private readonly Game _game;

        public GameHeaderViewModel(Game game)
        {
            _game = game;            
        }

        public string Name
        {
            get
            {
                return _game.Name == null
                    ? "<New Game>"
                    : _game.Name.Value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            GameHeaderViewModel that = obj as GameHeaderViewModel;
            if (that == null)
                return false;
            return Object.Equals(this._game, that._game);
        }

        public override int GetHashCode()
        {
            return _game.GetHashCode();
        }
    }
}
