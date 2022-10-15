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
    public class CourierRepository : BaseRepository, ICourierRepository<CourierEntity>
    {
		public IEnumerable<CourierEntity> Get()
		{
			try
			{
				IList<CourierEntity> resultList = SqlMapper.Query<CourierEntity>(ConnectionString, "usp_GetCourierDetails", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public CourierEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", id);
				return SqlMapper.Query<CourierEntity>(ConnectionString, "usp_GetCourier", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public string Add(CourierEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@courier", entity.Courier);
				parameters.Add("@oId", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "sp_createCourier", param: parameters, commandType: CommandType.StoredProcedure);

				return parameters.Get<string>("@oId");


			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public string Update(CourierEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Courier", entity.Courier);
				parameters.Add("@id", entity.ID);
				parameters.Add("@oId", "", direction: ParameterDirection.Output);

				SqlMapper.Execute(ConnectionString, "sp_updateCourier", param: parameters, commandType: CommandType.StoredProcedure);

				return parameters.Get<string>("@oId");

			}
			catch (Exception)
			{
				throw;
			}
		}
		public bool Delete(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Id", id);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "usp_DeleteCourier", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				if (!string.IsNullOrEmpty(val))
				{
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}


	}
}
