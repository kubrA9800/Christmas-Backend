using ChristmasBackend.Areas.ViewModels.Category;
using ChristmasBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdAsync((int)id);

            if (dbCategory is null) return NotFound();

            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM request)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            CategoryVM existCategory = await _categoryService.GetByNameAsync(request.Name);

            if (existCategory != null)
            {
                ModelState.AddModelError("Name", "This category already exists");
                return View();
            }


            await _categoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            await _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdAsync((int)id);

            if (dbCategory is null) return NotFound();

            return View(new CategoryEditVM
            {
                Name = dbCategory.Name
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdAsync((int)id);

            if (dbCategory is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            CategoryVM existCategory = await _categoryService.GetByNameAsync(request.Name);


            if (existCategory != null)
            {
                if (existCategory.Id == request.Id)
                {
                    await _categoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This category already exists");
                return View();
            }


            await _categoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
