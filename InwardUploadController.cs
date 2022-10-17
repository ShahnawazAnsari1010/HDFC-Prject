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
    public class InwardUploadController : ApiController
    {
        // GET: InwardUpload
        IUploadRepository<InwardUploadEntity> objRepositary;
        public InwardUploadController(IUploadRepository<InwardUploadEntity> obj)
        {
            objRepositary = obj;
        }
        [HttpPost]
        [Route("api/InwardUpload/Create")]
        public HttpResponseMessage Post(List <InwardUploadEntity> objEntity)
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