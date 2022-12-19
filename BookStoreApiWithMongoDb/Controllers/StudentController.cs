using BookStoreApiWithMongoDb.Models;
using BookStoreApiWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApiWithMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _studentService.GetAsync();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            Student student = await _studentService.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound($"Student with Id: {id} not found");
            }

            return Ok(student);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            var _ = await _studentService.CreateAsync(student);

            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest();
            }

            Student student = await _studentService.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound($"Student with Id: {id} not found");
            }

            await _studentService.UpdateAsync(id, updatedStudent);

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            Student student = await _studentService.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound($"Student with Id: {id} not found");
            }

            await _studentService.RemoveAsync(id);

            return Ok($"Student with Id:{id} deleted successfuly.");
        }
    }
}
