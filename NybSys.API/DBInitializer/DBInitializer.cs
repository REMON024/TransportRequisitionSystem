using Microsoft.Extensions.Caching.Distributed;
using NybSys.AuditLog.DAL;
using NybSys.Common.Utility;
using NybSys.DAL;
using NybSys.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using Action = NybSys.Models.DTO.Action;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NybSys.API.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly AuditLogContext _auditLogContext;
        private readonly DatabaseContext _databaseContext;
        private readonly IDistributedCache _distributedCache;

        public DBInitializer(
            AuditLogContext auditLogContext,
            DatabaseContext databaseContext,
            IDistributedCache distributedCache)
        {
            _auditLogContext = auditLogContext;
            _databaseContext = databaseContext;
            _distributedCache = distributedCache;
        }


        public virtual async Task StartDbInitialize(bool isAuditLogCreate, bool isDatabseCreate)
        {
            if(isAuditLogCreate)
            {
                await CreateAuditLogHelper();
            }

            if(isDatabseCreate)
            {
                await CreateUser();
                
            }

            await CreateAccessRight();
        }

        private async Task CreateUser()
        {
            EncryptionService encryptionService = new EncryptionService();

            Users users = new Users()
            {
                Name = "Super Admin",
                Surname = "Super",
                Username = "superadmin",
                Password = encryptionService.Encrypt("a"),
                PhoneNumber = "01xxxxx21",
                Email = "mail@email.com",
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                Status = (int)Common.Enums.Enums.Status.Active,
                AccessRight = new AccessRight()
                {
                    AccessRightName = "SuperAdmin",
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    Status = (int)Common.Enums.Enums.Status.Inactive,
                }
            };

            await _databaseContext.Users.AddAsync(users);
            await _databaseContext.SaveChangesAsync();
        }

        private async Task CreateAuditLogHelper()
        {
            List<Action> lstActon = new List<Models.DTO.Action>()
            {
                new Action()
                {
                    ActionName = "Insert"
                },
                new Action()
                {
                    ActionName = "Update"
                },
                new Action()
                {
                    ActionName = "Delete"
                },
                new Action()
                {
                    ActionName = "View"
                },
                new Action()
                {
                    ActionName = "Other"
                }
            };

            _auditLogContext.Action.AddRange(lstActon);

            List<LogType> lstLogType = new List<LogType>()
            {
                new LogType()
                {
                    LogTypeName = "Security Log"
                },
                new LogType()
                {
                    LogTypeName = "Error Log"
                },
                new LogType()
                {
                    LogTypeName = "System Log"
                },
                new LogType()
                {
                    LogTypeName = "DB Query"
                },
                new LogType()
                {
                    LogTypeName = "Other"
                }
            };

            _auditLogContext.LogType.AddRange(lstLogType);

            List<Models.DTO.Module> lstModule = new List<Models.DTO.Module>()
            {
                new Models.DTO.Module()
                {
                    ModuleName = "Web"
                },
                new Models.DTO.Module()
                {
                     ModuleName = "Desktop"
                },
                new Models.DTO.Module()
                {
                     ModuleName = "Mobile"
                }
            };

            _auditLogContext.Module.AddRange(lstModule);

            await _auditLogContext.SaveChangesAsync();
        }

        private async Task CreateAccessRight()
        {
            var result = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                                .GroupBy(x => x.DeclaringType.Name)
                                .Select(x => new Models.DTO.Controllers
                                {
                                    ControllerName = x.Key.Replace("Controller", ""),
                                    CreatedBy = "system",
                                    CreatedDate = DateTime.Now,
                                    Status = (int)Common.Enums.Enums.Status.Active,
                                    Title = x.Key,
                                    Actions = x.Select(a => new Actions()
                                    {
                                        Title = a.Name,
                                        ActionName = a.Name,
                                        CreatedBy = "system",
                                        CreatedDate = DateTime.Now,
                                        Status = (int)Common.Enums.Enums.Status.Active
                                    }).ToList()
                                })
                                .ToList();

            await _distributedCache.SetStringAsync("SuperAdmin", JSONConvert.ConvertString(result));

            await _databaseContext.Controllers.AddRangeAsync(result);
            var count = await _databaseContext.SaveChangesAsync();

        }

    }
}
