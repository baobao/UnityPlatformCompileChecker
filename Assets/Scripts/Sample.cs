using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS
        Test();
#endif
    }
}