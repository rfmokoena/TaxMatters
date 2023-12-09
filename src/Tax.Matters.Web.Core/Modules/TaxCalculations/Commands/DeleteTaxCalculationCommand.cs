﻿using MediatR;
using Tax.Matters.Client;
using Tax.Matters.Domain.Entities;

namespace Tax.Matters.Web.Core.Modules.TaxCalculations.Commands;

public class DeleteTaxCalculationCommand(string id) : IRequest<IResponse<TaxCalculation>>
{
    public string Id { get; } = id;
}
