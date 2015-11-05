using NTBugManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBModel;
using System.IO;
using System.Drawing;
using System.Web;
using DBService;
using NTBugManage.AppFliter;
using System.Text;

namespace NTBugManage.Controllers
{
    //[EnableCors(origins: "http://123.57.73.171:8066", headers: "*", methods: "*")]
    [CorsIControllerConfiguration]
    public class BugManageController : BaseController
    {
        #region
        [HttpGet]
        [HttpPost]
        [EnableCors]

        public Result<String> AddBug(BugModel bugModel)
        {
            //转换类并转存
            //T_BugList tbList = ConvertAdd(bugModel);

            Result<String> returnResult = Result<String>.CreateInstance(ResultCode.Fail);

            if (bugModel != null && GetService<BugService>().AddNewBug(ConvertAdd(bugModel)))
            {
                returnResult.SetSuccess();
                returnResult.message = "bug提交成功!";
                return returnResult;

            }
            returnResult.message = "bug提交失败!";
            return returnResult;




        }
        #endregion
        [HttpGet]
        [HttpPost]
        [EnableCors]
        public Result<String> SaveImage(ImageCls imageData)
        {
            Result<String> returnResult = Result<String>.CreateInstance(ResultCode.Fail);
            String kk = "data:img/jpg;base64,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";



            MemoryStream mStream = new MemoryStream(Convert.FromBase64String(imageData.FImageList));
            String fileName = Guid.NewGuid().ToString() + ".png";
            String fileUrl = "/UploadFiles/Images/" + fileName;
            Bitmap bitmap = new Bitmap(mStream);
            var filePath = HttpContext.Current.Server.MapPath(fileUrl);
            bitmap.Save(filePath);



            returnResult.SetSuccess();
            returnResult.message = "创建成功!";
            return returnResult;


        }
        [HttpGet]
        [HttpPost]
        [EnableCors]
        public Result<List<T_BugList>> BugList()
        {
            Result<List<T_BugList>> returnResult = Result<List<T_BugList>>.CreateInstance(ResultCode.Fail);

            returnResult.SetSuccess();
            returnResult.message = "查询成功!";
            returnResult.result_data = GetService<BugService>().BugList();
            return returnResult;

        }

        public T_BugList ConvertAdd(BugModel bugModel)
        {
            T_BugList tbList = new T_BugList();
            tbList.FGID = System.Guid.NewGuid().ToString();
            tbList.FBugName = bugModel.FBugName;
            tbList.FVersion = bugModel.FVersion;
            tbList.FOS = bugModel.FOS;
            tbList.FMobile = bugModel.FMobile;
            tbList.FOSVersion = bugModel.FOSVersion;
            tbList.FMemo = bugModel.FMemo;
            tbList.FBugLevel = bugModel.FBugLevel;
            tbList.FBugType = bugModel.FBugType;
            tbList.FCreateName = bugModel.FCreateName;
            tbList.FImageList = "";
            if (bugModel.FImageList != null && bugModel.FImageList.Count > 0)
            {
                for (int i = 0; i < bugModel.FImageList.Count; i++)
                {
                    MemoryStream mStream = new MemoryStream(Convert.FromBase64String(bugModel.FImageList[i]));
                    String fileName = Guid.NewGuid().ToString() + ".png";
                    String fileUrl = "/UploadFiles/Images/" + fileName;
                    Bitmap bitmap = new Bitmap(mStream);
                    var filePath = HttpContext.Current.Server.MapPath(fileUrl);
                    bitmap.Save(filePath);
                    if (i == 0) { tbList.FImageList = fileUrl; }
                    else { tbList.FImageList += "|" + fileUrl; }
                }

            }
            tbList.FINDATE = DateTime.Now;
            tbList.FEditDATE = DateTime.Now;

            return tbList;
        }



    }
}
