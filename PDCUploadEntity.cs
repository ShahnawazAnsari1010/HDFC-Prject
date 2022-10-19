using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDMS.Entities
{
    public class PDCUploadEntity : BaseEntity
    {
        public int? ID { get; set; }
        public int AGREEMENTID { get; set; }
        public string APPROVALDATE { get; set; }
        public int SecurityPDCCHQCount { get; set; }
    }
}
