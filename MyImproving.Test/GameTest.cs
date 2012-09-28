using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyImproving.Model;
using System.Linq;
using System.Collections.Generic;

namespace MyImproving.Test
{
    [TestClass]
    public class GameTest : TestBase
    {
        private Company _companyAlan;
        private Company _companyFlynn;
        private Game _gameModerator;
        private Game _gameAlan;
        private Game _gameFlynn;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCommunity();

            _companyAlan = _individualAlan.CreateCompany();
            _companyAlan.Name = "Initech";

            _companyFlynn = _individualFlynn.CreateCompany();
            _companyFlynn.Name = "Flynn's";

            Synchronize();

            _gameModerator = _domainModerator.CreateGame(_domainModerator.Companies);

            Synchronize();

            _gameAlan = _companyAlan.Games.Single();
            _gameFlynn = _companyFlynn.Games.Single();
        }

        [TestMethod]
        public void ModeratorBeginsARound()
        {
            _gameModerator.CreateRound(1);

            Synchronize();

            Assert.AreEqual(1, _gameAlan.Rounds.Count());
            Assert.AreEqual(1, _gameFlynn.Rounds.Count());
        }

        [TestMethod]
        public void ModeratorDealsCandidateCards()
        {
            CandidateDeck deck = new CandidateDeck();
            Round round = _gameModerator.CreateRound(1);
            List<Candidate> candidates = Enumerable.Range(0, 4)
                .Select(i => deck.DealCandidateCard(round))
                .ToList();

            Synchronize();

            Round roundAlan = _gameAlan.Rounds.Single();
            foreach (var candidate in candidates)
            {
                Assert.IsTrue(roundAlan.Candidates.Any(c =>
                    c.Skill == candidate.Skill &&
                    c.Relationship == candidate.Relationship));
            }

            Round roundFlynn = _gameFlynn.Rounds.Single();
            foreach (var candidate in candidates)
            {
                Assert.IsTrue(roundFlynn.Candidates.Any(c =>
                    c.Skill == candidate.Skill &&
                    c.Relationship == candidate.Relationship));
            }
        }
    }
}
