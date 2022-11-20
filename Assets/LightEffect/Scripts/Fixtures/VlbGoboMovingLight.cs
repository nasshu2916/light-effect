using UnityEngine;
using VLB;

namespace Fixtures
{
    // FixtureName: Stinger spot 11ch
    public class VlbGoboMovingLight : BaseFixture
    {
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _body;
        [SerializeField] private VolumetricLightBeam _beam;
        [SerializeField] private MeshRenderer _lens;
        [SerializeField] private float _emissionIntensity = 3f;

        private const int MaxPan = 270;
        private const int MaxTilt = 135;
        private const float PanCorrectValue = MaxPan / 128f;
        private const float TiltCorrectValue = MaxTilt / 128f;
        private const float MaxMoveSpeed = 750f;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        
        private float _prevPan = 0f;
        private float _prevTilt = 0f;
        
        private void Update()
        {
            byte[] dmx = m_artNetData.Dmx[m_universe - 1];
            float pan = PositionValue(dmx, m_startAddress - 1);
            float tilt = PositionValue(dmx, m_startAddress + 1);
            float dimmer = dmx[m_startAddress + 6] / 255f;
            Color color = GoboColor(dmx[m_startAddress + 3]) * dimmer;

            float maxMove = MaxMoveSpeed * Time.deltaTime;
            pan = Mathf.Clamp(pan , _prevPan - maxMove, _prevPan + maxMove);
            tilt = Mathf.Clamp(tilt , _prevTilt - maxMove, _prevTilt + maxMove);
            
            _head.localRotation = Quaternion.Euler(Vector3.up * pan * PanCorrectValue);
            _body.localRotation = Quaternion.Euler(Vector3.right * tilt * TiltCorrectValue);
            _prevPan = pan;
            _prevTilt = tilt;
            
            _beam.color = color;
            _lens.material.SetColor(EmissionColor, color * _emissionIntensity);
        }

        private float PositionValue(byte[] dmx, int startChannel)
        {
            return dmx[startChannel] + dmx[startChannel + 1] / 255f - 128f;
        }

        private Color GoboColor(int colorValue)
        {
            // TODO: 条件分けのリファクタ
            if (colorValue < 8)
            {
                return Color.white;
            }
            else if (8 <= colorValue && colorValue < 15)
            {
                return Color.red;
            }
            else if (15 <= colorValue && colorValue < 22)
            {
                return new Color(1, 0.5f, 0, 1);
            }
            else if (22 <= colorValue && colorValue < 29)
            {
                return Color.yellow;
            }
            else if (29 <= colorValue && colorValue < 36)
            {
                return Color.green;
            }
            else if (36 <= colorValue && colorValue < 42)
            {
                return Color.blue;
            }
            else if (36 <= colorValue && colorValue < 42)
            {
                return Color.cyan;
            }
            else if (36 <= colorValue && colorValue < 42)
            {
                return Color.magenta;
            }
            else
            {
                return Color.black;
            }

        }
    }
}
