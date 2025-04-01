using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CantosoPizza.pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);
            return RedirectToPage("PizzaList");
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id <= 0) // Verifica se o ID é válido.
            {
                return Page();
            }

            _service.DeletePizza(id);

            // Redireciona para a mesma página após a exclusão.
            return RedirectToPage("PizzaList"); 
        }

    }
}
