using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Adaro.Centralize.Queries.Container;
using System;

namespace Adaro.Centralize.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}