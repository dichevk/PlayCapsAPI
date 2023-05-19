using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/playcaps")]
    public class PlayCapController : Controller 
    {
        private readonly IPlayCapService _playCapService;
        private readonly IMapper _mapper;

        public PlayCapController(IPlayCapService playCapService, IMapper mapper)
        {
            _playCapService = playCapService;
            _mapper = mapper;
        }
    }
}
