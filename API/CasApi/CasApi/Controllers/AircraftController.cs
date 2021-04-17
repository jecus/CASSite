using CasApi.Absteractions.Controller;
using CasApi.Absteractions.Middleware;
using Entity.Entity.General;
using Entity.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CasApi.Controllers
{
    [ApiRoute("aircraft"), ApiController, ApiVersion("2.0")]
    public class AircraftController : BaseGetController<Aircraft>
    {
        public AircraftController(DatabaseContext db) : base(db)
        {
        }
    }
}