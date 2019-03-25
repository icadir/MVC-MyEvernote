using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities.Messages;

namespace MyEvernote.BusinessLayer
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<ErrorMessagaObj> Erros { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Erros = new List<ErrorMessagaObj>();
        }

        public void AddError(ErrorMessageCode code, string message)
        {
            Erros.Add(new ErrorMessagaObj()
            {
                Code = code,
                Message = message
            });
        }
    }
}
