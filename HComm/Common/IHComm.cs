using System.Collections.Generic;

namespace HComm.Common
{
    /// <summary>
    ///     HCommInterface acknowledge delegate
    /// </summary>
    /// <param name="cmd">command</param>
    /// <param name="packet">packet</param>
    public delegate void AckData(Command cmd, byte[] packet);

    /// <summary>
    ///     HCommInterface raw acknowledge delegate
    /// </summary>
    /// <param name="packet">packet</param>
    public delegate void AckRawData(byte[] packet);

    /// <summary>
    ///     HCommInterface monitor acknowledge delegate
    /// </summary>
    public delegate void AckMorData(MonitorCommand cmd, byte[] packet);

    /// <summary>
    ///     HCommInterface changed connection state delegate
    /// </summary>
    public delegate void ChangedConnection(bool state);

    public interface IHComm
    {
        /// <summary>
        ///     HCommInterface connection state
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        ///     HCommInterface acknowledge event
        /// </summary>
        AckData AckReceived { set; }

        /// <summary>
        ///     HCommInterface raw acknowledge event
        /// </summary>
        AckRawData AckRawReceived { set; }

        /// <summary>
        ///     HCommInterface monitor acknowledge event
        /// </summary>
        AckMorData AckMorReceived { set; }

        /// <summary>
        ///     HCommInterface connection state changed
        /// </summary>
        ChangedConnection ConnectionChanged { get; set; }

        /// <summary>
        ///     HCommInterface connect
        /// </summary>
        /// <param name="target">device target</param>
        /// <param name="option">connect option</param>
        /// <param name="id">device id</param>
        /// <returns>result</returns>
        bool Connect(string target, int option, byte id = 0x01);

        /// <summary>
        ///     HCommInterface close
        /// </summary>
        /// <returns>result</returns>
        bool Close();

        /// <summary>
        ///     HCommInterface write packet
        /// </summary>
        /// <param name="packet">packet</param>
        /// <param name="length">packet length</param>
        /// <returns>result</returns>
        bool Write(byte[] packet, int length);

        /// <summary>
        ///     HCommInterface get parameter packet
        /// </summary>
        /// <param name="addr">start address</param>
        /// <param name="count">request count</param>
        /// <returns>packet</returns>
        IEnumerable<byte> PacketGetParam(ushort addr, ushort count);

        /// <summary>
        ///     HCommInterface set parameter packet
        /// </summary>
        /// <param name="addr">address</param>
        /// <param name="value">value</param>
        /// <returns>packet</returns>
        IEnumerable<byte> PacketSetParam(ushort addr, ushort value);

        /// <summary>
        ///     HCommInterface get state packet
        /// </summary>
        /// <param name="addr">addr</param>
        /// <param name="count">count</param>
        /// <returns>packet</returns>
        IEnumerable<byte> PacketGetState(ushort addr, ushort count);

        /// <summary>
        ///     HCommInterface get info packet
        /// </summary>
        /// <returns>packet</returns>
        IEnumerable<byte> PacketGetInfo();

        /// <summary>
        ///     HCommInterface get graph packet
        /// </summary>
        /// <param name="addr">not use: address</param>
        /// <param name="count">not use: count</param>
        /// <returns>packet</returns>
        IEnumerable<byte> PacketGetGraph(ushort addr, ushort count);
    }
}