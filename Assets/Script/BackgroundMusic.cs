using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // ถ้ามีเพลงอยู่แล้ว
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        //  ไม่โดนลบตอนเปลี่ยน Scene
        DontDestroyOnLoad(gameObject);
    }
}