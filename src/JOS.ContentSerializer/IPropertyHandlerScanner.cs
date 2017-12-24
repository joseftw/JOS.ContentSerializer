using System;
using System.Collections.Generic;

namespace JOS.ContentSerializer
{
    public interface IPropertyHandlerScanner
    {
        void Scan();
        IEnumerable<Type> GetAllPropertyHandlers();
    }
}