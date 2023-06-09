namespace CoreMVC5_UsedBookProject.Interfaces
{
    public interface IHashService
    {
        string MD5Hash(string rawString);
        string MD5HashBase64(string rawString);
        string SHA1Hash(string rawString);
        string SHA512Hash(string rawString);
        string AesEncryptBase64(string SourceStr, string CryptoKey);
        string AesDecryptBase64(string SourceStr, string CryptoKey);
        string RandomString(int length);
        string HashPassword(string password);
        bool Verify(string password, string passwordHash);
    }
}
