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
using MyImproving.Model.Services;

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
        private ModeratorService _moderator;

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
            _moderator = new ModeratorService(_gameModerator);

            Synchronize();

            _gameAlan = _companyAlan.Games.Single();
            _gameFlynn = _companyFlynn.Games.Single();
        }

        [TestMethod]
        public void ModeratorBeginsARound()
        {
            _moderator.BeginNextRound();

            Synchronize();

            Assert.AreEqual(1, _gameAlan.Rounds.Count());
            Assert.AreEqual(1, _gameAlan.Rounds.Single().Index);
            Assert.AreEqual(1, _gameFlynn.Rounds.Count());
            Assert.AreEqual(1, _gameFlynn.Rounds.Single().Index);
        }

        [TestMethod]
        public void ModeratorDealsCandidateCards()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(4);

            Synchronize();

            Round roundAlan = _gameAlan.Rounds.Single();
            Assert.AreEqual(4, roundAlan.Candidates.Count());
            foreach (var candidate in candidates)
            {
                Assert.IsTrue(roundAlan.Candidates.Any(c =>
                    c.Skill == candidate.Skill &&
                    c.Relationship == candidate.Relationship));
            }

            Round roundFlynn = _gameFlynn.Rounds.Single();
            Assert.AreEqual(4, roundFlynn.Candidates.Count());
            foreach (var candidate in candidates)
            {
                Assert.IsTrue(roundFlynn.Candidates.Any(c =>
                    c.Skill == candidate.Skill &&
                    c.Relationship == candidate.Relationship));
            }
        }

        [TestMethod]
        public void FlynnMakesOfferToCandidate()
        {
            _moderator.BeginNextRound();
            Candidate candidate = _moderator.DealCandidates(1).Single();

            Synchronize();

            Round roundFlynn = _gameFlynn.Rounds.Single();
            Turn turnFlynn = _companyFlynn.CreateTurn(roundFlynn);
            Candidate candidateFlynn = roundFlynn.Candidates.Single();
            turnFlynn.CreateOffer(candidateFlynn, 1);

            Synchronize();

            Assert.AreEqual(1, candidate.Offers.Count());
            Assert.AreEqual(1, candidate.Offers.Single().Chances);
        }
    }
}
