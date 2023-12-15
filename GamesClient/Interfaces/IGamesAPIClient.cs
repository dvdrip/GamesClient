using GamesClient.Services;

namespace GamesClient.Interfaces
{
    public interface IGamesAPIClient
    {
        Task<IEnumerable<Game>> GamesAllAsync();
        Task<Game> GamesGETAsync(int id);
        Task GamesPUTAsync(int id, Game game);
        Task GamesPOSTAsync(Game game);
        Task GamesDELETEAsync(int id);
    }
}
