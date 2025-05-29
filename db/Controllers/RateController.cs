using db.Models;
using db.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

using TypeId = int;

namespace db.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RateController : BaseCrudController<Rate, TypeId> {
        public RateController(IRepository<Rate, int> repository) : base(repository) { }

        protected override int GetEntityId(Rate entity) {
            return entity.Id;
        }
    }
}
