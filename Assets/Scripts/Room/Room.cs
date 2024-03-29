using System;
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// 房间基类
/// </summary>
public class Room
{
    /// <summary>
    /// 房间Id
    /// </summary>
    /// <value></value>
#nullable enable
    public string? RoomId { get; set; }

    /// <summary>
    /// 房间名
    /// </summary>
    /// <value></value>
    public string? RoomName { get; set; }

    /// <summary>
    /// 房间密码
    /// </summary>
    /// <value></value>
    public string? RoomPassword { get; set; }

    /// <summary>
    /// 房间玩家列表
    /// </summary>
    /// <value></value>
    public List<string> RoomMemberList { get; set; }

    /// <summary>
    /// 房间人数
    /// </summary>
    /// <value></value>
    public int? RoomNumberOfPeople { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    /// <value></value>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// 房间状态（活跃/不活跃）
    /// </summary>
    /// <value></value>
    public bool Status { get; set; }

    public Room(
        string? id,
        string? name,
        string? password,
        int? NumberOfPeople,
        DateTime? createdTime,
        bool status
    )
    {
        RoomId = id;
        RoomPassword = password;
        RoomMemberList = new List<string>();
        RoomNumberOfPeople = NumberOfPeople;
        CreatedTime = createdTime;
        Status = status;
    }

    public Room() {
        RoomMemberList = new List<string>();
     }
}
