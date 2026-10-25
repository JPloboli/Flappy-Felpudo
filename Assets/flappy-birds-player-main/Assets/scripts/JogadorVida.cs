using UnityEngine;

public class JogadorVida : MonoBehaviour
{
    // Limites verticais da tela da câmera (ajuste os valores conforme o tamanho da sua tela)
    public float limiteSuperiorY = 7f;
    public float limiteInferiorY = 7f;

    void Update()
    {
        // Se sair por cima ou por baixo da tela, morre
        if (transform.position.y > limiteSuperiorY || transform.position.y < limiteInferiorY)
        {
            Morrer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo") || collision.CompareTag("Boss"))
        {
            Morrer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("Boss"))
        {
            Morrer();
        }
    }

    void Morrer()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.Derrota();
        }
        Destroy(gameObject);
    }
}