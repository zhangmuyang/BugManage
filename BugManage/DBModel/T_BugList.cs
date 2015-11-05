using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zelo.Common.CustomAttributes;

namespace DBModel
{
    [Table(Name = "T_BugList")]
    public class T_BugList
    {
       
        //[Column(Name = "F_ID")]
        //public int FID { get; set; }
     
        [Id(Name = "F_ID", Strategy = GenerationType.INDENTITY)]
        public int FID { get; set; }

        [Column(Name = "F_GID")]
        public String FGID { get; set; }

        [Column(Name = "F_BugName")]
        public String FBugName { get; set; }

        [Column(Name = "F_Version")]
        public String FVersion { get; set; }

        [Column(Name = "F_OS")]
        public int FOS { get; set; }

        [Column(Name = "F_Mobile")]
        public String FMobile { get; set; }

        [Column(Name = "F_OSVersion")]
        public String FOSVersion { get; set; }

        [Column(Name = "F_Memo")]
        public String FMemo { get; set; }

        [Column(Name = "F_ImageList")]
        public String FImageList { get; set; }

        [Column(Name = "F_BugLevel")]
        public int FBugLevel { get; set; }

        [Column(Name = "F_BugType")]
        public int FBugType { get; set; }

        [Column(Name = "F_CreateName")]
        public String FCreateName { get; set; }

        [Column(Name = "F_STATUS")]
        public String FSTATUS { get; set; }

        [Column(Name = "F_CloseName")]
        public String FCloseName { get; set; }

        [Column(Name = "F_CloseMemo")]
        public String FCloseMemo { get; set; }

        [Column(Name = "F_INDATE")]
        public DateTime FINDATE { get; set; }

        [Column(Name = "F_EditDATE")]
        public DateTime FEditDATE { get; set; }

    }
}
