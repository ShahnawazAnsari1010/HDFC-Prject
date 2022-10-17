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
    public class InwardUploadRepository:BaseRepository,IUploadRepository<InwardUploadEntity>
    {
		public string Add(InwardUploadEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				
				parameters.Add("@AGEEMENTNO", entity.AGEEMENTNO);
				parameters.Add("@BARCODE", entity.BARCODE);
				parameters.Add("@PRODUCTNAME", entity.PRODUCTNAME);
				parameters.Add("@DISBURSEMENTDATE", entity.DISBURSEMENTDATE);
				parameters.Add("@CUSTOMERNAME", entity.CUSTOMERNAME);
				parameters.Add("@SYSTEM", entity.SYSTEM);
				parameters.Add("@CASESTARTERBRANCH", entity.CASESTARTERBRANCH);
				parameters.Add("@GENERATEDON", entity.GENERATEDON);
				parameters.Add("@CPUID", entity.CPUID);
				parameters.Add("@batch_id", entity.batch_id);
				parameters.Add("@RepaymentMode", entity.RepaymentMode);
				parameters.Add("@VendorLocation", entity.VendorLocation);
				parameters.Add("@VendorID", entity.VendorID);
				parameters.Add("@oId", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "sp_InwardUpload", param: parameters, commandType: CommandType.StoredProcedure);
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
