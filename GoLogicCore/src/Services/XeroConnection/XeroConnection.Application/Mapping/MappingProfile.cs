using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XeroConnection.Application.Features.XeroConnection.Queries.CheckXeroConnection;
using XeroConnection.Application.Features.XeroConnection.Queries.DeleteXeroConnection;
using XeroConnection.Application.Features.XeroConnection.Queries.XeroCallback;
using XeroConnection.Application.Queries.GetXeroConnection;
using XeroConnection.Core.Entities;

namespace XeroConnection.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<GetXeroConnectionViewModel, XeroConnectionProperties>().ReverseMap();
            CreateMap<XeroCallbackViewModel, XeroConnectionProperties>().ReverseMap();
            CreateMap<DeleteXeroConnectionViewModel, XeroConnectionProperties>().ReverseMap();
            CreateMap<CheckXeroConnectionViewModel, XeroConnectionProperties>().ReverseMap();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
