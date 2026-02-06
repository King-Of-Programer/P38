using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P38.Data;
using P38.DTO;
using P38.Models;

namespace P38.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        private readonly IMapper _maper;
        private readonly DBContext _db;

        public HomeController(DBContext db, IMapper mapper)
        {
            _maper = mapper;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<ActionResult<List<ProductReadDTO>>> GetAll()
        {
            var items = await _db.Products.ToListAsync();
            return Ok(_maper.Map<ProductReadDTO>(items));
        }




        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductReadDTO>> GetById(Guid id)
        {
            var item = await _db.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }


            return Ok(_maper.Map<ProductReadDTO>(item));
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ProductCreateDTO>> CreateProduct([FromBody] ProductCreateDTO product)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = _maper.Map<Product>(product);
            entity.Id = Guid.NewGuid();
            entity.Characteristics ??= new Characteristics();

            _db.Products.Add(entity);
            await _db.SaveChangesAsync();

            var result = _maper.Map<ProductReadDTO>(entity);
            return CreatedAtAction(nameof(GetById), new {id = entity.Id}, result);
           
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]ProductUpdateDTO product)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null) { return NotFound(); }

            entity.Characteristics ??= new Characteristics();

            _maper.Map(product, entity);

            await _db.SaveChangesAsync();
            return Ok();

        }
        // створити Delete
        //Створити модель Користувача(id, Date of birth, phone number, email, name, lastname)
        // реалізувати CRUD та Automaper(DTO)



    }
}
