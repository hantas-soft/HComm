namespace HComm.Common
{
    /// <summary>
    /// Communicator type
    /// </summary>
    public enum CommType
    {
        None, Serial, Ethernet, Usb
    }
    /// <summary>
    /// Communicator connection types
    /// </summary>
    public enum ConnectionState
    {
        // Connection state
        None, Request, Connecting, Connected,
        // 500ms -> None
        Disconnected, Lost, Error
    }
    /// <summary>
    /// Communicator command
    /// </summary>
    public enum Command
    {
        Read = 0x03,
        Mor = 0x04,
        Write = 0x06,
        Info = 0x11,
        Graph = 0x64,
        GraphRes = 0x65,
        GraphAd = 0xC8,
        Error = 0x80
    }
}