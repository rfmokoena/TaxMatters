using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tax.Matters.Domain.Entities;
using Tax.Matters.Web.Core.Modules.PostalCodes.Commands;
using Tax.Matters.Web.Core.Modules.PostalCodes.Models;
using Tax.Matters.Web.Core.Modules.PostalCodes.Queries;
using Tax.Matters.Web.Core.Modules.TaxManagement.Queries;

namespace Tax.Matters.Web.Pages.PostalCodes
{
    public class EditModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public PostalCodeEditModel PostalCode { get; set; } = default!;

        public SelectList TaxCalculationSelectList { get; set; } = default!;

        private readonly IMediator _mediator = mediator;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new GetPostalCodeQuery(id);

            var entity = await _mediator.Send(query);

            if (entity.IsError)
            {
                return NotFound();
            }

            PostalCode = new PostalCodeEditModel
            {
                Code = entity.Content!.Code,
                IncomeTaxId = entity.Content.IncomeTaxId,
                Version = entity.Content.Version,
            };

            await LoadTaxCalculationSelectList(PostalCode.IncomeTaxId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var emptyPostalCode = new PostalCodeEditModel();

            if (await TryUpdateModelAsync(
                 emptyPostalCode,
                 "postalcode",   // Prefix for form value.
                 s => s.Code, s => s.IncomeTaxId, s => s.Version))
            {
                var command = new EditPostalCodeCommand(id, emptyPostalCode);

                var result = await _mediator.Send(command);

                if (!result.IsError)
                {
                    return RedirectToPage("./Index");
                }

                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    // We can intercept the error to present user friendly message and possibly redact sensitive information
                    ModelState.AddModelError("", result.Error);
                }
                else
                {
                    ModelState.AddModelError("", "Unexpected response received while executing the request");
                }
            }

            await LoadTaxCalculationSelectList(emptyPostalCode.IncomeTaxId);
            return Page();
        }



        private async Task LoadTaxCalculationSelectList(string? selected = null)
        {
            var query = new ListTaxCalculationTypesQuery();

            var response = await _mediator.Send(query);

            if (!response.IsError)
            {
                TaxCalculationSelectList = new SelectList(response.Content, nameof(IncomeTax.Id), nameof(IncomeTax.TypeName), selected);
            }
            else
            {
                TaxCalculationSelectList = new SelectList(Enumerable.Empty<IncomeTax>(), nameof(IncomeTax.Id), nameof(IncomeTax.TypeName));
            }
        }
    }
}
