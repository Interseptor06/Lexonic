using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StockDPM;

namespace LexonicWebApplication.Pages
{

    public class IndexModel : PageModel
    {

       
        
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string TargetStock { get; set; }
        
        public void OnGet()
        {
            
        }

        private char dollarSign = '$';
        
        public string ValueToPass;
        public IActionResult OnPost()
        {
            //Data processing
            TargetStock = TargetStock.TrimStart(dollarSign);
            TargetStock = TargetStock.ToUpper();
            if (LexonicWebApplication.StockList.SList.Contains(TargetStock))
            {
                return RedirectToPage("Privacy", new {ValueToPass = TargetStock});
            }
            else
            {
                return RedirectToPage("Error");
            }
        }
    }
}