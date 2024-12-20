using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITouristFavoritesRepository
{
    public TouristFavorites GetByTouristId(int touristId);
}