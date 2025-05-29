using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : BaseCrudController<Models.Route, TypeId> {
        public RouteController(IRepository<Models.Route, int> repository) : base(repository) { }

        protected override int GetEntityId(Models.Route entity) {
            return entity.Id;
        }
    }
}
