# HComm
Hantas tool device communication library

## How to use
1. Install using Nuget.(https://www.nuget.org/packages/HComm/)

2. Add 'using' HComm refrence.

    using HComm;
    using HComm.Common;
    using HComm.Device;

3. Declare the HCommInterface object like this.

    private static HCommInterface hComm = new HCommInterface();

4. Set event and declare event

    hComm.ReceiveMsg = ReceivedMessage;
    hComm.ReceiveRawMsg = ReceivedRawMessage;
    ...
    private void ReceivedMsg(Command cmd, int addr, int[] values) { ... }
    private void ReceivedRawMsg(byte[] packet) { ... }

5. Set the communication type. (One of Serial, Ethernet, Usb)

    hComm.Setup(CommType.Serial);       // Serial type
    hComm.Setup(CommType.Ethernet);     // Ethernet type
    hComm.Setup(CommType.Usb);          // Usb type

6. Connect device

    hComm.Connect(TARGET, OPTION, ID);
> TARGET = COM PORT (Serial), IP ADDRESS (Ethernet), DEVICE ID (USB)
> OPTION = BAUD RATE (Serial), PORT (Ethernet), NOT USE (USB)
> ID = Identifier (Not input = default 1)

7. If the connection is successful, 'IsConnected' property becomes TRUE.

    hComm.IsConnected;      // TRUE

8. Used method

    hComm.GetParam(1, 10);          // GET parameter values. Start address = 1, Count = 10
    hComm.SetParam(1, 0);           // SET parameter value. Set address = 1, value = 0
    hComm.GetInfo();                // GET device information
    hComm.SetRealTime(4002, 1);     // SET event real-time monitoring event value = 0 (stop), value = 1 (start)
    hComm.SetGraph(4100, 1);        // SET event graph monitoring event value = 0 (stop), value = 1 (start)
    hComm.GetState(3300, 14);       // GET current tool status
    hComm.GetGraph(4200, 1);        // GET graph monitoring data

## History
v1.0.1
- Add example
- Raw data monitoring
v1.0.0
- Release Hantas communication library