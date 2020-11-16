using AutoMapper;

namespace Hub.Storage.Repository.AutoMapper
{
    public static class MapperConfigurationExpressionExtensions
    {
        public static IMapperConfigurationExpression AddHubProfiles(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.AddProfile<SettingMapperProfile>();

            return mapperConfigurationExpression;
        }

        public static IMapperConfigurationExpression AddHostedServiceProfiles(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.AddHubProfiles();
            mapperConfigurationExpression.AddProfile<WorkerLogMapperProfile>();
            mapperConfigurationExpression.AddProfile<BackgroundTaskConfigurationProfile>();

            return mapperConfigurationExpression;
        }
    }
}