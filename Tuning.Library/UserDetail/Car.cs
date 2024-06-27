using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuning.Library.UserDetail
{
    public class Car
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int MadeYear { get; set; }
        public string UserId { get; set; }
        public List<TuningDetail> TuningDetails { get; set; } = new List<TuningDetail>();
    }
}
