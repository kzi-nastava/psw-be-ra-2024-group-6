using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ImageDatabaseRepository : IImageRepository
    {
        private readonly StakeholdersContext _dbContext;
        public ImageDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Image image)
        {
            try
            {
                _dbContext.Images.Add(image);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to add image with ID {image.Id}. Error: {ex.Message}");
            }
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Images.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Image Get(long id)
        {
            var i = _dbContext.Images.Where(i => i.Id == id).FirstOrDefault();
            if (i == null)
            {
                throw new KeyNotFoundException($"Image not found: {id}");
            }
            return i;
        }
        public Image Update(Image image)
        {
            try
            {
                _dbContext.Images.Update(image);
                _dbContext.SaveChanges();
                return image;
            }
            catch (DbUpdateException ex)
            {
                throw new KeyNotFoundException(ex.Message);

            }
        }
    }
}
