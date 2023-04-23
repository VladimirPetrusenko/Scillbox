using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrusenko_Skillbox._11._6
{
    interface ITransaction<in T>
    {
        void Transact(T transmoney);
    }
}
