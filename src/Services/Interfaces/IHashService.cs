namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface IHashService
{
    string EncryptyText(string textToEncripty);
    bool CompareHashes(string hashString, string text);
}