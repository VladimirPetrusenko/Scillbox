using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantManagerLibrary
{
    public interface IAccountAddMoney<out T>
    {
        T AccountAddMoney(double money);
    }
}
