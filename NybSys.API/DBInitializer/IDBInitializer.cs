using System.Threading.Tasks;

namespace NybSys.API.DBInitializer
{
    public interface IDBInitializer
    {
        Task StartDbInitialize(bool isAuditLogCreate, bool isDatabseCreate);
    }
}
