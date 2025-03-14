using TLMaster.Application.Dtos;

namespace TLMaster.Application.Interfaces;

public interface IGuildService : IBaseService<GuildDto>
{
    Task AcceptMember(Guid guildId, Guid applicantId, Guid authenticatedUserId);
    Task RejectMember(Guid guildId, Guid applicantId, Guid authenticatedUserId);
    Task RemoveMember(Guid guildId, Guid memberId, Guid authenticatedUserId);
}
