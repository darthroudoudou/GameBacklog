using BlazorApp1.server.DbEntity;

namespace BlazorApp1.server;

public class GameService : IGameService
{
    private List<Game> Games = new();

    private readonly AppDbContext db;

    public GameService(AppDbContext appDbContext)
    {
        db = appDbContext;
        Games = RetrieveGames();
    }

    public List<Game> RetrieveGames(bool isTodo = false, Platform? platform = null, string? sortBy = null, bool isDescending = false, bool? isRetro = null)
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

        if (isRetro != null)
        {
            query = query.Where(x => x.IsRetro == isRetro);
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            switch (sortBy.Trim().ToLowerInvariant())
            {
                case "title":
                    query = isDescending ? query.OrderByDescending(g => g.Title) : query.OrderBy(g => g.Title);
                    break;
                case "platform":
                    query = isDescending ? query.OrderByDescending(g => g.Platform) : query.OrderBy(g => g.Platform);
                    break;
                case "iscompleted":
                    query = isDescending ? query.OrderByDescending(g => g.IsCompleted) : query.OrderBy(g => g.IsCompleted);
                    break;
                case "istodo":
                    query = isDescending ? query.OrderByDescending(g => g.IsTodo) : query.OrderBy(g => g.IsTodo);
                    break;
                case "isretro":
                    query = isDescending ? query.OrderByDescending(g => g.IsRetro) : query.OrderBy(g => g.IsRetro);
                    break;
                case "order":
                    if (isDescending)
                    {
                        query = query.OrderBy(g => g.Order == null).ThenByDescending(g => g.Order);
                    }
                    else
                    {
                        query = query.OrderBy(g => g.Order == null).ThenBy(g => g.Order);
                    }
                    break;
                default:
                    break;
            }
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
