using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ArtnetReceiver : MonoBehaviour
{

    private const int ART_NET_PORT = 6454;
    static UdpClient udp;
    Thread thread;
    void Start()
    {
        UDPStart();
    }

    public void UDPStart()
    {
        udp = new UdpClient(ART_NET_PORT);
        udp.EnableBroadcast = true;
        Debug.Log("start");
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    private void ThreadMethod()
    {
        while (true)
        {
            Debug.Log('a');
            IPEndPoint remoteEp = new IPEndPoint(IPAddress.Any, ART_NET_PORT);
            byte[] data = udp.Receive(ref remoteEp);
            string text = Encoding.ASCII.GetString(data);
            Debug.Log(text);
        }
    }

    void OnApplicationQuit()
    {
        thread.Abort();
    }
}
