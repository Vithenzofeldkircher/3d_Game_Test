using UnityEngine;

public class Enemy_Status : MonoBehaviour, IShootable
{
    public void Hitted(float damege)
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
