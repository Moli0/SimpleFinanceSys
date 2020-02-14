using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    interface IBasePModel : IBaseModel
    {
        string create_user { get; set; }
        string last_user { get; set; }
    }
}
