using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuning.Library.UserDetail
{
    public class TuningDetail
    {
        public Guid Id { get; set; }
        public string TuningPartOfCar { get; set; }
        public string Description { get; set; }
        public Guid CarId { get; set; }
    }
}
