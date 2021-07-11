using AutoMapper;
using Deviot.Hermes.ModbusTcp.Api.Mappings;

namespace Deviot.Hermes.ModbusTcp.TDD.Bases
{
    public class MappingBaseTest
    {
        protected readonly IMapper _mapper;

        public MappingBaseTest()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.AddProfile(new EntityToViewModelMapping());
                options.AddProfile(new ViewModelToEntityMapping());
            });
            _mapper = mappingConfig.CreateMapper();
        }
    }
}