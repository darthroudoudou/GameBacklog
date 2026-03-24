namespace BlazorApp1.server;

using BlazorApp1.server.DbEntity;

public interface IGameService
{
    public List<Game> RetrieveGames(bool isTodo = false, Platform? platform = null, string? sortBy = null, bool descending = false, bool? isRetro = null);

    public void AddGame(Game newGame);

    public void UpdateGame(Game updatedGame);

    public void DeleteGame(int id);

    public int GetNextId();
}