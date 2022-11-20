using UnityEditor;
using UnityEngine;

namespace LightEffect.Stress
{
    public class LightGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _lightPrefab;
        [SerializeField] [Min(1)] private int _xSize;
        [SerializeField] [Min(1)] private int _zSize;

        public int GenerateValue()
        {
            return _xSize * _zSize;
        }

        private void Start()
        {
            for (int x = 0; x < _xSize; x++)
            {
                for (int z = 0; z < _xSize; z++)
                {
                    CreateObject(x - _xSize / 2, z - _zSize / 2);
                }
            }
        }

        private void CreateObject(int x, int z)
        {
            Instantiate(_lightPrefab, new Vector3(x, 0f, z), Quaternion.identity, this.transform);
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(LightGenerator))]
    public class LightGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LightGenerator generator = target as LightGenerator;
            EditorGUILayout.IntField("Generate value", generator.GenerateValue());
        }
    }
#endif
}