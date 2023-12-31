﻿using MediatR;
using Tax.Matters.Client;
using Tax.Matters.Domain.Entities;
using Tax.Matters.Web.Core.Models.Dto;

namespace Tax.Matters.Web.Core.Modules.PostalCodes.Queries;

/// <summary>
/// Inirializes a new instance of the <see cref="GetPostalCodesQuery"/> query class
/// </summary>
/// <param name="keyword"></param>
/// <param name="pageNumber"></param>
/// <param name="limit"></param>
public class GetPostalCodesQuery(
    string? keyword,
    int pageNumber = 1,
    int limit = 10) : IRequest<IResponse<PageListDto<PostalCode>>>
{
    public int PageNumber { get; } = pageNumber;
    public int Limit { get; } = limit;
    public string? Keyword { get; } = keyword;
}