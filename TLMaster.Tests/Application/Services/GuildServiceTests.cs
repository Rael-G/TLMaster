using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using TLMaster.Application.Dtos;
using TLMaster.Application.Exceptions;
using TLMaster.Application.Services;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Tests.Application.Services;


public class GuildServiceTests
{
    private readonly Mock<IGuildRepository> _guildRepositoryMock;
    private readonly GuildService _guildService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<RoleManager<User>> _roleManagerMock;

    public GuildServiceTests()
    {

        _guildRepositoryMock = new Mock<IGuildRepository>();
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _roleManagerMock = new Mock<RoleManager<User>>(Mock.Of<IRoleStore<User>>(), null, null, null, null);
        _mapperMock = new Mock<IMapper>();
        _guildService = new GuildService(_guildRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateGuild_ShouldSucceed()
    {
        // Arrange
        var user = new User() {Id = Guid.NewGuid()};
        var guildDto = new GuildDto { Name = "Guild Name", GuildMasterId = user.Id };
        var guild = new Guild(Guid.NewGuid(), "Guild Name", "", user);

        _mapperMock.Setup(m => m.Map<Guild>(guildDto)).Returns(guild);
        _guildRepositoryMock.Setup(r => r.Create(It.IsAny<Guild>()));
        _guildRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);

        // Ac
        await _guildService.Create(guildDto, user.Id);

        // Assert
        _guildRepositoryMock.Verify(r => r.Create(guild), Times.Once);
        _guildRepositoryMock.Verify(r => r.Commit(), Times.Once);
    }

    
    [Fact]
    public async Task UpdateGuild_ShouldSucceed_IfOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var guildDto = new GuildDto { Id = Guid.NewGuid(), Name = "Updated Guild", GuildMasterId = userId };
        var guild = new Guild(guildDto.Id, "Guild Name", "", new User { Id = userId });

        _guildRepositoryMock.Setup(r => r.GetByIdFull(guildDto.Id)).ReturnsAsync(guild);
        _mapperMock.Setup(m => m.Map(guildDto, guild));
        _guildRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);

        // Act
        await _guildService.Update(guildDto, userId);

        // Assert
        _guildRepositoryMock.Verify(r => r.Commit(), Times.Once);
    }
    

    [Fact]
    public async Task UpdateGuild_ShouldThrowException_IfNotOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var guildDto = new GuildDto { Id = Guid.NewGuid(), Name = "Updated Guild", GuildMasterId = ownerId };
        var guild = new Guild(guildDto.Id, "Guild Name", "", new User { Id = ownerId });

        _guildRepositoryMock.Setup(r => r.GetByIdFull(guildDto.Id)).ReturnsAsync(guild);

        // Act
        var act = async () => await _guildService.Update(guildDto, anotherUserId);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

[Fact]
public async Task DeleteGuild_ShouldSucceed_IfOwner()
{
    // Arrange
    var userId = Guid.NewGuid();
    var guildDto = new GuildDto { Id = Guid.NewGuid() };
    var guild = new Guild(guildDto.Id, "Guild Name", "", new User { Id = userId });

    _guildRepositoryMock.Setup(r => r.GetByIdFull(guild.Id)).ReturnsAsync(guild);
    _guildRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);
    _mapperMock.Setup(m => m.Map<Guild>(guildDto)).Returns(guild);

    // Act
    var result = await _guildService.Delete(guildDto, userId);

    // Assert
    result.Should().BeTrue();
    _guildRepositoryMock.Verify(r => r.Delete(guild), Times.Once);
    _guildRepositoryMock.Verify(r => r.Commit(), Times.Once);
}

[Fact]
public async Task DeleteGuild_ShouldThrowException_IfNotOwner()
{
    // Arrange
    var ownerId = Guid.NewGuid();
    var anotherUserId = Guid.NewGuid();
    var guildDto = new GuildDto { Id = Guid.NewGuid() };
    var guild = new Guild(guildDto.Id, "Guild Name", "", new User { Id = ownerId });

    _guildRepositoryMock.Setup(r => r.GetByIdFull(guild.Id)).ReturnsAsync(guild);

    // Act
    var act = async () => await _guildService.Delete(guildDto, anotherUserId);

    // Assert
    await act.Should().ThrowAsync<ForbiddenAccessException>();
}

    [Fact]
    public async Task GetGuild_ShouldSucceed_IfOwner()
    {
        // Arrange
        var owner = new User { Id = Guid.NewGuid() };
        var guild = new Guild(Guid.NewGuid(), "Guild Name", "", owner);
        var guildDto = new GuildDto { Id = guild.Id };

        _guildRepositoryMock.Setup(r => r.GetByIdFull(guild.Id))
            .ReturnsAsync(guild);
        _mapperMock.Setup(m => m.Map<GuildDto>(guild)).Returns(guildDto);
            
        // Act
        var result = await _guildService.GetByIdFull(guild.Id, owner.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(guild.Id);
    }

    [Fact]
    public async Task GetGuild_ShouldThrowException_IfNotOwner()
    {
        // Arrange
        var owner = new User { Id = Guid.NewGuid() };
        var anotherUser = new User { Id = Guid.NewGuid() };
        var guild = new Guild(Guid.NewGuid(), "Guild Name", "", owner);

        _guildRepositoryMock.Setup(r => r.GetByIdFull(guild.Id))
            .ReturnsAsync(guild);

        // Act
        var act = async () => await _guildService.GetByIdFull(guild.Id, anotherUser.Id);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task GetAllGuilds_ShouldSucceed()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid() };

        _guildRepositoryMock.Setup(r => r.GetAll())
            .ReturnsAsync([]);

        // Act
        var result = await _guildService.GetAll(user.Id);

        // Assert
        result.Should().NotBeNull();
    }
}
