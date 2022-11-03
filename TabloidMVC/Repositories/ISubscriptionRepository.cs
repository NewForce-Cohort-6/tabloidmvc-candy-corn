using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ISubscriptionRepository
    {
        void Add(Subscription subscription);
        List<Subscription> GetUserSubscriptions(int id);
    }
}
