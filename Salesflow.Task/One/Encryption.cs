using System.Text;

namespace Salesflow.Task.One;

public static class Encryption
{
    public static string DecryptString(string passwordEncrypted)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(passwordEncrypted));
    }

    public static string EncryptString(string value)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
}