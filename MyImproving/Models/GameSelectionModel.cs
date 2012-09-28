using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;
using UpdateControls.Fields;

namespace MyImproving.Models
{
    public class GameSelectionModel
    {
        private readonly CompanySelectionModel _companySelection;

        private Independent<Game> _selectedGame = new Independent<Game>();
        
        public GameSelectionModel(CompanySelectionModel companySelection)
        {
            _companySelection = companySelection;
        }

        public Game SelectedGame
        {
            get
            {
                return
                    (_selectedGame.Value == null ||
                     !_selectedGame.Value.Companies.Contains(_companySelection.SelectedCompany))
                        ? _companySelection.SelectedCompany.Games.FirstOrDefault()
                        : _selectedGame.Value;
            }
            set { _selectedGame.Value = value; }
        }
    }
}
