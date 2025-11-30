using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students (عرض قائمة الطلاب مع دعم الترتيب والبحث)
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // حفظ حالة الترتيب الحالية وقيمة البحث لتمريرها إلى الـ View
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                           select s;

            // 1. تطبيق البحث
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            // 2. تطبيق الترتيب
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            return View(await students.AsNoTracking().ToListAsync());
        }

        // GET: Students/Details/5 (عرض تفاصيل طالب معين)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // التحميل الصريح (Eager Loading) لـ Enrollments و Courses
            var student = await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // دالة عرض نموذج إنشاء طالب جديد (سنضيف المنطق لاحقاً)
        public IActionResult Create() { return View(); }
        // دالة عرض نموذج تعديل بيانات طالب (سنضيف المنطق لاحقاً)
        public IActionResult Edit(int id) { return View(); }
        // دالة عرض نموذج حذف طالب (سنضيف المنطق لاحقاً)
        public IActionResult Delete(int id) { return View(); }

        // يجب إضافة بقية دوال CRUD هنا عند كتابة الـ Views المقابلة.
    }
}