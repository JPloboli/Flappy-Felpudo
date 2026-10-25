using UnityEngine;

public class Boss : MonoBehaviour
{
    public int vidaMaxima = 15;
    private int vidaAtual;
    public float velocidade = 2f;
    private bool movendoParaCima = true;

    public float limiteCima = 3.5f;
    public float limiteBaixo = -3.5f;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (movendoParaCima)
        {
            transform.Translate(Vector2.up * velocidade * Time.deltaTime);
            if (transform.position.y >= limiteCima)
                movendoParaCima = false;
        }
        else
        {
            transform.Translate(Vector2.down * velocidade * Time.deltaTime);
            if (transform.position.y <= limiteBaixo)
                movendoParaCima = true;
        }
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            DerrotarBoss();
        }
    }

    void DerrotarBoss()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.AdicionarPontos(1000);
            GameManager.instancia.Vitoria();
        }
        Destroy(gameObject);
    }
}
