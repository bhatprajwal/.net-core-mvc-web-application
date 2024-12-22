namespace Web.Dtos;

public class SessionConfig
{
    public int IdleTimeout { get; set; }
    public bool HttpOnly { get; set; }
    public bool IsEssential { get; set; }
    public string SessionTimeOutIn { get; set; }
}
