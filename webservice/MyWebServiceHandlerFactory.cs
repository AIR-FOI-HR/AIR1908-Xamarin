using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public class MyWebServiceHandlerFactory
    {
        public static MyWebServiceHandler GetHandler<T>()
        {
            if (typeof(T) == typeof(Store))
            {
                return new StoresHandler();
            }
            else if (typeof(T) == typeof(Discount))
            {
                return new HandlerDiscounts();
            }
            else
                return null;
        }
    }
}
