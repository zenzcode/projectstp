using UnityEngine;

[System.Serializable]
public class Vector3Serializeable
{
    [SerializeField]
    private float x, y, z;
    
    public Vector3Serializeable(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public float GetZ()
    {
        return z;
    }

    public void SetX(float x)
    {
        this.x = x;
    }

    public void SetY(float y)
    {
        this.y = y;
    }

    public void SetZ(float z)
    {
        this.z = z;
    }
}