using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Queries;

namespace YetAnotherECommerce.Shared.Infrastructure.Queries;

internal class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await ((Task<TResult>)handlerType
            .GetMethod(nameof(IQueryHandler<,>.HandleAsync))
            ?.Invoke(handler, [query]))!;
    }
}