using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.Core.Base;
using Adaro.Centralize.Core.Extensions;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Types;

namespace Adaro.Centralize.Queries
{
    public class RoleQuery : CentralizeQueryBase<ListGraphType<RoleType>, List<RoleDto>>
    {
        private readonly RoleManager _roleManager;

        public static class Args
        {
            public const string Id = "id";
            public const string Name = "name";
        }

        public RoleQuery(RoleManager roleManager)
            : base("roles", new Dictionary<string, Type>
                {
                    {Args.Id, typeof(IdGraphType)},
                    {Args.Name, typeof(StringGraphType)}
                }
            )
        {
            _roleManager = roleManager;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles)]
        public override async Task<List<RoleDto>> Resolve(IResolveFieldContext context)
        {
            var query = _roleManager.Roles.AsNoTracking();

            context
                .ContainsArgument<int>(Args.Id, id => query = query.Where(r => r.Id == id))
                .ContainsArgument<string>(Args.Name, name => query = query.Where(r => r.Name == name));

            return await ProjectToListAsync<RoleDto>(query);
        }
    }
}
