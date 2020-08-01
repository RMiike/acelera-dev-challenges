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
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? companyId = null, int? accelerationId = null)
        {
            if (HandleVerify.Verify(companyId, accelerationId))
                return NoContent();

            var result = (companyId != null) ?
                         _service.FindByCompanyId(companyId.Value) :
                         _service.FindByAccelerationId(accelerationId.Value);

            return Ok(_mapper.Map<List<CandidateDTO>>(result));
        }

        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
            => Ok(_mapper
                .Map<CandidateDTO>(_service
                .FindById(userId, accelerationId, companyId)));

        [HttpPost]
        public ActionResult<CandidateDTO> Post(CandidateDTO expected)
            => Ok(_mapper
                .Map<CandidateDTO>(_service
                .Save(_mapper
                    .Map<Candidate>(expected))));
    }
}
