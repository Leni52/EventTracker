using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json;

namespace EventTracker.BLL.Exceptions
{
    public abstract class HttpException:Exception
    {    
        public  HttpStatusCode StatusCode { get; set; }
       
        public HttpException(string message):base(message)
        {
                     
        }      

    }
}
