using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper
{
    public static class MappingExtensions
    {
        public static TDestination MapMultiple<TDestination>(this IMapper mapper, params object[] sources) 
        {
            if (sources == null || sources.Length == 0)
            {
                return default;
            }

            var destination = Activator.CreateInstance<TDestination>();

            foreach (var source in sources)
            {
                destination = mapper.Map(source, destination);
            }

            return destination;
        }
    }
}
