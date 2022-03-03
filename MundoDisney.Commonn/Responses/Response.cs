using System;
using System.Collections.Generic;
using System.Text;

namespace MundoDisney.Commonn.Responses
{
   public class Response
    {
        public string Message { get; set; }

        public object Result { get; set; }
        public bool IsSuccess { get; set; }
    }
}
