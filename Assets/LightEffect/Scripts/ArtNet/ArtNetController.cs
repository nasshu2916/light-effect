using System.Net;
using UnityEngine;
using ArtNet.Sockets;
using ArtNet.Packets;

public class ArtNetController : MonoBehaviour
{

    private ArtNetSocket _artnet = new ArtNet.Sockets.ArtNetSocket();
    [SerializeField] private byte m_universe = 1;
    public DmxData dmxData;
    [SerializeField] private string m_ipAdress;

    [SerializeField] private string m_subNetMask;

    void Start()
    {
        _artnet.EnableBroadcast = true;
        
        _artnet.Open(IPAddress.Parse(m_ipAdress), IPAddress.Parse(m_subNetMask));

        _artnet.NewPacket += (object sender, ArtNet.Sockets.NewPacketEventArgs<ArtNet.Packets.ArtNetPacket> e) =>
        {
            if (e.Packet.OpCode == ArtNet.Enums.ArtNetOpCodes.Dmx)
            {
                ArtNetDmxPacket packet = e.Packet as ArtNet.Packets.ArtNetDmxPacket;

                if (packet.Universe == m_universe - 1 & packet.DmxData != dmxData.Dmx[m_universe - 1])
                {
                    dmxData.Dmx[m_universe - 1] = packet.DmxData;
                }
            }
        };
    }
}
