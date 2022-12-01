namespace DatabaseEFC.Utils;

/// <summary>
/// Credentials used to connect to PostgreSQL database and execute queries
/// </summary>
public class DbCredentials
{
    /// <summary>
    /// The username of database account
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// The password of database account
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// The host where database is located
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// The database to connect to
    /// </summary>
    public string Database { get; set; }
    /// <summary>
    /// The schema on which the queries will be executed
    /// </summary>
    public string Schema { get; set; }
}