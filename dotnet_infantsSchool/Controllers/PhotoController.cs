using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Helper;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Controllers
{
    /// <summary>
    /// 图片管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhotoController(IUserServices userServices, IHttpContextAccessor httpContextAccessor )
        {
            _userServices = userServices;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> UploadImg([FromServices] IWebHostEnvironment environment)
        {
            var data = new MessageModel<string>();
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = null;
            try
            {
                files = Request.Form.Files;
            }
            catch (Exception)
            {
                files = null;
            }

            if (files == null || !files.Any()) { data.Msg = "请选择上传的文件。"; return data; }
            //格式限制
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };

            string folderpath = Path.Combine(environment.WebRootPath, foldername);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    //foreach (var file in files)
                    var file = files.FirstOrDefault();
                    string strpath = Path.Combine(foldername, DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                    path = Path.Combine(environment.WebRootPath, strpath);

                    using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }

                    data = new MessageModel<string>()
                    {
                        Data = strpath,
                        Msg = "上传成功"
                    };
                    return data;
                }
                else
                    data.Success = false;
                {
                    data.Msg = "图片过大";
                    return data;
                }
            }
            else

            {
                data.Success = false;
                data.Msg = "图片格式错误";
                return data;
            }
        }
    }
}