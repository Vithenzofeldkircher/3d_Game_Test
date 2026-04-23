using UnityEngine;

public class Player_Interactable : MonoBehaviour
{
    private GunSystem _gunSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gunSystem = GetComponentInParent<GunSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollectable collectale))
            return;
        
        _gunSystem.Add_New_Gun(collectale.Collect());
    }

}
