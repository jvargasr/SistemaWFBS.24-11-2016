using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Contracts
{
    public interface IOperations<T>
    {
        bool Create();
        bool Read();
        bool Update();
        bool Delete();

        List<T> Listar();
    }
}
