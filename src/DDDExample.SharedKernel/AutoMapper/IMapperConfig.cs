using AutoMapper;

namespace DDDExample.SharedKernel.AutoMapper
{
    public interface IMapperConfig
    {
        void Map(IMapperConfigurationExpression cfg);
    }
}
