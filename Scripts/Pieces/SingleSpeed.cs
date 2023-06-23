using UnityEngine;

public class SingleSpeed : MonoBehaviour
{
    public float speed = 8;
    protected float acceleration = 1;
    bool taked = false;

    public static SingleSpeed Instance
    {
        get
        {
            return FindObjectOfType<SingleSpeed>();
        }
    }

    private void Update()
    {
        taked = false;
    }

    public float GetSpeed()
    {
        taked = true;
        return acceleration += speed;
    }

    public void LateUpdate()
    {
        if (!taked) acceleration = speed;
    }
}