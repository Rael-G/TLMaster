using System;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Enums;

namespace TLMaster.Application;

public record AuctionDto(
    Guid Id,
    ItemDto Item,
    int InitialPrice,
    DateTime StartTime,
    TimeSpan Duration,
    IReadOnlyList<BidDto> Bids,
    CharacterDto? Winner,
    AuctionStatus Status
) : IDto;

public record BidDto(
    Guid Id,
    CharacterDto Bidder,
    Guid AuctionId,
    int Value
) : IDto;

public record CharacterDto(
    Guid Id,
    string Name,
    Guid? GuildId,
    IReadOnlyList<ItemDto> Itens,
    int Coin,
    Role Role,
    IReadOnlyList<Weapon> Weapons
) : IDto;

public record GuildDto(
    Guid Id,
    Guid GuildMasterId,
    IReadOnlyList<Guid> StaffIds,
    IReadOnlyList<Guid> CharacterIds,
    IReadOnlyList<Guid> AuctionIds,
    IReadOnlyList<Guid> PartyIds
) : IDto;

public record ItemDto(
    Guid Id,
    string Name,
    Guid? OwnerId
) : IDto;

public record PartyDto(
    Guid Id,
    string Name,
    IReadOnlyList<Guid> CharacterIds
) : IDto;

public record UserDto(
    Guid Id,
    IReadOnlyList<Guid> CharacterIds
) : IDto;
