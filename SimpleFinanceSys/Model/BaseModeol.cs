using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseModeol : IBaseModel
    {
        public string id { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? last_time { get; set; }
        public int state { get; set; }

        public string create_timeStr {
            get {
                return create_time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }

        public string create_dateStr {
            get { return create_time.ToString("yyyy-MM-dd"); }
        }

        public virtual void Create()
        {
            var model = this as IBaseModel;
            model.id = Guid.NewGuid().ToString();
            model.create_time = DateTime.Now;
            model.state = 0;
        }

        public virtual void Modify() {
            var model = this as IBaseModel;
            model.last_time = DateTime.Now;
        }
    }
}
