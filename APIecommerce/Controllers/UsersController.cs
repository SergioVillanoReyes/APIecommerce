using APIecommerce.Models.Dtos;
using APIecommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var users = _userRepository.Getusers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{userId:int}", Name = "Getuser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Getuser(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if(user == null)
            {
                return NotFound($"El usuario id = {userId} no existe");
            }
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost(Name = "RegisterUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto createUserDto)
        {
            if(createUserDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);    
            }

            if (string.IsNullOrWhiteSpace(createUserDto.Username))
            {
                return BadRequest("username es requerido");
            }

            if (!_userRepository.IsUniqueUser(createUserDto.Username))
            {
                return BadRequest("El usuario ya existe");
            }

            var result = await _userRepository.Register(createUserDto);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al registrar el usuario");
            }
            return CreatedAtRoute("GetUser", new { result.id }, result);

        }

        [HttpPost("Login",Name = "LoginUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            if(userLoginDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);    
            }

            if (string.IsNullOrWhiteSpace(userLoginDto.Username))
            {
                return BadRequest("username es requerido");
            }

            if (string.IsNullOrWhiteSpace(userLoginDto.Password))
            {
                return BadRequest("password es requerido");
            }

            var user = await _userRepository.Login(userLoginDto);
            if(user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
            

        }

    }
}
