using BlazorApp1.server.DbEntity;

namespace BlazorApp1.server;

public class GameService
{
    private List<server.DbEntity.Game> Games = new();

    private string newGameTitle = string.Empty;

    private readonly AppDbContext db;

    public GameService(AppDbContext appDbContext)
    {
        db = appDbContext;
        Games = GetAllGames();
    }

    public List<Game> GetAllGames()
    {
        return db.Games.ToList(); 
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
        this.Games = this.GetAllGames();
        if (Games.Count == 0)
            return 1;
        return Games.Max(g => g.Id) + 1;
    }
}
