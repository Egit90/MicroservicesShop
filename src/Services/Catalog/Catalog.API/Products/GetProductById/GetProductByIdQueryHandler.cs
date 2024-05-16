using System.Net.Mail;
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using LanguageExt.Common;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.GetProductById;

public sealed class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductByIdQueryHandler> _logger = logger;

    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Looking for the record with id number {@q}", request.Id);
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            return new Result<Product>(new ProductNotFoundException());
        }

        return product;
    }
}
