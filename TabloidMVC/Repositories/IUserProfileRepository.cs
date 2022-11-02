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

        /// <summary>
        /// Change Active column for UserProfile from 1 (true) to 0 (false)
        /// </summary>
        /// <param name="id">The pk id of the user to deactivate</param>
        void Deactivate(int id);

        /// <summary>
        /// Change Active column for UserProfile from 0 (false) to 1 (true)
        /// </summary>
        /// <param name="id">The pk id of the user to activate</param>
        void Activate(int id);
    }
}