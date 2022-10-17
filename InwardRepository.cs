using System;
using System.Collections.Generic;
using System.Text;

namespace GDMS.Repository.Interfaces
{
	public interface InwardRepository<T> where T : class
	{
        IEnumerable<T> Get();
        T Get(int id);

        IEnumerable<T> GetPendingData(int UserID);

        IEnumerable<T> GetInwardData(int UserID);
        IEnumerable<T> GetTerritorydata(string podnO);
        IEnumerable<T> GetLANDetails(string podnO, string GetLANDetails);

        IEnumerable<T> UpdateMissingFiles(string BatchNo, string LanNo);
        IEnumerable<T> UpdateLanDetails(string BatchNo, string LanNo, int UserID);
        IEnumerable<T> GetLAN(string LANNo);
        IEnumerable<T> GetBatchDetails(string BatchNo, int USERId);
        IEnumerable<T> GetBatchClose(string BatchNo);
        IEnumerable<T> SearchRecords(int CompanyID, string FileNo, string SearchBy);
        IEnumerable<T> SearchRecordsByFulletron(string FileNo, string SearchBy);
        IEnumerable<T> GetPODDetailsEntry(string UserID);
        IEnumerable<T> GetPODDetailsFulletron();
        IEnumerable<T> GetPODDetailsFulletronSearch();
        IEnumerable<T> GetPODDetailsFulletronACK();
        IEnumerable<T> GetPODByLAN(string podnO);
        IEnumerable<T> GetBatchDetails(string BatchNo);
        string Add(T entity, String[] strReffields);
        string PODdetailsEntry(T entity);
        string PODEntryFulletron(T entity);
        string PODAcknowledge(T entity);
        string InwardEntry(T entity);
        string AcknowledgePOD(T entity);
        string TerritoryEntry(T entity, String[] strReffields);

        string UpdateFileStatus(T entity, String[] strReffields);
        string UpdateSAPCode(string FileNo, string SACKCODE, string Remark, string Accept);
        string UpdateReceivedPOD(T entity);
        bool Delete(string Territorycode);
        bool DeleteBarcode(string Territorycode);
        bool Delete(int id, int userid);
        string Update(T entity);
        IEnumerable<T> GetDetails(int TempID, string FileNo);
        IEnumerable<T> GetStatusReport(T entity);
        IEnumerable<T> GetPODReport(T entity);
        IEnumerable<T> GetBatchDetailsReport(T entity);         
        IEnumerable<T> GetInwardREport(T entity);         
        IEnumerable<T> GetFileStatusReport(T entity);         
        IEnumerable<T> GetNextFile(int TempID, string FileNo);
        IEnumerable<T> GetFileNo(string FileNo, string CompanyID);
        IEnumerable<T> GetBatchdetailsByBatchNo(string BatchNo);

        IEnumerable<T> GetBarcodeData(string CaseNo, string status);
        IEnumerable<T> GetFileUplaodDetails(string FileNo);
    }
}
