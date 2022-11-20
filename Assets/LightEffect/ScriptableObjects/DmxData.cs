using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMX/Create DmxData")]
public class DmxData : ScriptableObject
{
    private const byte MAX_UNIVERSE = 4;

    private byte[][] dmx = new byte[MAX_UNIVERSE][];

    public byte[][] Dmx
    {
        get => dmx;
        set => dmx = value;
    }

    public void OnEnable()
    {
        for (int universe = 0; universe < MAX_UNIVERSE; universe++)
        {
            Dmx[universe] = new byte[512];
        }
    }

    public byte[] getDmx(int universe)
    {
        return Dmx[universe];
    }

    public void setDmx(int universe, byte[] dmxData)
    {
        Dmx[universe] = dmxData;
    }
}