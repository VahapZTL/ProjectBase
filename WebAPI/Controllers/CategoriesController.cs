using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryBusiness categoryBusiness;

        public CategoriesController(ICategoryBusiness categoryBusiness)
        {
            this.categoryBusiness = categoryBusiness;
        }
    }
}