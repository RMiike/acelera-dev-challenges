using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _service;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId, int? userId)
        {
            if (accelerationId == null || userId == null)
                return NoContent();

            var result = _service
                .FindByAccelerationIdAndUserId(accelerationId.Value, userId.Value);

            return Ok(_mapper.Map<List<ChallengeDTO>>(result));
        }

        [HttpPost]
        public ActionResult<ChallengeDTO> Post(ChallengeDTO expected)
            => Ok(_mapper
                .Map<ChallengeDTO>(_service
                .Save(_mapper
                    .Map<Codenation.Challenge.Models.Challenge>(expected))));
    }
}
