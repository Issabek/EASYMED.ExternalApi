using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyPortal.ExternalApi.Model
{
    //code  - 000 значить норм отработала
    //500 - ошибка которая обработана
    //с русс и казахским
    //404 - ошибка которую не ожидали
    //лучше ее не показывать клиенту

    public class RentRequestResult
    {
        public bool isError { get; set; }
        public string reply { get; set; }
        public string code { get; set; }
        public Description description { get; set; }
    }

    public class Description
    {
        public string ru { get; set; }
        public string kz { get; set; }
    }
}
