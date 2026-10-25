using UnityEngine;
using System.Collections;

public class GeradorInimigos : MonoBehaviour
{
    // Prefab do inimigo (arraste no Inspector)
    public GameObject inimigoPrefab;

    // Intervalo entre spawns (segundos)
    public float intervalo = 3f;

    // Limites de spawn no cenário
    public float limiteX = 8f;
    public float limiteY = 4f;

    // Velocidade de movimento dos inimigos
    public float velocidade = 3f;

    // Limite X para destruir o inimigo ao sair da tela
    public float limiteDestruicaoX = -12f;

    void Start()
    {
        // Começa a gerar inimigos repetidamente
        InvokeRepeating("GerarInimigo", 0f, intervalo);
    }

    void OnDisable()
    {
        // Cancela o agendamento de novos inimigos caso o gerador seja desativado
        CancelInvoke("GerarInimigo");
    }

    void GerarInimigo()
    {
        if (inimigoPrefab == null) return;

        // Define posição de spawn (à direita da tela)
        float x = limiteX;
        float y = Random.Range(-limiteY, limiteY);
        Vector2 posicaoAleatoria = new Vector2(x, y);

        // Instancia o inimigo
        GameObject inimigo = Instantiate(inimigoPrefab, posicaoAleatoria, Quaternion.identity);

        // Inicia o movimento automático (corrotina)
        StartCoroutine(MoverInimigo(inimigo));
    }

    IEnumerator MoverInimigo(GameObject inimigo)
    {
        // Executa enquanto a referência do inimigo for válida
        while (inimigo != null)
        {
            // Move o inimigo da direita para a esquerda
            inimigo.transform.Translate(Vector2.left * velocidade * Time.deltaTime);

            // Garante que o inimigo ainda existe antes de checar a posição X
            if (inimigo != null && inimigo.transform.position.x < limiteDestruicaoX)
            {
                Destroy(inimigo);
                yield break; // Encerra a corrotina com segurança
            }

            yield return null; // Aguarda o próximo frame
        }
    }
}