using UnityEngine;

public class Gun_Collect : Item
{
    [SerializeField] private GunElement _attributes;

    public override Element Collect()
    {
        Destroy(gameObject);
        return _attributes;
    }

    protected override void Teste1()
    {
        throw new System.NotImplementedException();
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
