using System.Collections.Generic;

namespace NybSys.HttpRequest
{
    public class BaseAddressBuilder
    {
        public List<BaseAddress> lstAddress = new List<BaseAddress>();

        public void AddBaseAddress(string BaseAddressName, string BaseUri)
        {
            lstAddress.Add(new BaseAddress()
            {
                BaseAddressName = BaseAddressName,
                BaseAddressUri = BaseUri
            });
        }
    }

    public class BaseAddress
    {
        public string BaseAddressName { get; set; }
        public string BaseAddressUri { get; set; }
    }
}
