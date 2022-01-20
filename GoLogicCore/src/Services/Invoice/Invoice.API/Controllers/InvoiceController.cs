using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : BaseController
    {
       
        //[HttpGet("{ID}")]
        //public async Task<JsonResult> GetContact([FromRoute] Guid ID) => await HandleControllerAction.Execute(this, new GetContactQuery { ContactID = ID });

        //[HttpPost]
        //public async Task<JsonResult> CreateContact([FromBody] CreateContactCommand command) => await HandleControllerAction.Execute(this, command);
    
    }
}
