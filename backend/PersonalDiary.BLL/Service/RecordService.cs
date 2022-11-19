using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.BLL.Exeptions;
using PersonalDiary.BLL.Helpers;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;
using System.Diagnostics;
using System.Text;

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

            if (record.ImageBase64 != null && Encoding.UTF8.GetByteCount(record.ImageBase64) > 3000000)
            {
                record.ImageBase64 = StringCompression.Compress(record.ImageBase64);
                record.IsCompressed = true;
            }

            await _recordRepository.AddAsync(record);
            await _recordRepository.SaveChangesAsync();

            if (record.IsCompressed == true && record.ImageBase64 != null)
            {
                record.ImageBase64 = StringCompression.Decompress(record.ImageBase64);
            }

            return _mapper.Map<RecordInfoDTO>(record);
        }

        public async Task<List<RecordInfoDTO>> GetFiveRecords(Guid authorId, int pageNumber)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var records = _recordRepository
                .Query()
                .Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * 5)
                .Take(5);

            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds / 1000.0);

            foreach (var record in records)
            {
                if (record.IsCompressed == true && record.ImageBase64 != null)
                {
                    record.ImageBase64 = StringCompression.Decompress(record.ImageBase64);
                }
            }

            return _mapper.Map<List<RecordInfoDTO>>(records);
        }

        public async Task<RecordInfoDTO> GetRecordById(Guid recordId)
        {
            var record = await _recordRepository.GetByKeyAsync(recordId);

            if (record.IsCompressed == true && record.ImageBase64 != null)
            {
                record.ImageBase64 = StringCompression.Decompress(record.ImageBase64);
            }

            return _mapper.Map<RecordInfoDTO>(record);
        }

        public async Task DeleteRecord(Guid recordId)
        {
            var record = await _recordRepository.GetByKeyAsync(recordId);

            if(record.CreatedAt < DateTime.Now.AddDays(-2))
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "A user cannot delete a record after two days of creation");
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
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "Record was not found");
            }

            _mapper.Map(recordDTO, record);
            record.UpdatedAt = DateTime.UtcNow;

            await _recordRepository.UpdateAsync(record);
            await _recordRepository.SaveChangesAsync();
        }
    }
}
