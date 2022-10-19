using System.Collections.Generic;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using GDMS.Entities;
using GDMS.Repository.Interfaces;
using System.Linq;

namespace GDMS.Repository
{
    public class PDCUploadRepository : BaseRepository,IUploadRepository<PDCUploadEntity>
    {
		public string Add(PDCUploadEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				
				parameters.Add("@AGREEMENTID", entity.AGREEMENTID);
				parameters.Add("@APPROVALDATE", entity.APPROVALDATE);
				parameters.Add("@SecurityPDCCHQCount", entity.SecurityPDCCHQCount);
				SqlMapper.Execute(ConnectionString, "sp_PDCUpload", param: parameters, commandType: CommandType.StoredProcedure);
				return "";
				//return parameters.Get<string>("@oId");


			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
