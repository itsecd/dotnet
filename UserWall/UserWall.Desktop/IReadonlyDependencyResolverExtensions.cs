using System;

using Splat;

namespace UserWall.Desktop;

public static class IReadonlyDependencyResolverExtensions
{
    public static T GetServiceOrThrow<T>(this IReadonlyDependencyResolver resolver, string? contract = null)
    {
        var service = resolver.GetService<T>();

        if (service is null)
            throw new InvalidOperationException();

        return service;
    }
}
