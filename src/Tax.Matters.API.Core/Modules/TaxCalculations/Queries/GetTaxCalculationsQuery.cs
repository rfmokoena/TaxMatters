﻿using MediatR;
using Tax.Matters.API.Core.Modules.TaxCalculations.Models;
using Tax.Matters.API.Core.Wrappers;
using Tax.Matters.Client;
using Tax.Matters.Domain.Entities;

namespace Tax.Matters.API.Core.Modules.TaxCalculations.Queries;

/// <summary>
/// Initializes a new instance of the <see cref="GetTaxCalculationsQuery"/> query class
/// </summary>
/// <param name="model"></param>
public class GetTaxCalculationsQuery(TaxCalculationsFilteringModel model) : IRequest<IResponse<PageList<TaxCalculation>>>
{
    public TaxCalculationsFilteringModel Model { get; } = model;
}
