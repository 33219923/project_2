using System.Collections.Generic;

namespace PhotoAlbum.Repository.Interfaces.Base
{
    public interface ISearchableRepository<T> : IBaseRepository<T> where T : class
    {
        List<T> Search(string searchPhrase);
    }
}
