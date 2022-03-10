namespace HComm.Common
{
    /// <summary>
    ///     Communicator type
    /// </summary>
    public enum CommType
    {
        None,
        Serial,
        Ethernet,
        Usb
    }

    /// <summary>
    ///     Communicator connection types
    /// </summary>
    public enum ConnectionState
    {
        // Connection state
        None,
        Connecting,
        Connected,
        Disconnecting,
        Disconnected
    }

    /// <summary>
    ///     Communicator command
    /// </summary>
    public enum Command
    {
        None = 0x00,
        Read = 0x03,
        Mor = 0x04,
        Write = 0x06,
        Info = 0x11,
        Graph = 0x64,
        GraphRes = 0x65,
        GraphAd = 0xC8,
        Error = 0x80
    }

    /// <summary>
    ///     Monitoring event command types
    /// </summary>
    public enum MonitorCommand
    {
        Backup = 0x04,
        Report = 0x66
    }
}