using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface Division_Interface
    {
        IEnumerable<DivisionVM> Get();
        Task<IEnumerable<DivisionVM>> Get(int id);
        int Create(DivisionVM divisionVM);
        int Update(int id, DivisionVM divisionVM);
        int Delete(int id);
    }
}
