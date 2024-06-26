using ChatApp.Domain.Entities;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Persistence.Repositories;

public class ParticipantRepository(AppDbContext database) : GenericRepository<Participant>(database), IParticipantRepository
{
}
