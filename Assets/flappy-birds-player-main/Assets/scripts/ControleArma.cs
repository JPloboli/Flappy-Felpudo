using UnityEngine;

public class ControleArma : MonoBehaviour
{
    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        Vector3 posicao = pontoDeDisparo != null ? pontoDeDisparo.position : transform.position;
        Instantiate(projetilPrefab, posicao, Quaternion.identity);
    }
}