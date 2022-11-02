using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetByEmail(string email);
        void AddUser(UserProfile user);
        void ChangeType(UserProfile user);

        /// <summary>
        /// Queries UserProfile table for admin count
        /// </summary>
        /// <returns>True if admin count = 1, else false</returns>
        bool IsLastAdmin(int id);
    }
}