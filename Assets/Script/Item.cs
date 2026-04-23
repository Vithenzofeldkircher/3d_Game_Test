using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable
{

    public GunElement Collect()
    {
        throw new System.NotImplementedException();
    }
    //Métodos abstratos
    //Forla os filhos a implementarem
    //Usado quando todos os filhos usam, mas com comportamentos diferentes
    //não declaro corpo, apenas a assinatura 
    protected abstract void Teste1();
    //Métodos Virtuais
    //permite que os filhos sobrescrevam, mas não obriga
    //quando apenas alguns filhos tem comportamento diferente


    //se eu sobresescrevo o método virtual do pai
    //ao chamar no filho, o método do filho é executado
    protected virtual void Teste2()
    {
        Debug.Log("Teste2");
    }

    void Start()
    {
    
    }
    // métodos nroamis
    //quando todos os filhos tem o mesmo comportamento
    protected void Teste3()
    {

    }
}
