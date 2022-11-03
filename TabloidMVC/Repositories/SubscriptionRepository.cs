using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class SubscriptionRepository : BaseRepository
    {
        public SubscriptionRepository(IConfiguration config) : base(config) { }



    }
}
