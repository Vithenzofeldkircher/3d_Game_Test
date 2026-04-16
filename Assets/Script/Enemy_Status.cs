using UnityEngine;

public class Enemy_Status : MonoBehaviour, IShootable
{
    [SerializeField] float _lifeMax = 2f;
    [SerializeField] private GameObject _bloodEffect;
    private float _curretnLife;

    public void Hitted(float damege, Vector3 shootPoint)
    {
        _curretnLife -= damege;

        GameObject blood = Instantiate(_bloodEffect, shootPoint, Quaternion.LookRotation(shootPoint - transform.position));
        blood.transform.SetParent(transform);
        if (_curretnLife > 0)
            return;

       
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _curretnLife = _lifeMax;
    }

}
