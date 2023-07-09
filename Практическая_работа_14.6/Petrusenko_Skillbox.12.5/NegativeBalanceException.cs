using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Petrusenko_Skillbox._11._6
{
    class NegativeBalanceException: Exception
    {
        public void ErrorMessage()
        {
            MessageBox.Show("Сумма на счете указана отрицательной, будет нулевое значение");
        }
    }
}
