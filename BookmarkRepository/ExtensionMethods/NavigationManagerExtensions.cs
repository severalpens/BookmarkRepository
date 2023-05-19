using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace BookmarkRepository
{
    public static class NavigationManagerExtensions
    {
        //public static Task<ClaimsPrincipal> GetUserV1Async(this NavigationManager navigationManager)
        //{
        //    //navigationManager.
        //    IHostEnvironmentNavigationManager? user = (navigationManager as IHostEnvironmentNavigationManager);
        //    //var user = (navigationManager as IHostEnvironmentNavigationManager)?.User;
        //    return Task.FromResult(user);
        //}

        //public static Task<ClaimsPrincipal> GetUserAsync(this NavigationManager navigationManager)
        //{
        //    var httpContextAccessor = (HttpContextAccessor)navigationManager
        //        .GetType()
        //        .GetProperty("HttpContextAccessor")
        //        .GetValue(navigationManager, null);

        //    var user = httpContextAccessor.HttpContext?.User;
        //    return Task.FromResult(user);
        //}
    }
}
