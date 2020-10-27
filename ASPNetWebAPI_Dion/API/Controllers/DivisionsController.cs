using API.Repositories;
using API.ViewModel;
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
    public class DivisionsController : ApiController
    {
        Division_Repository _repository = new Division_Repository();
        
        [HttpPost]
        public IHttpActionResult Post(DivisionVM divisionVM)
        {
            if((divisionVM.Name != null) && (divisionVM.Name != ""))
            {
                _repository.Create(divisionVM);
                return Ok($"Data {divisionVM.Name} Berhasil Dimasukkan ke Database Department");
            }
            return BadRequest("Department Name can't be NULL");
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var getData = _repository.Get();
            return Ok(getData);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var delete = _repository.Delete(id);
            //if (delete != 0)
            //{
                return Ok($"Data {id} di Database Department berhasil dihapus {delete}");
            //}
            //return BadRequest("Department Name can't be NULL");
        }
        [HttpPut]
        public IHttpActionResult Update(int id, DivisionVM divisionVM)
        {
            if ((divisionVM.Name != null) && (divisionVM.Name != ""))
            {
                var data = _repository.Update(id, divisionVM);
                return Ok($"Data {divisionVM.Name} berhasil diupdate");
            }
            return BadRequest("Department Name can't be NULL");
        }
        [ResponseType(typeof(DivisionVM))]
        public Task<IEnumerable<DivisionVM>> Get(int id)
        {
            int idRequest = Convert.ToInt32(id);
            //var getDataById = _repository.Get(id);
            return _repository.Get(idRequest);

        }
    }
}
