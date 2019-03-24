using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<string> Erros { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Erros = new List<string>();
        }
    }
}
