using UnityEngine;

public class PlayerInteractable : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.TryGetComponent(out ICollectable collectable))
            return;

        switch (other.gameObject.tag)
        {
            case "Gun":
                print("colidiu");
                _gunSystem.Add_New_Gun((GunElement)collectable.Collect());
                break;
            case "Ammo":

                break;
            case "Armor":
                break;
            default:
                break;
        }
    }
}