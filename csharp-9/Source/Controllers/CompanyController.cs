using System.Collections.Generic;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Source.Services;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll( int? userId = null, int? accelerationId = null)
        {
            if (HandleVerify.Verify(userId,accelerationId))
                    return NoContent();

            var result = (accelerationId != null) ?
                        _service.FindByAccelerationId(accelerationId.Value) :
                        _service.FindByUserId(userId.Value);

            return Ok(_mapper.Map<List<CompanyDTO>>(result));
        }
      
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
             => Ok(_mapper
                 .Map<CompanyDTO>(_service
                 .FindById(id)));

        [HttpPost]
        public ActionResult<CompanyDTO> Post(CompanyDTO expected)
             => Ok(_mapper
                 .Map<CompanyDTO>(_service
                 .Save(_mapper
                     .Map<Company>(expected))));
    }
}
