using System;
[System.Serializable]
/// <summary>
/// 用户基类
/// </summary>
public class User
{
    /// <summary>
    /// 用户id
    /// </summary>
#nullable enable
    public string? id { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>

    public string? Email { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>

    public string? Password { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>

    public string? Username { get; set; }

    /// <summary>
    /// 注册时间
    /// </summary>

    public DateTime? RegTime { get; set; }

    /// <summary>
    /// 最后登陆时间
    /// </summary>

    public DateTime? LastLogInTime { get; set; }

    /// <summary>
    /// 用户状态
    /// </summary>

    public bool Status { get; set; }
    public User()
    {

    }
    public User(string? id, string? email, string? password, string? username, DateTime? regTime, DateTime? lastLogInTime, bool status)
    {
        this.id = id;
        Email = email;
        Password = password;
        Username = username;
        RegTime = regTime;
        LastLogInTime = lastLogInTime;
        Status = status;
    }
}
