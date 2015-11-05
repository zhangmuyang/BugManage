using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTBugManage.Models
{

    public class BugModel
    {


        public String FBugName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FVersion为空")]
        public String FVersion { get; set; }

        
        public int FOS { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FMobile为空")]
        public String FMobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FOSVersion为空")]
        public String FOSVersion { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "F_Memo为空")]
        public String FMemo { get; set; }


        public List<String> FImageList { get; set; }

        
        public int FBugLevel { get; set; }


        
        public int FBugType { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "F_CreateName为空")]
        public String FCreateName { get; set; }




    }


    public class ImageCls {
        public String FImageList { get; set; }
    }
}