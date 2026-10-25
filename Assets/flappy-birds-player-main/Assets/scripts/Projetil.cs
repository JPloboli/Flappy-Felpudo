using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public float tempoDeVida = 3f;
    public int dano = 1;

    void Start()
    {
        // Destrói o projétil se não acertar nada após alguns segundos
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            // Adiciona pontuação e destrói o inimigo
            if (GameManager.instancia != null)
            {
                GameManager.instancia.AdicionarPontos(100);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TomarDano(dano);
            }
            Destroy(gameObject);
        }
    }
}