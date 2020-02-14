using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    interface IBaseModel
    {
        string id { get; set; }
        DateTime create_time { get; set; }
        DateTime? last_time { get; set; }
        int state { get; set; }
    }
}
