using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class DepartmentsController : ApiController
    {
        Department_Repository _repository = new Department_Repository();
        public IHttpActionResult Post(Department department)
        {
            if((department.Name != null) && (department.Name != ""))
            {
                _repository.Create(department);
                return Ok($"Data {department.Name} Berhasil Dimasukkan ke Database Department");
            }
            return BadRequest("Department Name can't be NULL");
        }

        public IHttpActionResult Get()
        {
            var getData = _repository.Get();
            return Ok(getData);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var delete = _repository.Delete(id);
            //if(delete != 0)
            //{
                return Ok($"Data {id} di Database Department berhasil dihapus {delete}");
            //}
            //return BadRequest("Department Name can't be NULL");
        }

        [HttpPut]
        public IHttpActionResult Update(int id, Department department)
        {
            if((department.Name != null) && (department.Name != ""))
            {
                var data = _repository.Update(id, department);
                return Ok($"Data {department.Name} berhasil diupdate");
            }
            return BadRequest("Department Name can't be NULL");
        }
        [ResponseType(typeof(Department))]
        public Task<IEnumerable<Department>> Get(int? id)
        {

                int idRequest = Convert.ToInt32(id);
                //var getDataById = _repository.Get(id);
                return _repository.Get(idRequest);

        }
    }
}
