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
namespace GDMS.Repository.Interfaces
{
    public class CourierController : ApiController
    {
		// GET: Courier
		ICourierRepository<CourierEntity> objRepositary;
		public CourierController (ICourierRepository<CourierEntity>obj)
        {
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/Courier/GetList")]
		public HttpResponseMessage GetList([FromUri] string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.Get();
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
        [HttpGet]
        [Route("api/Courier/GetDetails")]
        public HttpResponseMessage GetDetails([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.Get(ID);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        [HttpPost]
        [Route("api/Courier/Create")]
        public HttpResponseMessage Post([FromBody] CourierEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.Add(objEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, val);
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        [HttpPost]
        [Route("api/Courier/Update")]
        public HttpResponseMessage Update([FromBody] CourierEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    if (!string.IsNullOrEmpty(objRepositary.Update(objEntity)))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Document is updated successfully!!");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
                    }
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        [HttpPost]
        [Route("api/Courier/Delete")]
        public HttpResponseMessage Delete([FromBody] CourierEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.Delete(Convert.ToInt32(objEntity.ID)))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "Succsessfuly Deleted!!!");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed!!!");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
            }
            return response;
        }
    }
}