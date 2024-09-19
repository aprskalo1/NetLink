using FirebaseAdmin.Auth;
using NetLink.API.DTOs.Request;
using NetLink.API.Exceptions;
using NetLink.API.Repositories;

namespace NetLink.API.Services.Auth;

public interface IFirebaseService
{
    Task<string> AuthoriseFirebaseClient(string firebaseToken);
}

public class FirebaseService(
    FirebaseAuth firebaseAdmin,
    IJwtTokenService jwtTokenService,
    IDeveloperRepository developerRepository,
    IDeveloperService developerService)
    : IFirebaseService
{
    public async Task<string> AuthoriseFirebaseClient(string firebaseToken)
    {
        var decodedToken = await firebaseAdmin.VerifyIdTokenAsync(firebaseToken);

        var userEmail = decodedToken.Claims["email"].ToString();
        if (userEmail == null)
        {
            throw new DeveloperException("User email not found in token");
        }

        var foundDeveloper = await developerRepository.FindDeveloperByUsernameAsync(userEmail);
        if (foundDeveloper != null) return jwtTokenService.GenerateToken(foundDeveloper);

        var developerReq = new DeveloperRequestDto()
        {
            Username = userEmail
        };

        var newDeveloperId = await developerService.AddDeveloperAsync(developerReq);
        return jwtTokenService.GenerateToken(await developerRepository.GetDeveloperByIdAsync(newDeveloperId));
    }
}