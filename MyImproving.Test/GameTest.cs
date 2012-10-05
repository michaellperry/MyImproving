using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyImproving.Model;
using MyImproving.Model.Services;
using MyImproving.Model.Machine;

namespace MyImproving.Test
{
    [TestClass]
    public class GameTest : TestBase
    {
        private Game _gameModerator;

        private CompanyGameService _alan;
        private CompanyGameService _flynn;
        private ModeratorService _moderator;

        private Actuator _actuator;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCommunity();

            var companyAlan = _individualAlan.CreateCompany();
            companyAlan.Name = "Encom";

            var companyFlynn = _individualFlynn.CreateCompany();
            companyFlynn.Name = "Flynn's";

            Synchronize();

            _gameModerator = _domainModerator.CreateGame(_domainModerator.Companies);
            _moderator = new ModeratorService(_gameModerator);

            Synchronize();

            var gameAlan = companyAlan.Games.Single();
            var gameFlynn = companyFlynn.Games.Single();

            _alan = new CompanyGameService(companyAlan, gameAlan);
            _flynn = new CompanyGameService(companyFlynn, gameFlynn);

            _actuator = new Actuator();
            _alan.RegisterWith(_actuator);
            _flynn.RegisterWith(_actuator);

            _actuator.Start();
        }

        [TestMethod]
        public void ModeratorBeginsARound()
        {
            _moderator.BeginNextRound();

            Synchronize();
            _actuator.Quiesce();

            Assert.AreEqual(1, _alan.Turns.Count());
            Assert.AreEqual(1, _alan.Turns.Single().Round.Index);
            Assert.AreEqual(1, _flynn.Turns.Count());
            Assert.AreEqual(1, _flynn.Turns.Single().Round.Index);
        }

        [TestMethod]
        public void ModeratorDealsCandidateCards()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(4);

            Synchronize();

            Round roundAlan = _alan._game.Rounds.Single();
            Assert.AreEqual(4, roundAlan.Candidates.Count());
            foreach (var candidate in candidates)
            {
                Assert.IsTrue(roundAlan.Candidates.Any(c =>
                    c.Skill == candidate.Skill &&
                    c.Relationship == candidate.Relationship));
            }

            Round roundFlynn = _flynn._game.Rounds.Single();
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

            MakeOffer(_flynn._company, 1, candidate.Unique);

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

            Offer offerAlan1 = MakeOffer(_alan._company, 3, candidates[0].Unique);
            Offer offerAlan2 = MakeOffer(_alan._company, 1, candidates[1].Unique);

            Offer offerFlynn1 = MakeOffer(_flynn._company, 2, candidates[0].Unique);
            Offer offerFlynn2 = MakeOffer(_flynn._company, 7, candidates[1].Unique);

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

            Offer offerAlan1 = MakeOffer(_alan._company, 3, candidates[0].Unique);

            Offer offerFlynn2 = MakeOffer(_flynn._company, 7, candidates[1].Unique);

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

        [TestMethod]
        public void CandidateSelectionIsFair()
        {
            _moderator.BeginNextRound();
            List<Candidate> candidates = _moderator.DealCandidates(100);

            Synchronize();

            foreach (var candidate in candidates)
            {
                MakeOffer(_alan._company, 3, candidate.Unique);
                MakeOffer(_flynn._company, 7, candidate.Unique);
            }

            Synchronize();

            _moderator.AwardCandidates();

            Synchronize();

            int hiresAlan = _alan._game.Rounds
                .SelectMany(round => round.Turns)
                .Where(turn => turn.Company == _alan._company)
                .SelectMany(turn => turn.Hires)
                .Count();
            int hiresFlynn = _flynn._game.Rounds
                .SelectMany(round => round.Turns)
                .Where(turn => turn.Company == _flynn._company)
                .SelectMany(turn => turn.Hires)
                .Count();

            Assert.AreEqual(100, hiresAlan + hiresFlynn);
            // This test will sometimes fail.
            Assert.IsTrue(25 < hiresAlan && hiresAlan < 35, String.Format("Alan hired {0} candidates. He should hire close to 30.", hiresAlan));
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
