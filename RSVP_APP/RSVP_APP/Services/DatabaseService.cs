using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSVP_APP.Services
{
    public class DatabaseService
{
    readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "rsvp_app.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<User>().Wait();
        _db.CreateTableAsync<EventModel>().Wait();
        _db.CreateTableAsync<Rsvp>().Wait();

        // seed a default user for login testing
        SeedDefaultUser().Wait();
    }

    private async Task SeedDefaultUser()
    {
        var existing = await _db.Table<User>()
            .Where(u => u.Email == "user@example.com")
            .FirstOrDefaultAsync();
        if (existing == null)
        {
            await _db.InsertAsync(new User
            {
                Name = "John Doe",
                Email = "user@example.com",
                Password = "password123",
                Phone = "555-555-5555"
            });
        }
    }

    // === User methods ===
    public Task<int> AddUserAsync(User u) => _db.InsertAsync(u);
    public Task<User> GetUserByEmailAsync(string email) =>
        _db.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
    public Task<User> GetUserByIdAsync(int id) =>
        _db.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();

    // === Event methods ===
    public Task<int> AddEventAsync(EventModel e) => _db.InsertAsync(e);
    public Task<List<EventModel>> GetAllEventsAsync() => _db.Table<EventModel>().ToListAsync();
    public Task<EventModel> GetEventByIdAsync(int id) =>
        _db.Table<EventModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
    public Task<List<EventModel>> GetEventsByHostAsync(int hostUserId) =>
        _db.Table<EventModel>().Where(x => x.HostUserId == hostUserId).ToListAsync();

    // === RSVP methods ===
    public Task<int> AddRsvpAsync(Rsvp r) => _db.InsertAsync(r);
    public Task<List<Rsvp>> GetRsvpsByEventAsync(int eventId) =>
        _db.Table<Rsvp>().Where(x => x.EventId == eventId).ToListAsync();

    public Task<List<EventModel>> GetEventsUserAttendingAsync(int userId)
    {
        return _db.QueryAsync<EventModel>(
            "SELECT e.* FROM EventModel e JOIN Rsvp r ON e.Id = r.EventId WHERE r.UserId = ?",
            userId);
    }

    public Task<int> GetRsvpCountForEventAsync(int eventId) =>
        _db.Table<Rsvp>().Where(r => r.EventId == eventId).CountAsync();

    public Task<Rsvp> GetUserRsvpForEventAsync(int eventId, int userId) =>
        _db.Table<Rsvp>()
           .Where(r => r.EventId == eventId && r.UserId == userId)
           .FirstOrDefaultAsync();
}
}

