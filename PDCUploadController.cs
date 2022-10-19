using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using GDMS.Entities;
using GDMS.Repository.Interfaces;
using GDMS.WebAPI.Extensions;
using System.Web.Http.Cors;
using System.Configuration;

namespace GDMS.WebAPI.Controllers
{
    public class PDCUploadController : ApiController
    {
        // GET: InwardUpload
        IUploadRepository<PDCUploadEntity> objRepositary;
        public PDCUploadController(IUploadRepository<PDCUploadEntity> obj)
        {
            objRepositary = obj;
        }
        [HttpPost]
        [Route("api/PDCUpload/Create")]
        public HttpResponseMessage PDCUpload(List <PDCUploadEntity> objEntity)
        {
            try
            {
                string val = "";
                for(var i=0;i<objEntity.Count;i++)
                {
                     val = objRepositary.Add(objEntity[i]);
                     
                }
                return Request.CreateResponse(HttpStatusCode.OK, val);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
       
    }
}