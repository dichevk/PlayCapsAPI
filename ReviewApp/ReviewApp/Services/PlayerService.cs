﻿using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Services
{
    public class PlayerService : IPlayerService
    {
        private DataContext _context;
        public PlayerService(DataContext context)
        {
            _context = context;
        }
    }
}
