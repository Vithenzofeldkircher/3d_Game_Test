using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Guninventory
{
    [SerializeField] private List<GunElement> _guns = new List<GunElement>();

    // Agora o sistema consegue ler a lista real
    public List<GunElement> Guns => _guns;

    public void addweapon(GunElement newgun)
    {
        _guns.Add(newgun);
    }
}



public class GunSystem : MonoBehaviour
{

    [SerializeField] Guninventory _inventory;
    [SerializeField] private Transform _Hand_Gun_Model_Parent;
    private Transform _camera;
    [SerializeField] private GunElement _handGun;
    private float _shootTimer;
    private bool _isReloading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
        _handGun.Initialize();
        _shootTimer = _handGun.ShootRate;
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
        _inventory.addweapon(_handGun);
    }

    // Update is called once per frame
    void Update()
    {
        

        float currentGunIndex = Input.GetAxis("Mouse ScrolWheel");

        if(currentGunIndex != 0)
        {
            ChangeWeapon(currentGunIndex);
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (_handGun.Ammunation <= 0)
                return;

            _handGun.OnReload.Invoke();
        }

        _shootTimer += Time.deltaTime;
        if (_isReloading)
            return;
        if (_shootTimer < _handGun.ShootRate)
            return;
        //Verifica se o player atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (!_handGun.UseAmmunation())//Se não tiver munição, não é possível atirar
            return;
        //Verifica se o player acertou algo
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;
        //Verifica se o objeto acertado implementa IShootable
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        //Aciona o método do contrato IShootable
        shootable.Hitted(_handGun.Damage, target.point);
        _shootTimer = 0;
    }

    private void ChangeWeapon(float nexIndex)
    {
        if (_inventory.Guns.Count <= 0) return;

        int currentIndex = _inventory.Guns.IndexOf(_handGun);
        currentIndex += (int)Mathf.Sign(nexIndex);

        if(currentIndex == _inventory.Guns.Count)
        {
            currentIndex = 0;
        }

        else if (currentIndex < 0)
        {
            currentIndex = _inventory.Guns.Count - 1;
        }

        _handGun = _inventory.Guns[currentIndex];
        ChagenGunVisual();

    }

    IEnumerator Reload()
    {
        _isReloading = true;
        //Trava até ser verdadeiro
        //yield return new WaitUntil(() => _handGun.Ammunation > 0);
        //Trava enquanto for verdadeiro
        //yield return new WaitWhile(() => _handGun.Ammunation <= 0);
        yield return new WaitForSeconds(_handGun.ReloadTime);
        _handGun.Reload();
        _shootTimer = _handGun.ShootRate;//Deixa a arma já pronta para atirar
        _isReloading = false;
    }

    public void Add_New_Gun(GunElement New_Gun)
    {
        _handGun = New_Gun;
        _handGun.Initialize();
        _shootTimer = _handGun.ShootRate;
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
        _inventory.addweapon(New_Gun);
        ChagenGunVisual();
    }

    public void ChagenGunVisual()
    {
        Destroy(_Hand_Gun_Model_Parent.GetChild(0).gameObject);
        GameObject gun = Instantiate(_handGun.Gun_model, _Hand_Gun_Model_Parent);
        gun.layer = LayerMask.NameToLayer("Gun");
        gun.transform.localPosition = new Vector3(0, 0, -gun.transform.localScale.z);
    }

}
