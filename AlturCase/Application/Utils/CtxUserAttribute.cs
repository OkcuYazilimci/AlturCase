using AlturCase.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AlturCase.Core.Interfaces;

namespace AlturCase.Application.Utils
{
    public class CtxUserAttribute : TypeFilterAttribute
    {
        public CtxUserAttribute(Type entityType) : base(typeof(CtxUserFilter))
        {
            Arguments = new object[] { entityType };
        }
    }

    public class CtxUserFilter : IAsyncActionFilter
    {
        private readonly Type _entityType;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public CtxUserFilter(Type entityType, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _entityType = entityType;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Items["UserId"] = userId;

            if (!context.ActionArguments.ContainsKey("id"))
            {
                await next();
                return;
            }

            var entityId = (Guid)context.ActionArguments["id"];
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();

            var entity = await dbContext.FindAsync(_entityType, entityId);

            if (entity == null || !(entity is IOwnedEntity ownedEntity) || ownedEntity.UserId != userId)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
