using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Logic.Interfaces.Base
{
    public interface IBaseService<T>
    {
        T Get(Guid id);
        List<T> ListAll();
        T Add(T dto);
        T Update(T dto, Guid id);
        void Delete(Guid id);
    }
}
