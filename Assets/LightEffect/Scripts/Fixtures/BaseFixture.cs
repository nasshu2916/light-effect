using UnityEngine;

namespace Fixtures
{
    public class BaseFixture : MonoBehaviour
    {
        [SerializeField] protected int m_startAddress = 1;
        [SerializeField] protected byte m_universe = 1;
        [SerializeField] protected DmxData m_artNetData;

        public void OnEnable()
        {
        }
    }
}
