using UnityEngine;
using VLB;

namespace Fixtures
{
    public class VlbRgbwLight : BaseFixture
    {
        [SerializeField] private VolumetricLightBeam _beam;
        [SerializeField] private MeshRenderer _lens;
        [SerializeField] private float _emissionIntensity = 3f;

        private Color _color;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Update()
        {
            float r = m_artNetData.Dmx[m_universe - 1][m_startAddress - 1] / 256f;
            float g = m_artNetData.Dmx[m_universe - 1][m_startAddress] / 256f;
            float b = m_artNetData.Dmx[m_universe - 1][m_startAddress + 1] / 256f;
            Color color = new Color(r, g, b, 1f);
            
            color += Color.white * 0.6f * m_artNetData.Dmx[m_universe - 1][m_startAddress + 2] / 256f;

            _beam.color = _color;
            _lens.material.SetColor(EmissionColor, _color * _emissionIntensity);
        }
    }
}