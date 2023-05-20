﻿using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class PlayerService : IPlayerService
    {
        private DataContext _context;
        public PlayerService(DataContext context)
        {
            _context = context;
        }

        public async Task<Player?> CreatePlayer(Player player)
        {
            var newPlayer = await _context.Players.AddAsync(player);
            if (newPlayer != null)
            {
                await _context.SaveChangesAsync();
                return newPlayer.Entity;
            }
            return null;
        }

        public async Task<bool> DeletePlayer(int playerId)
        {
            var foundPlayer = await _context.Players.FirstOrDefaultAsync(x => x.Id == playerId);
            if (foundPlayer != null)
            {
                _context.Remove(foundPlayer);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Player?> GetPlayer(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player?> UpdatePlayer(Player player)
        {
            var updated = _context.Update(player);
            var saved = await _context.SaveChangesAsync();
            if (saved > 0)
            {
                return updated.Entity;
            }
            else
            {
                return null;
            }
        }
        public async Task<ICollection<PlayCap>> GetPlayCapByPlayer(int playerId)
        {
            return await _context.PlayCapsPlayers.Where(p => p.Player.Id == playerId).Select(p => p.PlayCap).ToListAsync();
        }
    }
}
