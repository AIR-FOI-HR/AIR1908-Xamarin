using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;

namespace webservice
{
    public interface MyWebService
    {
        [Post("/stores.php")]
        Task<MyWebServiceResponse> getStores([Body(BodySerializationMethod.UrlEncoded)] Object method);

        [Post("/discounts.php")]
        Task<MyWebServiceResponse> getDiscounts([Body(BodySerializationMethod.UrlEncoded)] Object method);
    }
}
