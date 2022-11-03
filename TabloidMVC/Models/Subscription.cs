using System;

namespace TabloidMVC.Models
{
    public class Subscription
    {
        public int Id { get; set; } 
        public int SubscriberUserProfileId { get; set; }
        public int ProviderUserProfileId { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
