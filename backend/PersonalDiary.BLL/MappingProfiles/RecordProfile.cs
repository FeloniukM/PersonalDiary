using AutoMapper;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.MappingProfiles
{
    public class RecordProfile : Profile
    {
        public RecordProfile()
        {
            CreateMap<RecordCreateDTO, Record>();
            CreateMap<Record, RecordInfoDTO>();
            CreateMap<RecordUpdateDTO, Record>();
        }
    }
}
