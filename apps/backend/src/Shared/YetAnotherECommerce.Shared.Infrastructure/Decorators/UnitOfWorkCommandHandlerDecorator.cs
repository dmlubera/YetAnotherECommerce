using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Shared.Infrastructure.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand, TDbContext>(
    ICommandHandler<TCommand> decorated,
    TDbContext dbContext) : ICommandHandler<TCommand>
    where TCommand : ICommand 
    where TDbContext : DbContext
{
    public async Task HandleAsync(TCommand command)
    {
        if (dbContext.Database.CurrentTransaction is not null)
        {
            await decorated.HandleAsync(command);
            return;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await decorated.HandleAsync(command);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand, TResult, TDbContext>(
    ICommandHandler<TCommand, TResult> decorated,
    TDbContext dbContext) : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TDbContext : DbContext
{

    public async Task<TResult> HandleAsync(TCommand command)
    {
        if (dbContext.Database.CurrentTransaction is not null)
        {
            return await decorated.HandleAsync(command);
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await decorated.HandleAsync(command);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}