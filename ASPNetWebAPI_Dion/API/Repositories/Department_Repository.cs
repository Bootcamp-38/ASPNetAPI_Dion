using API.Models;
using API.Repositories.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repositories
{
    public class Department_Repository : Department_Interface
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
        public int Create(Department department)
        {
            var sp = "SP_Insert_Department";
            parameters.Add("@Name", department.Name);
            var create = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int id)
        {
            var sp = "SP_Delete_Department";
            parameters.Add("@Id", id);
            var delete = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            return delete;
        }

        public IEnumerable<Department> Get()
        {
            var sp = "SP_GetAll_Department";
            var getData = connection.Query<Department>(sp, commandType: CommandType.StoredProcedure);
            return getData;
        }

        public Task<IEnumerable<Department>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, Department department)
        {
            var sp = "SP_Update_Department";
            parameters.Add("@Id", id);
            parameters.Add("@Name", department.Name);
            var update = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            //throw new NotImplementedException();
            return update;
        }
    }
}