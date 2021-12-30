using System;
using System.Collections.Generic;
using HComm.Common;

namespace HComm
{
    /// <summary>
    ///     HCommInterface message send struct
    /// </summary>
    public class HCommMsg
    {
        /// <summary>
        ///     HCommInterface send message constructor
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <param name="addr">Address</param>
        /// <param name="count">Count</param>
        /// <param name="packet">Packet</param>
        /// <param name="retry">Retry count</param>
        public HCommMsg(Command cmd, int addr, int count, IEnumerable<byte> packet, int retry = 1)
        {
            Command = cmd;
            Address = addr;
            Count = count;
            Active = false;
            Time = DateTime.Now;
            Retry = retry;
            Packet = new List<byte>(packet);
        }

        public Command Command { get; }

        /// <summary>
        ///     HCommInterface message send address
        /// </summary>
        public int Address { get; }

        /// <summary>
        ///     HCommInterface message send count
        /// </summary>
        public int Count { get; }

        /// <summary>
        ///     HCommInterface message sending wait active status
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     HCommInterface message send time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     HCommInterface message retry send count
        /// </summary>
        public int Retry { get; set; }

        /// <summary>
        ///     HCommInterface message send packet
        /// </summary>
        public List<byte> Packet { get; }
    }
}