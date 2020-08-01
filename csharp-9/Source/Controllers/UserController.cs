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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (HandleVerify.Verify(accelerationName, companyId))
                return NoContent();

            var result = (accelerationName == null) ?
                _service.FindByCompanyId(companyId.Value) :
                _service.FindByAccelerationName(accelerationName);

            return Ok(_mapper.Map<List<UserDTO>>(result));
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
            => Ok(_mapper
                .Map<UserDTO>(_service
                .FindById(id)));

        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
             => Ok(_mapper
                 .Map<UserDTO>(_service
                 .Save(_mapper
                     .Map<User>(value))));

    }
}
