using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/reviewers")]
    public class ReviewerController : Controller
    {
        private readonly IReviewerService _reviewerService;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerService reviewerService, IMapper mapper)
        {
            _reviewerService = reviewerService;
            _mapper = mapper;
        }
    }
}
