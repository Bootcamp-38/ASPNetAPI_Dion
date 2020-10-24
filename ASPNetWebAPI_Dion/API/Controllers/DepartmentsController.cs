using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class DepartmentsController : ApiController
    {
        Department_Repository _repository = new Department_Repository();
        public IHttpActionResult Post(Department department)
        {
            _repository.Create(department);
            return Ok($"Data {department.Name} Berhasil Dimasukkan ke Database Department");
        }

        public IHttpActionResult Get()
        {
            var getData = _repository.Get();
            return Ok(getData);
        }
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok($"Data {id} di Database Department berhasil dihapus");
        }
        public IHttpActionResult Update(int id, Department department)
        {
            var data = _repository.Update(id, department);
            return Ok("Data Department berhasil diupdate" + data);
        }
    }
}
