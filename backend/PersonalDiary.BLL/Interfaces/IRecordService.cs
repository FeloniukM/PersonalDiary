using Microsoft.AspNetCore.Http;
using PersonalDiary.Common.DTO.Record;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IRecordService
    {
        Task<RecordInfoDTO> CreateRecord(RecordCreateDTO recordCreateDTO, Guid authorId);
        Task<List<RecordInfoDTO>> GetFiveRecords(Guid authorId, int pageNumber);
        Task<RecordInfoDTO> GetRecordById(Guid recordId);
        Task DeleteRecord(Guid recordId);
        Task UpdateRecord(RecordUpdateDTO recordDTO, Guid authorId);
        Task<List<RecordInfoDTO>> GetRecordsByDate(DateTime with, DateTime unto, Guid authorId);
        Task<List<RecordInfoDTO>> GetRecordByContent(string content, Guid authorId);
    }
}
