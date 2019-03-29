using MyEvernote.Entities.Messages;
using System.Collections.Generic;

namespace MyEvernote.BusinessLayer.Result
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
