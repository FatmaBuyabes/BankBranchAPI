using BankBranchAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BankBranchAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<BankBranchResponse> GetAll(int page = 1, string searchTerm="")
        {
            var bank = _context.BankBranches.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bank = bank.Where(c => c.location.Contains(searchTerm));
            }

            return bank
                .Skip((page -1)*3)
                .Take(3)
                .Select(b => new BankBranchResponse
            {

                branchManager = b.branchManager,
                location = b.location,
                name = b.name
            }).ToList();

        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankBranchResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<BankBranchResponse> Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if(bank == null)
            {
                return NotFound();
            }
            return new BankBranchResponse
            {
                branchManager = bank.branchManager,
                location = bank.location,
                name = bank.name
            };
        }


        [HttpPatch("{id}")]
        public IActionResult Edit(int id,AddBankRequest req)
        {
            var bank = _context.BankBranches.Find(id);
            bank.name = req.name;
            bank.branchManager = req.branchManager;
            bank.location = req.location;
            _context.SaveChanges();

            return Created(nameof(Details), new { id = bank.id });
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Add(BankBranch branch)
        {
            var newBank = new BankBranch()
            {

                name = branch.name,
                location = branch.location,
                branchManager = branch.branchManager,
            };
            _context.BankBranches.Add(newBank);
            _context.SaveChanges();

            return Created(nameof(Details), new { id = newBank.id});

        }
    }
}
