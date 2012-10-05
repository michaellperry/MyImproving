using UpdateControls.Correspondence;

namespace MyImproving.Model.Subscriptions
{
    public static class DomainSubscription
    {
        public static Community Subscribe(this Community community, Domain domain)
        {
            community
                .Subscribe(() => domain)
                .Subscribe(() => domain.Games)
                ;

            return community;
        }
    }
}
