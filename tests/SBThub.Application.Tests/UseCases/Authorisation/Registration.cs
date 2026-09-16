using Moq;
using SBThub.Application.Abstractions;
using SBThub.Application.Contracts.Contracts.Requests.Registration;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Application.UseCases.Authorization.Registration;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using Xunit;

namespace SBThub.Application.Tests.UseCases.Authorisation;

public class RegistrationHandlerTests
{
    private readonly Mock<IRepository> _usersRepo = new();
    private readonly Mock<IRepository> _profilesRepo = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly Mock<IJwtTokenService> _jwtTokenService = new();

    private RegistrationHandler CreateHandler() => new(
        _usersRepo.Object,
        _unitOfWork.Object,
        _passwordHasher.Object,
        _profilesRepo.Object,
        _jwtTokenService.Object);

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessWithToken()
    {
        var request = new RegistrationRequest("Хуесосик", "+380501234567", "huyesosick@test.com", "hdidaok8309");

        _passwordHasher
            .Setup(x => x.Hash(request.Password!))
            .Returns("hashed_password");

        _jwtTokenService
            .Setup(x => x.GenerateAccessToken(It.IsAny<User>()))
            .Returns(new TokenResponse("fake.jwt.token", 3600));

        var handler = CreateHandler();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("fake.jwt.token", result.Value.AccessToken);
        Assert.Equal(3600, result.Value.ExpiresIn);

        _usersRepo.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _profilesRepo.Verify(x => x.Add(It.IsAny<UserProfile>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_MissingEmail_ReturnsFailure()
    {
        var request = new RegistrationRequest("Хуесосик", null, null, "hdidaok8309");

        _passwordHasher
            .Setup(x => x.Hash(request.Password!))
            .Returns("hashed_password");

        var handler = CreateHandler();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailure);

        _usersRepo.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_MissingFullName_ReturnsFailure()
    {
        var request = new RegistrationRequest("", null, "huyesosick@test.com", "hdidaok8309");

        var handler = CreateHandler();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsFailure);
        _usersRepo.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}