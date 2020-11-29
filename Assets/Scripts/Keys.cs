/// <summary>
/// Used to store references to various keys used by the game and allow
/// for quick global updates. Static class is used to prevent changes by
/// other classes/methods.
/// </summary>
public static class Keys
{
    public static string        JUMP { get { return "space"; } }
    public static string        REVERSE { get { return "r"; } }
    public static string        PAUSE { get { return "escape"; } }
}
