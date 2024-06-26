using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuning.Library.Base
{
    public class ApiBaseResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiBaseResultModel()
        {
            Success = true;
        }

        public ApiBaseResultModel(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }
    }
}
