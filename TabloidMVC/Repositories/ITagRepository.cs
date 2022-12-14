using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();

        void UpdateTag(Tag tag);

        void DeleteTag(int id);

        Tag GetTagById(int id);
    }
}
