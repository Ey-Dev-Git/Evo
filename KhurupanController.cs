using Khurupan.Areas.Identity.Data;
using Khurupan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Khurupan.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KhurupanController : Controller
    {
        private readonly DBContext _db;
        public KhurupanController(DBContext db)
        {
            _db = db;
        }

        //***Bad smell Duplicated Code (โค้ดที่ซ้ำกัน)
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        //***Bad smell Duplicated Code (โค้ดที่ซ้ำกัน)
        // /Khurupan/AllKhurupan
        [HttpGet]
        [Route("AllKhurupan")]
        [Authorize(Roles = "Admin")]
        public IActionResult AllKhurupan()
        {
            IEnumerable<Khurupans> allKhurupan = _db.Khurupans;

            return Ok(allKhurupan);
        }

        // /Khurupan/KhurupanIndex
        [HttpGet]
        [Route("KhurupanIndex")]
        [Authorize(Roles = "Admin")]
        public IActionResult KhurupanIndex()
        {
            IEnumerable<Khurupans> allKhurupan = _db.Khurupans;
            var khurupans = new List<Khurupans>();
            foreach (var khurupan in _db.Khurupans)
            {
                khurupans.Add(khurupan);
            }

            return View(allKhurupan);
        }

        // /Khurupan/GetKhurupanByID/{id}
        [HttpGet]
        [Route("GetKhurupanByID/{id}")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult GetKhurupanByID(int id)
        {
            var khurupan = _db.Khurupans.Find(id);
            if (khurupan == null || id == 0)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                return Ok(khurupan);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Khurupan/CreateKhurupan
        [HttpPost]
        [Route("CreateKhurupan")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateKhurupan([FromForm] Khurupans model)
        {

            if (ModelState.IsValid)
            {
                // ตรวจสอบว่า EquipmentNumber ที่ต้องการบันทึกนี้มีอยู่แล้วหรือไม่
                if (_db.Khurupans.Any(en => en.EquipmentNumber == model.EquipmentNumber))
                {
                    //ModelState.AddModelError("EquipmentNumber", "หมายเลขสินทรัพย์นี้มีอยู่แล้ว");
                    //return BadRequest(ModelState);

                    TempData["AlertMessagefail"] = "fail";
                    return View("ShowCreateKhurupan");

                }

                _db.Khurupans.Add(model);
                _db.SaveChanges();

                TempData["AlertMessageSuccess"] = "success";
                return View("ShowCreateKhurupan");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Khurupan/ShowCreateKhurupan
        [HttpGet]
        [Route("ShowCreateKhurupan")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowCreateKhurupan()
        {
            return View();
        }

        // /Khurupan/UpdateKhurupan/{id}
        [HttpPost]
        [Route("UpdateKhurupan/{id}")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateKhurupan([FromForm] Khurupans model)
        {
            if (ModelState.IsValid)
            {
                _db.Khurupans.Update(model);
                _db.SaveChanges();
                TempData["AlertMessageSuccess"] = "success";
                return RedirectToAction("ShowUpdateKhurupan", new { id = model.EquipmentNumber });
            }
            return View(model);
        }

        // /Khurupan/ShowUpdateKhurupan/{id}
        [HttpGet]
        [Route("ShowUpdateKhurupan/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowUpdateKhurupan(string? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var obj = _db.Khurupans.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // /Khurupan/DeleteKhurupan/{id}
        [HttpDelete]
        [Route("DeleteKhurupan/{id}")]
        [IgnoreAntiforgeryToken]
        public ActionResult DeleteKhurupan(string id)
        {
            var khurupanToDelete = _db.Khurupans.Find(id);

            if (khurupanToDelete == null)
            {
                return BadRequest(ModelState);
            }

            _db.Khurupans.Remove(khurupanToDelete);
            _db.SaveChanges();

            return Ok("ลบแล้ว");
            //return RedirectToAction("KhurupanIndex"); // หรือให้กลับไปยังหน้าอื่น ๆ ที่คุณต้องการ
        }


    }
}
