using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.BLL.Exeptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class RecordService : IRecordService
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IRepository<Image> _imageRepository;
        private readonly IRepository<Record> _recordRepository;

        public RecordService(IMapper mapper, 
            IRepository<Record> recordRepository, 
            IUploadService uploadService, 
            IRepository<Image> imageRepository)
        {
            _mapper = mapper;
            _recordRepository = recordRepository;
            _uploadService = uploadService;
            _imageRepository = imageRepository;
        }

        public async Task<RecordInfoDTO> CreateRecord(RecordCreateDTO recordCreateDTO, Guid authorId)
        {
            var record = _mapper.Map<Record>(recordCreateDTO);
            record.AuthorId = authorId;

            if(recordCreateDTO.Image != null)
            {
                var image = await _uploadService.UploadImage(recordCreateDTO.Image);

                record.Image = image;
                await _imageRepository.AddAsync(image);
            }

            await _recordRepository.AddAsync(record);
            await _recordRepository.SaveChangesAsync();

            return _mapper.Map<RecordInfoDTO>(record);
        }

        public async Task<List<RecordInfoDTO>> GetFiveRecords(Guid authorId, int pageNumber)
        {
            var records = await _recordRepository
                .Query()
                .Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * 5)
                .Take(5)
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

            await _recordRepository.UpdateAsync(record);
            await _recordRepository.SaveChangesAsync();
        }

        public async Task<List<RecordInfoDTO>> GetRecordsByDate(DateTime date, Guid authorId)
        {
            var records = await _recordRepository
                .Query()
                .Where(x => x.AuthorId == authorId && x.CreatedAt == date)
                .ToListAsync();

            return _mapper.Map<List<RecordInfoDTO>>(records);
        }
    }
}
