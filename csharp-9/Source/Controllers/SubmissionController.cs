using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
       
        [HttpGet("higherScore")]
        public ActionResult<SubmissionDTO> GetHigherScore(int? challengeId)
        {
            if (challengeId == null)
                return NoContent();

            return Ok(_service.FindHigherScoreByChallengeId(challengeId.Value));
        }

        [HttpGet]
        public ActionResult<SubmissionDTO> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (challengeId == null && accelerationId == null)
                return NoContent();

            var result = _service
                .FindByChallengeIdAndAccelerationId(challengeId.Value, accelerationId.Value);

            return Ok(_mapper
                .Map<List<SubmissionDTO>>(result));
        }

        [HttpPost]
        public ActionResult<SubmissionDTO> Post(SubmissionDTO expected)
            => Ok(_mapper
                .Map<SubmissionDTO>(_service
                .Save(_mapper
                    .Map<Submission>(expected))));
    }
}
