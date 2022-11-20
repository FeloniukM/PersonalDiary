using AutoMapper;
using PersonalDiary.Common.DTO.Record;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.MappingProfiles
{
    public class RecordProfile : Profile
    {
        public RecordProfile()
        {
            CreateMap<RecordCreateDTO, Record>()
                .ForMember(x => x.Image, config => config.Ignore());
            CreateMap<Record, RecordInfoDTO>();
            CreateMap<RecordUpdateDTO, Record>();
        }
    }
}
