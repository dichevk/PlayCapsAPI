using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

    }
}
