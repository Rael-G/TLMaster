using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using TLMaster.Application.Dtos;
using TLMaster.Application.Exceptions;
using TLMaster.Application.Services;
using TLMaster.Core.Entities;
using TLMaster.Core.Enums;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Tests.Application.Services;

public class CharacterServiceTests
{
    private readonly Mock<ICharacterRepository> _characterRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<RoleManager<User>> _roleManagerMock;
    private readonly CharacterService _characterService;
    

    public CharacterServiceTests()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _mapperMock = new Mock<IMapper>();
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

        _characterService = new CharacterService(
            _characterRepositoryMock.Object, 
            _mapperMock.Object,
            _userManagerMock.Object);
    }

    [Fact]
    public async Task CreateCharacter_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var characterDto = new CharacterDto { Name = "Hero", UserId = userId, GuildId = null };
        var character = new Character(Guid.NewGuid(), "Hero", Role.Dps, new List<Weapon>(), new User { Id = userId });

        _mapperMock.Setup(m => m.Map<Character>(characterDto)).Returns(character);
        _characterRepositoryMock.Setup(r => r.Create(It.IsAny<Character>()));
        _characterRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);

        // Act
        await _characterService.Create(characterDto, userId);

        // Assert
        _characterRepositoryMock.Verify(r => r.Create(character), Times.Once);
        _characterRepositoryMock.Verify(r => r.Create(It.Is<Character>(c => c.GuildId == null)), Times.Once);
        _characterRepositoryMock.Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async Task UpdateCharacter_ShouldSucceed_IfOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var characterDto = new CharacterDto { Id = Guid.NewGuid(), Name = "Updated Hero", UserId = userId };
        var character = new Character(characterDto.Id, "Hero", Role.Healer, [], new User { Id = userId });

        _characterRepositoryMock.Setup(r => r.GetById(characterDto.Id)).ReturnsAsync(character);
        _mapperMock.Setup(m => m.Map(characterDto, character));
        _characterRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);

        // Act
        await _characterService.Update(characterDto, userId);

        // Assert
        _characterRepositoryMock.Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async Task UpdateCharacter_ShouldThrowException_IfNotOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var characterDto = new CharacterDto { Id = Guid.NewGuid(), Name = "Updated Hero", UserId = ownerId };
        var character = new Character(characterDto.Id, "Hero", Role.Healer, [], new User { Id = ownerId });

        _characterRepositoryMock.Setup(r => r.GetById(characterDto.Id)).ReturnsAsync(character);

        // Act
        var act = async () => await _characterService.Update(characterDto, anotherUserId);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task DeleteCharacter_ShouldSucceed_IfOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var characterDto = new CharacterDto { Id = Guid.NewGuid() };
        var character = new Character(characterDto.Id, "Hero", Role.Tank, new List<Weapon>(), new User { Id = userId });

        _characterRepositoryMock.Setup(r => r.GetById(character.Id)).ReturnsAsync(character);
        _characterRepositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<Character>(characterDto)).Returns(character);

        // Act
        var result = await _characterService.Delete(characterDto, userId);

        // Assert
        result.Should().BeTrue();
        _characterRepositoryMock.Verify(r => r.Delete(character), Times.Once);
        _characterRepositoryMock.Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async Task DeleteCharacter_ShouldThrowException_IfNotOwner()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var characterDto = new CharacterDto { Id = Guid.NewGuid() };
        var character = new Character(characterDto.Id, "Hero", Role.Tank, [], new User { Id = ownerId });

        _characterRepositoryMock.Setup(r => r.GetById(character.Id)).ReturnsAsync(character);

        // Act
        var act = async () => await _characterService.Delete(characterDto, anotherUserId);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task GetCharacter_ShouldSucceed_IfOwner()
    {
        // Arrange
        var owner = new User { Id = Guid.NewGuid() };
        var character = new Character(Guid.NewGuid(), "Hero", Role.Dps, [], owner);
        var characterDto = new CharacterDto { Id = character.Id };

        _characterRepositoryMock.Setup(r => r.GetById(character.Id))
            .ReturnsAsync(character);
        _mapperMock.Setup(m => m.Map<CharacterDto>(character)).Returns(characterDto);
            
        // Act
        var result = await _characterService.GetById(character.Id, owner.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(character.Id);
    }

    [Fact]
    public async Task GetCharacter_ShouldThrowException_IfNotOwner()
    {
        // Arrange
        var owner = new User { Id = Guid.NewGuid() };
        var anotherUser = new User { Id = Guid.NewGuid() };
        var character = new Character(Guid.NewGuid(), "Hero", Role.Dps, [], owner);

        _characterRepositoryMock.Setup(r => r.GetById(character.Id))
            .ReturnsAsync(character);

        // Act
        var act = async () => await _characterService.GetById(character.Id, anotherUser.Id);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task GetAllCharacters_ShouldSucceed_IfAdmin()
    {
        // Arrange
        var admin = new User { Id = Guid.NewGuid() };

        _userManagerMock.Setup(u => u.FindByIdAsync(admin.Id.ToString()))
            .ReturnsAsync(admin);
        _roleManagerMock.Setup(r => r.GetRoleNameAsync(admin))
            .ReturnsAsync("admin");
        _characterRepositoryMock.Setup(r => r.GetAll())
            .ReturnsAsync([]);

        // Act
        var result = await _characterService.GetAll(admin.Id);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllCharacters_ShouldThrowException_IfNotAdmin()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid() };

        _userManagerMock.Setup(u => u.FindByIdAsync(user.Id.ToString()))
            .ReturnsAsync(user);
        _roleManagerMock.Setup(r => r.GetRoleNameAsync(user))
            .ReturnsAsync("user");

        // Act
        var act = async () => await _characterService.GetAll(user.Id);

        // Assert
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }
}
