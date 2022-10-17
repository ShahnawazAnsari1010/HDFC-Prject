using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDMS.Entities
{
    public class InwardUploadEntity:BaseEntity
    {
        public int? ID { get; set; }
        public int AGEEMENTNO { get; set; }
        public string BARCODE { get; set; }
        public string PRODUCTNAME { get; set; }
        public string DISBURSEMENTDATE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string SYSTEM { get; set; }
        public string CASESTARTERBRANCH { get; set; }
        public string GENERATEDON { get; set; }
        public string CPUID { get; set; }
        public string batch_id { get; set; }
        public string RepaymentMode { get; set; }

        public string VendorLocation { get; set; }

        public int VendorID { get; set; }

    }
}
