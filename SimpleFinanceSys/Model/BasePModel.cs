using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BasePModel : BaseModeol,IBasePModel
    {
        public string create_user { get; set; }
        public string last_user { get; set; }

        public override void Create()
        {
            base.Create();
            var model = this as IBasePModel;
        }

        public override void Modify()
        {
            base.Modify();
            var model = this as IBaseModel;
        }
    }
}
