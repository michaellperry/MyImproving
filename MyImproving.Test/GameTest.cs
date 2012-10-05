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

            MakeOffer(_companyFlynn, 1, candidate.Unique);

            Synchronize();

            Assert.AreEqual(1, candidate.Offers.Count());
            Assert.AreEqual(1, candidate.Offers.Single().Chances);
        }

        [TestMethod]
        public void ModeratorAwardsCandidates()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(2);

            Synchronize();

            Offer offerAlan1 = MakeOffer(_companyAlan, 3, candidates[0].Unique);
            Offer offerAlan2 = MakeOffer(_companyAlan, 1, candidates[1].Unique);

            Offer offerFlynn1 = MakeOffer(_companyFlynn, 2, candidates[0].Unique);
            Offer offerFlynn2 = MakeOffer(_companyFlynn, 7, candidates[1].Unique);

            Synchronize();

            _moderator.AwardCandidates();

            Synchronize();

            Assert.AreEqual(1, candidates[0].Offers.Count(offer => offer.Hires.Any()));
            Assert.AreEqual(1, candidates[1].Offers.Count(offer => offer.Hires.Any()));

            bool candidate1SelectedAlan = offerAlan1.Hires.Any();
            bool candidate2SelectedAlan = offerAlan2.Hires.Any();
            bool candidate1SelectedFlynn = offerFlynn1.Hires.Any();
            bool candidate2SelectedFlynn = offerFlynn2.Hires.Any();

            Assert.IsTrue(candidate1SelectedAlan ^ candidate1SelectedFlynn);
            Assert.IsTrue(candidate2SelectedAlan ^ candidate2SelectedFlynn);
        }

        [TestMethod]
        public void NoCompetitionOnCandidatesIsASureThing()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(2);

            Synchronize();

            Offer offerAlan1 = MakeOffer(_companyAlan, 3, candidates[0].Unique);

            Offer offerFlynn2 = MakeOffer(_companyFlynn, 7, candidates[1].Unique);

            Synchronize();

            _moderator.AwardCandidates();

            Synchronize();

            Assert.AreEqual(1, candidates[0].Offers.Count(offer => offer.Hires.Any()));
            Assert.AreEqual(1, candidates[1].Offers.Count(offer => offer.Hires.Any()));

            bool candidate1SelectedAlan = offerAlan1.Hires.Any();
            bool candidate2SelectedFlynn = offerFlynn2.Hires.Any();

            Assert.IsTrue(candidate1SelectedAlan);
            Assert.IsTrue(candidate2SelectedFlynn);
        }

        [TestMethod]
        public void NoOfferAndCandidateWalks()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(2);

            _moderator.AwardCandidates();

            Assert.AreEqual(0, candidates[0].Offers.Count(offer => offer.Hires.Any()));
            Assert.AreEqual(0, candidates[1].Offers.Count(offer => offer.Hires.Any()));
        }

        private static Offer MakeOffer(Company company, int chances, Guid candidateId)
        {
            Game game = company.Games.Single();
            Round round = game.Rounds.Single();
            Turn turn = company.CreateTurn(round);
            Candidate candidate = round.Candidates.Single(c => c.Unique == candidateId);
            return turn.CreateOffer(candidate, chances);
        }
    }
}
