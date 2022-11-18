using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class RecordService : IRecordService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Record> _recordRepository;

        public RecordService(IMapper mapper, IRepository<Record> recordRepository)
        {
            _mapper = mapper;
            _recordRepository = recordRepository;
        }

        public async Task<RecordInfoDTO> CreateRecord(RecordCreateDTO recordCreateDTO, Guid authorId)
        {
            var record = _mapper.Map<Record>(recordCreateDTO);

            record.Id = Guid.NewGuid();
            record.AuthorId = authorId;
            record.CreatedAt = DateTime.UtcNow;
            record.UpdatedAt = DateTime.UtcNow;

            await _recordRepository.AddAsync(record);
            await _recordRepository.SaveChangesAsync();

            return _mapper.Map<RecordInfoDTO>(record);
        }

        public async Task<List<RecordInfoDTO>> GetFiveRecords(Guid authorId)
        {
            var records = await _recordRepository
                .Query()
                .Where(x => x.AuthorId == authorId)
                .ToListAsync();

            return _mapper.Map<List<RecordInfoDTO>>(records);
        }

        public async Task<RecordInfoDTO> GetRecordById(Guid recordId)
        {
            var record = await _recordRepository.GetByKeyAsync(recordId);

            return _mapper.Map<RecordInfoDTO>(record);
        }

        public async Task DeleteRecord(Guid recordId)
        {
            var record = await _recordRepository.GetByKeyAsync(recordId);

            if(record.CreatedAt < DateTime.Now.AddDays(-2))
            {
                throw new Exception();
            }

            await _recordRepository.DeleteAsync(record);
            await _recordRepository.SaveChangesAsync();
        }

        public async Task UpdateRecord(RecordUpdateDTO recordDTO, Guid authorId)
        {
            var record = await _recordRepository
                .Query()
                .Where(x => x.Id == recordDTO.Id && x.AuthorId == authorId)
                .FirstOrDefaultAsync();

            if (record == null)
                throw new Exception();

            _mapper.Map(recordDTO, record);
            record.UpdatedAt = DateTime.UtcNow;

            await _recordRepository.UpdateAsync(record);
            await _recordRepository.SaveChangesAsync();
        }
    }
}
