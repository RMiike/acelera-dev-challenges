using System.Collections.Generic;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _service;
        private  IMapper _mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
            => Ok(_mapper
                .Map<AccelerationDTO>(_service
                .FindById(id)));

        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId)
        {
            if (companyId == null)
                return NoContent();

            return Ok(_mapper.Map<List<AccelerationDTO>>(_service.FindByCompanyId(companyId.Value)));
        }

        [HttpPost]
        public ActionResult<AccelerationDTO> Post(AccelerationDTO expected)
            => Ok(_mapper
                .Map<AccelerationDTO>(_service
                .Save(_mapper
                    .Map<Acceleration>(expected))));

    }
}
