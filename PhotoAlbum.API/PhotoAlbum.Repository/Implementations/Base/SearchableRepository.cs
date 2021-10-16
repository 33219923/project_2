using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class SearchableRepository<T> : BaseRepository<T>, ISearchableRepository<T> where T : class
    {
        public virtual List<T> Search(string searchPhrase)
        {
            throw new NotImplementedException();
        }
    }
}
