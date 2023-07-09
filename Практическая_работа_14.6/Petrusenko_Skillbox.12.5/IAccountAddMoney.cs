using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrusenko_Skillbox._11._6
{
    interface IAccountAddMoney<out T>
    {
        T AccountAddMoney(double money);
    }
}
