using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkAd.Data;

namespace WorkAd.Data
{
    public class WorkAdvertService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WorkAdvertService> _logger;

        public WorkAdvertService(AppDbContext context, ILogger<WorkAdvertService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<WorkAdvert>> GetAllWorkAdvertsAsync()
        {
            try
            {
                return await _context.WorkAdvert
                    .OrderByDescending(advert => advert.CreatedDate)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all work adverts");
                throw;
            }
        }

        public async Task<WorkAdvert?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.WorkAdvert.FindAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching WorkAdvert by id {Id}", id);
                throw;
            }
        }

        public Task<WorkAdvert?> GetWorkAdvertByIdAsync(int id) => GetByIdAsync(id);

        public async Task<WorkAdvert> AddWorkAdvertAsync(WorkAdvert workAdvert)
        {
            if (workAdvert == null) throw new ArgumentNullException(nameof(workAdvert));

            try
            {
                workAdvert.CreatedDate = DateTime.UtcNow;
                _context.WorkAdvert.Add(workAdvert);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return workAdvert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the work advert");
                throw;
            }
        }

        public async Task<WorkAdvert> UpdateWorkAdvertAsync(WorkAdvert workAdvert)
        {
            if (workAdvert == null) throw new ArgumentNullException(nameof(workAdvert));

            try
            {
                _context.Entry(workAdvert).State = EntityState.Modified;
                // Ensure CreatedDate is not overwritten
                _context.Entry(workAdvert).Property(x => x.CreatedDate).IsModified = false;

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return workAdvert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing the work advert");
                throw;
            }
        }

        public async Task<bool> DeleteWorkAdvertAsync(int id)
        {
            try
            {
                var advertToDelete = await _context.WorkAdvert.FindAsync(id).ConfigureAwait(false);

                if (advertToDelete != null)
                {
                    _context.WorkAdvert.Remove(advertToDelete);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the work advert with id {Id}", id);
                throw;
            }
        }
    }
}
