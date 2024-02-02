using System.Text;
using System.Security.Cryptography;

using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.HashService;
public class HashService(IAppSettings appSettings) : IHashService
{
    private readonly IAppSettings _appSettings = appSettings;

    public string EncryptyText(string textToEncripty)
    {
        try
        {
            var salt = _appSettings.HashSalt;

            var textBytes = Encoding.UTF8.GetBytes(textToEncripty + salt);
            byte[] hashBytes = SHA256.HashData(textBytes);

            return Convert.ToHexString(hashBytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(Constants.INTERNAL_SERVER_ERROR_MESSAGE);
        }
    }

    public bool CompareHashes(string hashString, string text)
    {
        var hashText = EncryptyText(text);

        return hashText == hashString ? true : false;
    }
}