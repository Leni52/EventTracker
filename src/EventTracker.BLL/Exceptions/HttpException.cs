using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json;

namespace EventTracker.BLL.Exceptions
{
    public abstract class HttpException:Exception
    {    
        public  HttpStatusCode StatusCode { get; set; }
        public string _message { get; set; }
        public HttpException(string message)
        {
            _message = message;            
        }      

    }
}
