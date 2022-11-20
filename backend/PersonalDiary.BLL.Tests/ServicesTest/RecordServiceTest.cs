using AutoMapper;
using FakeItEasy;
using MockQueryable.FakeItEasy;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.BLL.Service;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;
using Image = PersonalDiary.DAL.Entities.Image;
using Record = PersonalDiary.DAL.Entities.Record;

namespace PersonalDiary.BLL.Tests.ServicesTest
{
    public class RecordServiceTest
    {
        private readonly RecordService _recordService;
        private readonly IRepository<Record> _recordRepository;
        private readonly IUploadService _uploadService;
        private readonly IRepository<Image> _imageRepository;
        private readonly IMapper _mapper;

        public RecordServiceTest()
        {
            _mapper = A.Fake<IMapper>();
            _uploadService = A.Fake<IUploadService>();
            _imageRepository = A.Fake<IRepository<Image>>();
            _recordRepository = A.Fake<IRepository<Record>>();

            _recordService = new RecordService(_mapper, _recordRepository, _uploadService, _imageRepository);
        }

        [Fact]
        public async Task CreateRecord_WhenSuccessful_ThenGetRecordInfoDTO()
        {
            var recordCreateDTO = new RecordCreateDTO()
            {
                Title = "SomeTitle",
                Text = "SomeText",
            };
            var record = new Record()
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                Text = "SomeText",
                Title = "SomeTitle",
                CreatedAt = DateTime.Now,
                Author = new User()
            };
            var recordInfoDTO = new RecordInfoDTO()
            {
                Id = record.Id,
                CreatedAt = record.CreatedAt,
                Text = "SomeText",
                Title = "SomeTitle",
            };

            A.CallTo(() => _mapper.Map<Record>(recordCreateDTO)).Returns(record);
            A.CallTo(() => _recordRepository.AddAsync(record)).Returns(record);
            A.CallTo(() => _recordRepository.SaveChangesAsync()).Returns(Task.FromResult(1));
            A.CallTo(() => _mapper.Map<RecordInfoDTO>(record)).Returns(recordInfoDTO);

            var result = await _recordService.CreateRecord(recordCreateDTO, record.AuthorId);

            Assert.NotNull(result);
            Assert.Equal(recordInfoDTO, result);
        }

        [Fact]
        public async Task GetFiveRecord_WhenRecordExist_ThenGetRecordList()
        {
            var authorId = Guid.NewGuid();
            var records = new List<Record>()
            {
                new Record()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    Text = "SomeText",
                    Title = "SomeTitle",
                    CreatedAt = DateTime.Now,
                    Author = new User()
                },
                new Record()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    Text = "SomeText2",
                    Title = "SomeTitle2",
                    CreatedAt = DateTime.Now,
                    Author = new User()
                }
            };
            var queryable = records.BuildMock();
            var recordsInfo = new List<RecordInfoDTO>()
            {
                new RecordInfoDTO()
                {
                    Id = records[0].Id,
                    CreatedAt = records[0].CreatedAt,
                    Text = "SomeText",
                    Title = "SomeTitle"

                },
                new RecordInfoDTO()
                {
                    Id = records[1].Id,
                    CreatedAt = records[1].CreatedAt,
                    Text = "SomeText2",
                    Title = "SomeTitle2"
                }
            };

            A.CallTo(() => _recordRepository.Query()).Returns(queryable);
            A.CallTo(() => _mapper.Map<List<RecordInfoDTO>>(A<List<Record>>.That.IsInstanceOf(typeof(List<Record>)))).Returns(recordsInfo);

            var result = await _recordService.GetFiveRecords(authorId, 1);

            Assert.NotNull(result);
            Assert.Equal(recordsInfo, result);
        }

        [Fact]
        public async Task GetRecordById_WhenRecordExist_ThenGetRecord()
        {
            var record = new Record()
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                Text = "SomeText",
                Title = "SomeTitle",
                CreatedAt = DateTime.Now,
                Author = new User()
            };
            var recordInfoDTO = new RecordInfoDTO()
            {
                Id = record.Id,
                Text = "SomeText",
                Title = "SomeTitle",
                CreatedAt = DateTime.Now
            };

            A.CallTo(() => _recordRepository.GetByKeyAsync(record.Id)).Returns(record);
            A.CallTo(() => _mapper.Map<RecordInfoDTO>(record)).Returns(recordInfoDTO);

            var result = await _recordService.GetRecordById(record.Id);

            Assert.NotNull(result);
            Assert.Equal(recordInfoDTO, result);
        }

        [Fact]
        public void DeleteRecord_WhenRecordExist_ThenRemoveThisRecord()
        {
            var record = new Record()
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                Text = "SomeText",
                Title = "SomeTitle",
                CreatedAt = DateTime.Now,
                Author = new User()
            };

            A.CallTo(() => _recordRepository.DeleteAsync(record)).Returns(Task.CompletedTask);
            A.CallTo(() => _recordRepository.SaveChangesAsync()).Returns(Task.FromResult(1));

            var result = _recordService.DeleteRecord(record.Id).IsCompleted;

            Assert.True(result);
        }

        [Fact]
        public void UpdateRecord_WhenRecordExost_ThenUpdateData()
        {
            var authorId = Guid.NewGuid();
            var records = new List<Record>()
            {
                new Record()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    Text = "SomeText",
                    Title = "SomeTitle",
                    CreatedAt = DateTime.Now,
                    Author = new User()
                }
            };
            var queryable = records.BuildMock();
            var recordUpdateDTO = new RecordUpdateDTO()
            {
                Id = records[0].Id,
                Text = "newText",
                Title = "NewTitle"
            };

            A.CallTo(() => _recordRepository.Query()).Returns(queryable);
            A.CallTo(() => _mapper.Map<Record>(recordUpdateDTO)).Returns(records[0]);
            A.CallTo(() => _recordRepository.UpdateAsync(records[0])).Returns(Task.CompletedTask);
            A.CallTo(() => _recordRepository.SaveChangesAsync()).Returns(Task.FromResult(1));

            var result = _recordService.UpdateRecord(recordUpdateDTO, authorId).IsCompleted;

            Assert.True(result);
        }
    }
}
