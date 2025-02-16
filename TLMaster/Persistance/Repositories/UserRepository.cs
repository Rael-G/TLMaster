using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;

namespace TLMaster.Persistance.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Create(User user)
        => _context.Users.Add(user);

    public void Update(User user)
        => _context.Users.Update(user);

    public void Delete(User user)
        => _context.Users.Remove(user);

    public async Task<User?> Get(Guid id)
        => await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<IEnumerable<User>> GetAll()
        => await _context.Users
        .AsNoTracking()
        .ToListAsync();

    public async Task Commit()
        => await _context.SaveChangesAsync();
}
