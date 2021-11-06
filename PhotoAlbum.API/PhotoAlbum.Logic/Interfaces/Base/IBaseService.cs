using System;
using System.Collections.Generic;

namespace PhotoAlbum.Logic.Interfaces.Base
{
    public interface IBaseService<TDto> where TDto : class
    {
        TDto Get(Guid id);
        List<TDto> ListAll();
        TDto Add(TDto dto);
        TDto Update(TDto dto, Guid id);
        void Delete(Guid id);
    }
}
