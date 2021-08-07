using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Mappers
{
    public class ChatMapperBuilder : IMapperDsl
    {
        public static MapperConfiguration MapperConfiguration => new MapperConfiguration(c =>
        {
            // c.CreateMap<StaffStatus, EditableStaffStatus>();
            // c.CreateMap<Domain.Models.Status, Status>();
            // c.CreateMap<Dodo.ClientSite.LegacyFacade.Contracts.Components.EmployeeProfile.v1.Employee,
            //         Dodo.Staff.Vaccination.Contracts.Outbound.StaffStatus>()
            //     .ForMember(s => s.UnitId, opt => opt.MapFrom(e => Uuid.Parse(e.UnitUUId.ToString())))
            //     .ForMember(s => s.StaffId, opt => opt.MapFrom(e => Uuid.Parse(e.UUId.ToString())))
            //     .ForMember(s => s.Id, opt => opt.Ignore());
            // c.CreateMap<StaffStatus, Dodo.Staff.Vaccination.Contracts.Outbound.StaffStatus>();
        });

        public IMapper Please => MapperConfiguration.CreateMapper();
    }
}
