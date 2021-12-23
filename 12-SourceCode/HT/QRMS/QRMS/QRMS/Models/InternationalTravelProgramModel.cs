using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class InternationalTravelProgramModel : Notifiable
    {
        public int ID { get; set; }
        public string INS_PROGRAM_CODE { get; set; }
        public string INS_PROGRAM_NAME { get; set; }
        public Nullable<decimal> INS_PROGRAM_VALUE { get; set; }
        public Nullable<System.DateTime> ACTIVE_DATE_FROM { get; set; }
        public Nullable<System.DateTime> ACTIVE_DATE_TO { get; set; }
        public string REMARK { get; set; }

        public bool IsSelected { get; set; }
        public string ProgramType => INS_PROGRAM_CODE + " - " + string.Format("{0:#,#}", INS_PROGRAM_VALUE);
    }
}
