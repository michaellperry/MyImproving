using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

/**
/ For use with http://graphviz.org/
digraph "MyImproving.Model"
{
    rankdir=BT
    Company -> Domain [color="red"]
    Company__name -> Company
    Company__name -> Company__name [label="  *"]
    Director -> Individual [color="red"]
    Director -> Company
    Game -> Domain [color="red"]
    Game -> Company [label="  *"] [color="red"]
    Game__name -> Game
    Game__name -> Game__name [label="  *"]
    Round -> Game [color="red"]
    Turn -> Company [color="red"]
    Turn -> Round
    Borrow -> Turn
    Repay -> Turn
    Candidate -> Round
    Offer -> Turn
    Offer -> Candidate
    Hire -> Offer
    Quit -> Hire
    Quit -> Turn
    Payroll -> Turn
    Payroll -> Hire
    Gig -> Round
    Gig -> Resource [label="  *"]
    Bid -> Turn
    Bid -> Gig
    Win -> Bid
    Lose -> Win
    Lose -> Turn
    Revenue -> Win
    Revenue -> Turn
}
**/

namespace MyImproving.Model
{
    public partial class Individual : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Individual newFact = new Individual(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Individual fact = (Individual)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Individual", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query MakeQueryCompanies()
		{
			return new Query()
				.JoinSuccessors(Director.RoleIndividual)
				.JoinPredecessors(Director.RoleCompany)
            ;
		}
        public static Query QueryCompanies = MakeQueryCompanies();
        public static Query MakeQueryGames()
		{
			return new Query()
				.JoinSuccessors(Director.RoleIndividual)
				.JoinPredecessors(Director.RoleCompany)
				.JoinSuccessors(Game.RoleCompanies)
            ;
		}
        public static Query QueryGames = MakeQueryGames();

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Company> _companies;
        private Result<Game> _games;

        // Business constructor
        public Individual(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Individual(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _companies = new Result<Company>(this, QueryCompanies);
            _games = new Result<Game>(this, QueryGames);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Company> Companies
        {
            get { return _companies; }
        }
        public Result<Game> Games
        {
            get { return _games; }
        }

        // Mutable property access

    }
    
    public partial class Domain : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Domain newFact = new Domain(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._name = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Domain fact = (Domain)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._name);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Domain", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query MakeQueryCompanies()
		{
			return new Query()
				.JoinSuccessors(Company.RoleDomain)
            ;
		}
        public static Query QueryCompanies = MakeQueryCompanies();
        public static Query MakeQueryGames()
		{
			return new Query()
				.JoinSuccessors(Game.RoleDomain)
            ;
		}
        public static Query QueryGames = MakeQueryGames();

        // Predicates

        // Predecessors

        // Fields
        private string _name;

        // Results
        private Result<Company> _companies;
        private Result<Game> _games;

        // Business constructor
        public Domain(
            string name
            )
        {
            InitializeResults();
            _name = name;
        }

        // Hydration constructor
        private Domain(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _companies = new Result<Company>(this, QueryCompanies);
            _games = new Result<Game>(this, QueryGames);
        }

        // Predecessor access

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access
        public Result<Company> Companies
        {
            get { return _companies; }
        }
        public Result<Game> Games
        {
            get { return _games; }
        }

        // Mutable property access

    }
    
    public partial class Company : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Company newFact = new Company(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Company fact = (Company)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Company", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDomain = new Role(new RoleMemento(
			_correspondenceFactType,
			"domain",
			new CorrespondenceFactType("MyImproving.Model.Domain", 1),
			true));

        // Queries
        public static Query MakeQueryName()
		{
			return new Query()
				.JoinSuccessors(Company__name.RoleCompany, Condition.WhereIsEmpty(Company__name.MakeQueryIsCurrent())
				)
            ;
		}
        public static Query QueryName = MakeQueryName();
        public static Query MakeQueryGames()
		{
			return new Query()
				.JoinSuccessors(Game.RoleCompanies)
            ;
		}
        public static Query QueryGames = MakeQueryGames();

        // Predicates

        // Predecessors
        private PredecessorObj<Domain> _domain;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Company__name> _name;
        private Result<Game> _games;

        // Business constructor
        public Company(
            Domain domain
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _domain = new PredecessorObj<Domain>(this, RoleDomain, domain);
        }

        // Hydration constructor
        private Company(FactMemento memento)
        {
            InitializeResults();
            _domain = new PredecessorObj<Domain>(this, RoleDomain, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Company__name>(this, QueryName);
            _games = new Result<Game>(this, QueryGames);
        }

        // Predecessor access
        public Domain Domain
        {
            get { return _domain.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Game> Games
        {
            get { return _games; }
        }

        // Mutable property access
        public TransientDisputable<Company__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Company__name(this, _name, value.Value));
				}
			}
        }

    }
    
    public partial class Company__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Company__name newFact = new Company__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Company__name fact = (Company__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Company__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("MyImproving.Model.Company", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("MyImproving.Model.Company__name", 1),
			false));

        // Queries
        public static Query MakeQueryIsCurrent()
		{
			return new Query()
				.JoinSuccessors(Company__name.RolePrior)
            ;
		}
        public static Query QueryIsCurrent = MakeQueryIsCurrent();

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Company> _company;
        private PredecessorList<Company__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Company__name(
            Company company
            ,IEnumerable<Company__name> prior
            ,string value
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _prior = new PredecessorList<Company__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Company__name(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
            _prior = new PredecessorList<Company__name>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }
        public IEnumerable<Company__name> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Director : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Director newFact = new Director(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Director fact = (Director)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Director", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleIndividual = new Role(new RoleMemento(
			_correspondenceFactType,
			"individual",
			new CorrespondenceFactType("MyImproving.Model.Individual", 1),
			true));
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("MyImproving.Model.Company", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Individual> _individual;
        private PredecessorObj<Company> _company;

        // Fields

        // Results

        // Business constructor
        public Director(
            Individual individual
            ,Company company
            )
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, RoleIndividual, individual);
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
        }

        // Hydration constructor
        private Director(FactMemento memento)
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, RoleIndividual, memento);
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Individual Individual
        {
            get { return _individual.Fact; }
        }
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Game : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Game newFact = new Game(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Game fact = (Game)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Game", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDomain = new Role(new RoleMemento(
			_correspondenceFactType,
			"domain",
			new CorrespondenceFactType("MyImproving.Model.Domain", 1),
			true));
        public static Role RoleCompanies = new Role(new RoleMemento(
			_correspondenceFactType,
			"companies",
			new CorrespondenceFactType("MyImproving.Model.Company", 1),
			true));

        // Queries
        public static Query MakeQueryName()
		{
			return new Query()
				.JoinSuccessors(Game__name.RoleGame, Condition.WhereIsEmpty(Game__name.MakeQueryIsCurrent())
				)
            ;
		}
        public static Query QueryName = MakeQueryName();
        public static Query MakeQueryRounds()
		{
			return new Query()
				.JoinSuccessors(Round.RoleGame)
            ;
		}
        public static Query QueryRounds = MakeQueryRounds();

        // Predicates

        // Predecessors
        private PredecessorObj<Domain> _domain;
        private PredecessorList<Company> _companies;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Game__name> _name;
        private Result<Round> _rounds;

        // Business constructor
        public Game(
            Domain domain
            ,IEnumerable<Company> companies
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _domain = new PredecessorObj<Domain>(this, RoleDomain, domain);
            _companies = new PredecessorList<Company>(this, RoleCompanies, companies);
        }

        // Hydration constructor
        private Game(FactMemento memento)
        {
            InitializeResults();
            _domain = new PredecessorObj<Domain>(this, RoleDomain, memento);
            _companies = new PredecessorList<Company>(this, RoleCompanies, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Game__name>(this, QueryName);
            _rounds = new Result<Round>(this, QueryRounds);
        }

        // Predecessor access
        public Domain Domain
        {
            get { return _domain.Fact; }
        }
        public IEnumerable<Company> Companies
        {
            get { return _companies; }
        }
     
        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Round> Rounds
        {
            get { return _rounds; }
        }

        // Mutable property access
        public TransientDisputable<Game__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Game__name(this, _name, value.Value));
				}
			}
        }

    }
    
    public partial class Game__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Game__name newFact = new Game__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Game__name fact = (Game__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Game__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleGame = new Role(new RoleMemento(
			_correspondenceFactType,
			"game",
			new CorrespondenceFactType("MyImproving.Model.Game", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("MyImproving.Model.Game__name", 1),
			false));

        // Queries
        public static Query MakeQueryIsCurrent()
		{
			return new Query()
				.JoinSuccessors(Game__name.RolePrior)
            ;
		}
        public static Query QueryIsCurrent = MakeQueryIsCurrent();

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Game> _game;
        private PredecessorList<Game__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Game__name(
            Game game
            ,IEnumerable<Game__name> prior
            ,string value
            )
        {
            InitializeResults();
            _game = new PredecessorObj<Game>(this, RoleGame, game);
            _prior = new PredecessorList<Game__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Game__name(FactMemento memento)
        {
            InitializeResults();
            _game = new PredecessorObj<Game>(this, RoleGame, memento);
            _prior = new PredecessorList<Game__name>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Game Game
        {
            get { return _game.Fact; }
        }
        public IEnumerable<Game__name> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Round : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Round newFact = new Round(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._index = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Round fact = (Round)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._index);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Round", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleGame = new Role(new RoleMemento(
			_correspondenceFactType,
			"game",
			new CorrespondenceFactType("MyImproving.Model.Game", 1),
			true));

        // Queries
        public static Query MakeQueryCandidates()
		{
			return new Query()
				.JoinSuccessors(Candidate.RoleRound)
            ;
		}
        public static Query QueryCandidates = MakeQueryCandidates();

        // Predicates

        // Predecessors
        private PredecessorObj<Game> _game;

        // Fields
        private int _index;

        // Results
        private Result<Candidate> _candidates;

        // Business constructor
        public Round(
            Game game
            ,int index
            )
        {
            InitializeResults();
            _game = new PredecessorObj<Game>(this, RoleGame, game);
            _index = index;
        }

        // Hydration constructor
        private Round(FactMemento memento)
        {
            InitializeResults();
            _game = new PredecessorObj<Game>(this, RoleGame, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _candidates = new Result<Candidate>(this, QueryCandidates);
        }

        // Predecessor access
        public Game Game
        {
            get { return _game.Fact; }
        }

        // Field access
        public int Index
        {
            get { return _index; }
        }

        // Query result access
        public Result<Candidate> Candidates
        {
            get { return _candidates; }
        }

        // Mutable property access

    }
    
    public partial class Turn : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Turn newFact = new Turn(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Turn fact = (Turn)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Turn", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("MyImproving.Model.Company", 1),
			true));
        public static Role RoleRound = new Role(new RoleMemento(
			_correspondenceFactType,
			"round",
			new CorrespondenceFactType("MyImproving.Model.Round", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;
        private PredecessorObj<Round> _round;

        // Fields

        // Results

        // Business constructor
        public Turn(
            Company company
            ,Round round
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _round = new PredecessorObj<Round>(this, RoleRound, round);
        }

        // Hydration constructor
        private Turn(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
            _round = new PredecessorObj<Round>(this, RoleRound, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }
        public Round Round
        {
            get { return _round.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Borrow : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Borrow newFact = new Borrow(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._amount = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Borrow fact = (Borrow)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._amount);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Borrow", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Turn> _turn;

        // Fields
        private int _amount;

        // Results

        // Business constructor
        public Borrow(
            Turn turn
            ,int amount
            )
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
            _amount = amount;
        }

        // Hydration constructor
        private Borrow(FactMemento memento)
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Turn Turn
        {
            get { return _turn.Fact; }
        }

        // Field access
        public int Amount
        {
            get { return _amount; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Repay : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Repay newFact = new Repay(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._amount = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Repay fact = (Repay)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._amount);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Repay", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Turn> _turn;

        // Fields
        private int _amount;

        // Results

        // Business constructor
        public Repay(
            Turn turn
            ,int amount
            )
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
            _amount = amount;
        }

        // Hydration constructor
        private Repay(FactMemento memento)
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Turn Turn
        {
            get { return _turn.Fact; }
        }

        // Field access
        public int Amount
        {
            get { return _amount; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Candidate : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Candidate newFact = new Candidate(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
						newFact._skill = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
						newFact._relationship = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Candidate fact = (Candidate)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._skill);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._relationship);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Candidate", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleRound = new Role(new RoleMemento(
			_correspondenceFactType,
			"round",
			new CorrespondenceFactType("MyImproving.Model.Round", 1),
			false));

        // Queries
        public static Query MakeQueryOffers()
		{
			return new Query()
				.JoinSuccessors(Offer.RoleCandidate)
            ;
		}
        public static Query QueryOffers = MakeQueryOffers();

        // Predicates

        // Predecessors
        private PredecessorObj<Round> _round;

        // Unique
        private Guid _unique;

        // Fields
        private int _skill;
        private int _relationship;

        // Results
        private Result<Offer> _offers;

        // Business constructor
        public Candidate(
            Round round
            ,int skill
            ,int relationship
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _round = new PredecessorObj<Round>(this, RoleRound, round);
            _skill = skill;
            _relationship = relationship;
        }

        // Hydration constructor
        private Candidate(FactMemento memento)
        {
            InitializeResults();
            _round = new PredecessorObj<Round>(this, RoleRound, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _offers = new Result<Offer>(this, QueryOffers);
        }

        // Predecessor access
        public Round Round
        {
            get { return _round.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }

        public int Skill
        {
            get { return _skill; }
        }
        public int Relationship
        {
            get { return _relationship; }
        }

        // Query result access
        public Result<Offer> Offers
        {
            get { return _offers; }
        }

        // Mutable property access

    }
    
    public partial class Offer : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Offer newFact = new Offer(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._chances = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Offer fact = (Offer)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._chances);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Offer", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));
        public static Role RoleCandidate = new Role(new RoleMemento(
			_correspondenceFactType,
			"candidate",
			new CorrespondenceFactType("MyImproving.Model.Candidate", 1),
			false));

        // Queries
        public static Query MakeQueryHires()
		{
			return new Query()
				.JoinSuccessors(Hire.RoleOffer)
            ;
		}
        public static Query QueryHires = MakeQueryHires();

        // Predicates

        // Predecessors
        private PredecessorObj<Turn> _turn;
        private PredecessorObj<Candidate> _candidate;

        // Fields
        private int _chances;

        // Results
        private Result<Hire> _hires;

        // Business constructor
        public Offer(
            Turn turn
            ,Candidate candidate
            ,int chances
            )
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
            _candidate = new PredecessorObj<Candidate>(this, RoleCandidate, candidate);
            _chances = chances;
        }

        // Hydration constructor
        private Offer(FactMemento memento)
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
            _candidate = new PredecessorObj<Candidate>(this, RoleCandidate, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _hires = new Result<Hire>(this, QueryHires);
        }

        // Predecessor access
        public Turn Turn
        {
            get { return _turn.Fact; }
        }
        public Candidate Candidate
        {
            get { return _candidate.Fact; }
        }

        // Field access
        public int Chances
        {
            get { return _chances; }
        }

        // Query result access
        public Result<Hire> Hires
        {
            get { return _hires; }
        }

        // Mutable property access

    }
    
    public partial class Hire : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Hire newFact = new Hire(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Hire fact = (Hire)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Hire", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleOffer = new Role(new RoleMemento(
			_correspondenceFactType,
			"offer",
			new CorrespondenceFactType("MyImproving.Model.Offer", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Offer> _offer;

        // Fields

        // Results

        // Business constructor
        public Hire(
            Offer offer
            )
        {
            InitializeResults();
            _offer = new PredecessorObj<Offer>(this, RoleOffer, offer);
        }

        // Hydration constructor
        private Hire(FactMemento memento)
        {
            InitializeResults();
            _offer = new PredecessorObj<Offer>(this, RoleOffer, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Offer Offer
        {
            get { return _offer.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Quit : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Quit newFact = new Quit(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Quit fact = (Quit)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Quit", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleHire = new Role(new RoleMemento(
			_correspondenceFactType,
			"hire",
			new CorrespondenceFactType("MyImproving.Model.Hire", 1),
			false));
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Hire> _hire;
        private PredecessorObj<Turn> _turn;

        // Fields

        // Results

        // Business constructor
        public Quit(
            Hire hire
            ,Turn turn
            )
        {
            InitializeResults();
            _hire = new PredecessorObj<Hire>(this, RoleHire, hire);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
        }

        // Hydration constructor
        private Quit(FactMemento memento)
        {
            InitializeResults();
            _hire = new PredecessorObj<Hire>(this, RoleHire, memento);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Hire Hire
        {
            get { return _hire.Fact; }
        }
        public Turn Turn
        {
            get { return _turn.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Payroll : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Payroll newFact = new Payroll(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Payroll fact = (Payroll)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Payroll", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));
        public static Role RoleHire = new Role(new RoleMemento(
			_correspondenceFactType,
			"hire",
			new CorrespondenceFactType("MyImproving.Model.Hire", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Turn> _turn;
        private PredecessorObj<Hire> _hire;

        // Fields

        // Results

        // Business constructor
        public Payroll(
            Turn turn
            ,Hire hire
            )
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
            _hire = new PredecessorObj<Hire>(this, RoleHire, hire);
        }

        // Hydration constructor
        private Payroll(FactMemento memento)
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
            _hire = new PredecessorObj<Hire>(this, RoleHire, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Turn Turn
        {
            get { return _turn.Fact; }
        }
        public Hire Hire
        {
            get { return _hire.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Resource : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Resource newFact = new Resource(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._skill = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
						newFact._need = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
						newFact._revenue = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Resource fact = (Resource)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._skill);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._need);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._revenue);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Resource", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private int _skill;
        private int _need;
        private int _revenue;

        // Results

        // Business constructor
        public Resource(
            int skill
            ,int need
            ,int revenue
            )
        {
            InitializeResults();
            _skill = skill;
            _need = need;
            _revenue = revenue;
        }

        // Hydration constructor
        private Resource(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public int Skill
        {
            get { return _skill; }
        }
        public int Need
        {
            get { return _need; }
        }
        public int Revenue
        {
            get { return _revenue; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Gig : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Gig newFact = new Gig(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
						newFact._duration = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
						newFact._term = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Gig fact = (Gig)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._duration);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._term);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Gig", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleRound = new Role(new RoleMemento(
			_correspondenceFactType,
			"round",
			new CorrespondenceFactType("MyImproving.Model.Round", 1),
			false));
        public static Role RoleResources = new Role(new RoleMemento(
			_correspondenceFactType,
			"resources",
			new CorrespondenceFactType("MyImproving.Model.Resource", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Round> _round;
        private PredecessorList<Resource> _resources;

        // Unique
        private Guid _unique;

        // Fields
        private int _duration;
        private int _term;

        // Results

        // Business constructor
        public Gig(
            Round round
            ,IEnumerable<Resource> resources
            ,int duration
            ,int term
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _round = new PredecessorObj<Round>(this, RoleRound, round);
            _resources = new PredecessorList<Resource>(this, RoleResources, resources);
            _duration = duration;
            _term = term;
        }

        // Hydration constructor
        private Gig(FactMemento memento)
        {
            InitializeResults();
            _round = new PredecessorObj<Round>(this, RoleRound, memento);
            _resources = new PredecessorList<Resource>(this, RoleResources, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Round Round
        {
            get { return _round.Fact; }
        }
        public IEnumerable<Resource> Resources
        {
            get { return _resources; }
        }
     
        // Field access
		public Guid Unique { get { return _unique; } }

        public int Duration
        {
            get { return _duration; }
        }
        public int Term
        {
            get { return _term; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Bid : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Bid newFact = new Bid(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._chances = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Bid fact = (Bid)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._chances);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Bid", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));
        public static Role RoleGig = new Role(new RoleMemento(
			_correspondenceFactType,
			"gig",
			new CorrespondenceFactType("MyImproving.Model.Gig", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Turn> _turn;
        private PredecessorObj<Gig> _gig;

        // Fields
        private int _chances;

        // Results

        // Business constructor
        public Bid(
            Turn turn
            ,Gig gig
            ,int chances
            )
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
            _gig = new PredecessorObj<Gig>(this, RoleGig, gig);
            _chances = chances;
        }

        // Hydration constructor
        private Bid(FactMemento memento)
        {
            InitializeResults();
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
            _gig = new PredecessorObj<Gig>(this, RoleGig, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Turn Turn
        {
            get { return _turn.Fact; }
        }
        public Gig Gig
        {
            get { return _gig.Fact; }
        }

        // Field access
        public int Chances
        {
            get { return _chances; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Win : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Win newFact = new Win(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Win fact = (Win)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Win", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleBid = new Role(new RoleMemento(
			_correspondenceFactType,
			"bid",
			new CorrespondenceFactType("MyImproving.Model.Bid", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Bid> _bid;

        // Fields

        // Results

        // Business constructor
        public Win(
            Bid bid
            )
        {
            InitializeResults();
            _bid = new PredecessorObj<Bid>(this, RoleBid, bid);
        }

        // Hydration constructor
        private Win(FactMemento memento)
        {
            InitializeResults();
            _bid = new PredecessorObj<Bid>(this, RoleBid, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Bid Bid
        {
            get { return _bid.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Lose : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Lose newFact = new Lose(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Lose fact = (Lose)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Lose", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleWin = new Role(new RoleMemento(
			_correspondenceFactType,
			"win",
			new CorrespondenceFactType("MyImproving.Model.Win", 1),
			false));
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Win> _win;
        private PredecessorObj<Turn> _turn;

        // Fields

        // Results

        // Business constructor
        public Lose(
            Win win
            ,Turn turn
            )
        {
            InitializeResults();
            _win = new PredecessorObj<Win>(this, RoleWin, win);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
        }

        // Hydration constructor
        private Lose(FactMemento memento)
        {
            InitializeResults();
            _win = new PredecessorObj<Win>(this, RoleWin, memento);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Win Win
        {
            get { return _win.Fact; }
        }
        public Turn Turn
        {
            get { return _turn.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Revenue : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Revenue newFact = new Revenue(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Revenue fact = (Revenue)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"MyImproving.Model.Revenue", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleWin = new Role(new RoleMemento(
			_correspondenceFactType,
			"win",
			new CorrespondenceFactType("MyImproving.Model.Win", 1),
			false));
        public static Role RoleTurn = new Role(new RoleMemento(
			_correspondenceFactType,
			"turn",
			new CorrespondenceFactType("MyImproving.Model.Turn", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Win> _win;
        private PredecessorObj<Turn> _turn;

        // Fields

        // Results

        // Business constructor
        public Revenue(
            Win win
            ,Turn turn
            )
        {
            InitializeResults();
            _win = new PredecessorObj<Win>(this, RoleWin, win);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, turn);
        }

        // Hydration constructor
        private Revenue(FactMemento memento)
        {
            InitializeResults();
            _win = new PredecessorObj<Win>(this, RoleWin, memento);
            _turn = new PredecessorObj<Turn>(this, RoleTurn, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Win Win
        {
            get { return _win.Fact; }
        }
        public Turn Turn
        {
            get { return _turn.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.QueryCompanies.QueryDefinition);
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.QueryGames.QueryDefinition);
			community.AddType(
				Domain._correspondenceFactType,
				new Domain.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Domain._correspondenceFactType }));
			community.AddQuery(
				Domain._correspondenceFactType,
				Domain.QueryCompanies.QueryDefinition);
			community.AddQuery(
				Domain._correspondenceFactType,
				Domain.QueryGames.QueryDefinition);
			community.AddType(
				Company._correspondenceFactType,
				new Company.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company._correspondenceFactType }));
			community.AddQuery(
				Company._correspondenceFactType,
				Company.QueryName.QueryDefinition);
			community.AddQuery(
				Company._correspondenceFactType,
				Company.QueryGames.QueryDefinition);
			community.AddType(
				Company__name._correspondenceFactType,
				new Company__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company__name._correspondenceFactType }));
			community.AddQuery(
				Company__name._correspondenceFactType,
				Company__name.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Director._correspondenceFactType,
				new Director.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Director._correspondenceFactType }));
			community.AddType(
				Game._correspondenceFactType,
				new Game.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Game._correspondenceFactType }));
			community.AddQuery(
				Game._correspondenceFactType,
				Game.QueryName.QueryDefinition);
			community.AddQuery(
				Game._correspondenceFactType,
				Game.QueryRounds.QueryDefinition);
			community.AddType(
				Game__name._correspondenceFactType,
				new Game__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Game__name._correspondenceFactType }));
			community.AddQuery(
				Game__name._correspondenceFactType,
				Game__name.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Round._correspondenceFactType,
				new Round.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Round._correspondenceFactType }));
			community.AddQuery(
				Round._correspondenceFactType,
				Round.QueryCandidates.QueryDefinition);
			community.AddType(
				Turn._correspondenceFactType,
				new Turn.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Turn._correspondenceFactType }));
			community.AddType(
				Borrow._correspondenceFactType,
				new Borrow.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Borrow._correspondenceFactType }));
			community.AddType(
				Repay._correspondenceFactType,
				new Repay.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Repay._correspondenceFactType }));
			community.AddType(
				Candidate._correspondenceFactType,
				new Candidate.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Candidate._correspondenceFactType }));
			community.AddQuery(
				Candidate._correspondenceFactType,
				Candidate.QueryOffers.QueryDefinition);
			community.AddType(
				Offer._correspondenceFactType,
				new Offer.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Offer._correspondenceFactType }));
			community.AddQuery(
				Offer._correspondenceFactType,
				Offer.QueryHires.QueryDefinition);
			community.AddType(
				Hire._correspondenceFactType,
				new Hire.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Hire._correspondenceFactType }));
			community.AddType(
				Quit._correspondenceFactType,
				new Quit.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Quit._correspondenceFactType }));
			community.AddType(
				Payroll._correspondenceFactType,
				new Payroll.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Payroll._correspondenceFactType }));
			community.AddType(
				Resource._correspondenceFactType,
				new Resource.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Resource._correspondenceFactType }));
			community.AddType(
				Gig._correspondenceFactType,
				new Gig.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Gig._correspondenceFactType }));
			community.AddType(
				Bid._correspondenceFactType,
				new Bid.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Bid._correspondenceFactType }));
			community.AddType(
				Win._correspondenceFactType,
				new Win.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Win._correspondenceFactType }));
			community.AddType(
				Lose._correspondenceFactType,
				new Lose.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Lose._correspondenceFactType }));
			community.AddType(
				Revenue._correspondenceFactType,
				new Revenue.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Revenue._correspondenceFactType }));
		}
	}
}
