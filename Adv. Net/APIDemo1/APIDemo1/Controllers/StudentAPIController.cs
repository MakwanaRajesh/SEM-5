using APIDemo1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return student;

        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student stu)
        {
            await context.Students.AddAsync(stu);
            await context.SaveChangesAsync();
            return Ok(stu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student stu)
        {
            if (id != stu.Id) 
            {
                return BadRequest();
            }
            context.Entry(stu).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(stu);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var stu = await context.Students.FindAsync(id);
            if(stu == null)
            {
                return NotFound();
            }
            context.Students.Remove(stu);
            await context.SaveChangesAsync();
            return Ok(id);
        }

    }
}
