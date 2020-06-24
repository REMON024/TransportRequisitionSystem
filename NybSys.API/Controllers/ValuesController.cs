using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NybSys.API.Helper;

namespace NybSys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        // GET api/values
        [HttpGet]
        // [SessionCheckByPass]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [NybSysAuthorize("Admin")]
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [SessionCheckByPass]
        [NybSysAllowAnonymous]
        [HttpGet("test-redis")]
        public IActionResult TestRedisCache()
        {
            _distributedCache.Refresh("");
            return Ok();
        }

        [SessionCheckByPass]
        [NybSysAllowAnonymous]
        [HttpGet("get-controller-list")]
        public IActionResult GetActionList()
        {
            //var result = Assembly.GetExecutingAssembly()
            //                    .GetTypes()
            //                    .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
            //                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            //                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            //                    .GroupBy(x => x.DeclaringType.Name)
            //                    .Select(x => new AccessControl()
            //                    {
            //                        ControllerName = x.Key,
            //                        ActionName = x.Select(s => s.Name).ToList()
            //                    })
            //                    .ToList();

            //AccessRights accessRights = new AccessRights()
            //{
            //    RoleName = "Admin",
            //    RightLists = result
            //};

            //System.Xml.Serialization.XmlSerializer writer =
            //new System.Xml.Serialization.XmlSerializer(typeof(AccessRights));

            //var path = Directory.GetCurrentDirectory() + "//AccessXML" + "//Access.xml";
            //System.IO.FileStream file = System.IO.File.Create(path);

            //writer.Serialize(file, accessRights);
            //file.Close();

            //AccessRights accessRights = null;

            //var path = Directory.GetCurrentDirectory() + "//AccessXML" + "//Access.xml";

            //XmlSerializer SerializerObj = new XmlSerializer(typeof(AccessRights));

            //using (StreamReader reader = new StreamReader(path))
            //{
            //    accessRights = (AccessRights)SerializerObj.Deserialize(reader);
            //}



            return Ok();
        }
    }
}
