using Data_Acces_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{   
        public class DatabaseHealthCheckService
        {
            private readonly ApplicationDbContext _dbContext;

            public DatabaseHealthCheckService(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> CanConnectAsync()
            {
                try
                {
                    return await _dbContext.Database.CanConnectAsync();
                }
                catch
                {
                    return false;
                }
            }
        }
}
