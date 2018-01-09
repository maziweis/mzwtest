using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace yjx.service.prepless.Database
{
    public class DbRepository : IDbRepository
    {
        private IOptions<Settings> settings;

        private IHttpContextAccessor httpContextAccessor;

        private MongoClient client1;
        private IMongoDatabase database1;

        public DbRepository(IOptions<Settings> settings, IHttpContextAccessor httpContextAccessor)
        {
            this.settings = settings;

            this.httpContextAccessor = httpContextAccessor;

            client1 = new MongoClient(settings.Value.dbConnectionString_1);
            if (client1 != null)
                database1 = client1.GetDatabase(settings.Value.dbName_1);
        }

        //public IMongoCollection<task_assign> TaskAssigns()
        //{
        //    return database1.GetCollection<task_assign>("task_assign");
        //}

        //public IMongoCollection<task_student> TaskStudents()
        //{
        //    //int sid = common.TokenHelper.GetSchoolId(httpContextAccessor.HttpContext);//用于未来按照学校分不同IP的Mongodb数据库服务器
        //    return database1.GetCollection<task_student>("task_student");
        //}
    }
}
