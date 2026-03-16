using BlazorApp1.server.DbEntity;

namespace BlazorApp1.server;

public class GameService
{
    private List<Game> Games = new();

    private readonly AppDbContext db;

    public GameService(AppDbContext appDbContext)
    {
        db = appDbContext;
        Games = RetrieveGames();
    }

    public List<Game> RetrieveGames(bool isTodo = false, Platform? platform = null)
    {
        IQueryable<Game> query = db.Games;

        if (isTodo == true)
        {
            query = query.Where(x => x.IsTodo);
        }

        if (platform != null)
        {
            query = query.Where(x => x.Platform == platform);
        }

        return query.ToList();
    }

    public void AddGame(Game newGame)
    {
        db.Games.Add(newGame);
        db.SaveChanges();
    }

    public void UpdateGame(Game updatedGame)
    {
        db.Games.Update(updatedGame);
        db.SaveChanges();
    }

    public void DeleteGame(int id)
    {
        var item = db.Games.FirstOrDefault(t => t.Id == id);
        if (item != null)
        {
            db.Games.Remove(item);
            db.SaveChanges();
        }

    }

    public int GetNextId()
    {
        this.Games = this.RetrieveGames();
        if (Games.Count == 0)
            return 1;
        return Games.Max(g => g.Id) + 1;
    }
}
