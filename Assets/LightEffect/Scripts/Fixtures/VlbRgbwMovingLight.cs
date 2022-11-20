using UnityEngine;
using VLB;

namespace Fixtures
{
    // FixtureName: Inno Pocket Beam Q4 13ch
    public class VlbRgbwMovingLight : BaseFixture
    {
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _body;
        [SerializeField] private VolumetricLightBeam _beam;
        [SerializeField] private MeshRenderer _lens;
        [SerializeField] private float _emissionIntensity = 3f;

        private const int MaxPan = 270;
        private const int MaxTilt = 105;
        private const float PanCorrectValue = MaxPan / 128f;
        private const float TiltCorrectValue = MaxTilt / 128f;
        private const float MaxMoveSpeed = 750f;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private float _prevPan = 0f;
        private float _prevTilt = 0f;

        private void Update()
        {
            byte[] dmx = m_artNetData.Dmx[m_universe - 1];
            float pan = PositionValue(dmx, m_startAddress - 1) * PanCorrectValue;
            float tilt = PositionValue(dmx, m_startAddress + 1) * TiltCorrectValue;
            float dimmer = dmx[m_startAddress + 8] / 255f;
            Color color = LedColor(dmx, m_startAddress + 3) * dimmer;

            float maxMove = MaxMoveSpeed * Time.deltaTime;
            pan = Mathf.Clamp(pan , _prevPan - maxMove, _prevPan + maxMove);
            tilt = Mathf.Clamp(tilt , _prevTilt - maxMove, _prevTilt + maxMove);
            
            _head.localRotation = Quaternion.Euler(Vector3.up * pan);
            _body.localRotation = Quaternion.Euler(Vector3.right * tilt);
            _prevPan = pan;
            _prevTilt = tilt;
            
            _beam.color = color;
            _lens.material.SetColor(EmissionColor, color * _emissionIntensity);
        }

        private float PositionValue(byte[] dmx, int startChannel)
        {
            return dmx[startChannel] + dmx[startChannel + 1] / 255f - 128f;
        }

        private Color LedColor(byte[] dmx, int startChannel)
        {
            float r = dmx[startChannel] / 256f;
            float g = dmx[startChannel + 1] / 256f;
            float b = dmx[startChannel + 2] / 256f;
            Color color = new Color(r, g, b, 1f);

            color += Color.white * 0.6f * dmx[startChannel + 3] / 256f;
            
            return color;
        }
    }
}
