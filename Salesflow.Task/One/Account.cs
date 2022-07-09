namespace Salesflow.Task.One;

public class Account
{
    private string _passwordEncrypted;

    public long AccountId { get; set; }

    public string Username { get; set; }

    public string Password
    {
        get => Encryption.DecryptString(PasswordEncrypted);
        set => PasswordEncrypted = Encryption.EncryptString(value);
    }

    private string PasswordEncrypted
    {
        get => _passwordEncrypted;
        set => _passwordEncrypted = value;
    }
}