using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public interface MyWebServiceHandler
    {
        void onDataArrived(Object result, bool ok, long timestamp);
    }
}
