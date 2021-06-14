# HComm
Hantas tool device communication library

## How to use
1. Install using Nuget.(https://www.nuget.org/packages/HComm/)

2. Add 'using' HComm reference.
<pre><code>
    using HComm;
    using HComm.Common;
    using HComm.Device;
</code></pre>
3. Create the HCommInterface object like this.
<pre><code>
    private HCommInterface hComm = new HCommInterface();
</code></pre>
4. Set event and implement event
<pre><code>
    hComm.ReceiveMsg = ReceivedMessage;             // received message
    hComm.ReceiveRawMsg = ReceivedRawMessage;       // received raw packet message
    hComm.ChangedConnection = ChangedState;         // changed connection state
    ...
    private void ReceivedMsg(Command cmd, int addr, int[] values) { ... }
    private void ReceivedRawMsg(byte[] packet) { ... }
    private void ChangedState(bool state) { ... }
</code></pre>
5. Set the communication type. (One of Serial, Ethernet, Usb)
<pre><code>
    hComm.Setup(CommType.Serial);       // Serial type
    hComm.Setup(CommType.Ethernet);     // Ethernet type
    hComm.Setup(CommType.Usb);          // Usb type
</code></pre>
6. Connect device and Set MAX_QUEUE_SIZE
<pre><code>
    // Get serial port list
    var serialPorts = HcSerial.GetPortNames();
    // Get usb device list
    var usbDevices = HcUsb.GetDeviceNames();

    // Connect
    hComm.Connect(TARGET, OPTION, ID);
    // TARGET = COM PORT (Serial), IP ADDRESS (Ethernet), DEVICE ID (USB)
    // OPTION = BAUD RATE (Serial), PORT (Ethernet), NOT USE (USB)
    // ID = Identifier (Not input = default 1)

    // set MAX_QUEUE_SIZE (default: 30)
    hComm.MaxQueueSize = 100;
</code></pre>

7. If the connection is successful, The 'ChangedConnection' event is called.
<pre><code>
    private void ChangedState(bool state)
    {
        Console.WriteLine($@"Connection: {state}");
        Console.WriteLine($@"Connect state: {hComm.State}");
        
        // console debug
        // Connection: true
        // Connect state: Connected
    }
</code></pre>
8. Used command
<pre><code>
    hComm.GetParam(1, 10);          // GET parameter values. Start address = 1, Count = 10
    hComm.SetParam(1, 0);           // SET parameter value. Set address = 1, value = 0
    hComm.GetInfo();                // GET device information (Automatically called when a command is not transmitted for a certain period of time while connected to the device.)
    hComm.SetRealTime(4002, 1);     // SET event real-time monitoring event value = 0 (stop), value = 1 (start)
    hComm.SetGraph(4100, 1);        // SET event graph monitoring event value = 0 (stop), value = 1 (start)
    hComm.GetState(3300, 14);       // GET current tool status
    hComm.GetGraph(4200, 1);        // GET graph monitoring data (AD Only)
</code></pre>

9. Received message implement
<pre><code>
    private void ReceivedMsg(Command cmd, int addr, int[] values)
    {
        // check command
        switch( cmd )
        {
            case Command.Read:
                // parameter READ acknowledge
                break;
            case Command.Mor:
                // device monitoring data
                break;
            case Command.Write:
                // parameter WRITE acknowledge
                break;
            case Command.Info:
                // device INFORMATION acknowledge
                break;
            case Command.Graph:
                // graph monitoring data
                break;
            case Command.GraphRes:      // MDTC only
                // graph monitoring result data
                break;
            case Command.GraphAd:       // AD only
                // graph monitoring data
                break;
            case Command.Error:
                // error
                break;
            default:
                break;
        }
    }
</code></pre>
## History

v1.2.8
 - Change monitor timeout laps
 - Command comm object null check
 
v1.2.7
 - Get Param method 'merge' argument added.
 - lock timeout bug fixed.

v1.2.6
 - Address 2900~ added (Air tool only)

v1.2.5 
 - Connection timeout bug fixed 2

v1.2.4
 - Connection timeout bug fixed

v1.2.3
 - Connection disconnect state bug fixed 2

v1.2.2
 - Connection disconnect state bug fixed

v1.2.1
 - Connection timeout bug fixed

v1.2.0
 - Automatic request command for information

v1.1.9
 - Session state intialize bug fixed

v1.1.8
 - HComm x86 -> AnyCPU
 - System.IO.Ports version changed
 - Document update

v1.1.7
 - HComm x86 rebuild

v1.1.6
 - Information driver serial sort reverse

v1.1.5
 - AD block size customized

v1.1.4
 - AD graph monitoring bug fixed

v1.1.3
 - Information length bug fixed

v1.1.2
 - Message get parameter block size 30 -> 100

v1.1.1
 - Message process timeout error event added

v1.1.0
 - lock queue message bug fixed

v1.0.9
 - auto connection check
 - device information

v1.0.8
 - connection state change event
 
v1.0.7
- thread lock timeout added

v1.0.6
- Message queue count thread-safe

v1.0.5
- GetState method immediately insert

v1.0.4
- Waiting message queue count
- SetParam immediately insert

v1.0.3
- Message process queue size flexible

v1.0.2
- Get parameter block sequence

v1.0.1
- Add example
- Raw data monitoring

v1.0.0
- Release Hantas communication library
