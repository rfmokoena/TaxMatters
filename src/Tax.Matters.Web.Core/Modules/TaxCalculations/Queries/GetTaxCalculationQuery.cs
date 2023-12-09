﻿using MediatR;
using Tax.Matters.Client;
using Tax.Matters.Domain.Entities;

namespace Tax.Matters.Web.Core.Modules.TaxCalculations.Queries;

public class GetTaxCalculationQuery(string id) : IRequest<IResponse<TaxCalculation>>
{
    public string Id { get; } = id;
}