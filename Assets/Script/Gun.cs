using UnityEngine;

[System.Serializable]
public class GunElement
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootRate;
    [SerializeField] private float _Clip_Size; // quantidade de balas que o pente suporta
    [SerializeField] private float _ammunation; // municao total de arma para referencia pro jogo
    private float _Ammunation_Clip; // ponte atual sendo utilizado até ter que puxar mais

    public GunElement(string name, float damage, float shootRate, float ammunation)
    {
        _name = name;
        _damage = damage;
        _shootRate = shootRate;
        _ammunation = ammunation;
    }

    public void Initialize()
    {
        _Ammunation_Clip = _Clip_Size;

    }

    public bool Use_Ammunation()
    {
        Debug.Log(_Ammunation_Clip);

        if (_Ammunation_Clip <= 0)
          return false;

        _Ammunation_Clip--;
        return true;
    }



    public string Name { get => _name; }
    public float Damage { get => _damage; }
    public float ShootRate { get => _shootRate; }
    public float Ammunation { get => _ammunation; }
}
public class Gun : MonoBehaviour
{
    private Transform _camera;
    [SerializeField] private GunElement _handGun;
    private float _shootTimer;
    void Start()
    {
        _camera = Camera.main.transform;
        _handGun.Initialize();
        _shootTimer = _handGun.ShootRate;
    }


    void Update()
    {

        _shootTimer += Time.deltaTime;
        if (_shootTimer < _handGun.ShootRate)
            return;
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (!_handGun.Use_Ammunation())
            return;
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        shootable.Hitted(1, target.point);
        _shootTimer = 0;
    }
}