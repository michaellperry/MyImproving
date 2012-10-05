using UpdateControls.Correspondence;

namespace MyImproving.Model.Subscriptions
{
    public static class IndividualSubscription
    {
        public static Community Subscribe(this Community community, Individual individual)
        {
            community
                .Subscribe(() => individual)
                .Subscribe(() => individual.Companies)
                .Subscribe(() => individual.Games)
                ;
            return community;
        }
    }
}
