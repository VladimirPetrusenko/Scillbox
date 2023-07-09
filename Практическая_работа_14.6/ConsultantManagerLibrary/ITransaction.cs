using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantManagerLibrary
{
    public interface ITransaction<in T>
    {
        void Transact(T transmoney);
    }
}
