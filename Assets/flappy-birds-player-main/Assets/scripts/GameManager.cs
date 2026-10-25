using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Necessário para TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Pontuação e Tempo")]
    public int pontuacao = 0;
    public float tempoRestante = 60f;
    private bool cronometroAtivo = true;

    [Header("Configurações do Boss")]
    public GameObject geradorInimigos;
    public GameObject bossPrefab;
    public Transform bossPontoSpawn;
    private bool bossInstanciado = false;

    [Header("Painéis de UI")]
    public GameObject painelDerrota;
    public GameObject painelVitoria;
    public TextMeshProUGUI textoPontuacao;
    public TextMeshProUGUI textoTempo;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Garante que o jogo corre à velocidade normal ao começar
        Time.timeScale = 1f;

        // Assegura que os painéis começam desativados
        if (painelDerrota != null)
            painelDerrota.SetActive(false);

        if (painelVitoria != null)
            painelVitoria.SetActive(false);
    }

    void Update()
    {
        // Controlo do temporizador
        if (cronometroAtivo)
        {
            tempoRestante -= Time.deltaTime;

            if (textoTempo != null)
                textoTempo.text = "Tempo: " + Mathf.Max(0, Mathf.CeilToInt(tempoRestante)).ToString();

            if (tempoRestante <= 0f)
            {
                cronometroAtivo = false;
                ChamarBoss();
            }
        }

        // Atualização contínua da pontuação no ecrã
        if (textoPontuacao != null)
            textoPontuacao.text = "Score: " + pontuacao.ToString();
    }

    public void AdicionarPontos(int valor)
    {
        pontuacao += valor;
    }

    public void ChamarBoss()
    {
        if (bossInstanciado) return;
        bossInstanciado = true;

        // Desliga o spawner de inimigos comuns para a batalha final
        if (geradorInimigos != null)
        {
            geradorInimigos.SetActive(false);
        }

        // Faz spawn do boss no ponto determinado
        if (bossPrefab != null && bossPontoSpawn != null)
        {
            Instantiate(bossPrefab, bossPontoSpawn.position, Quaternion.identity);
        }
    }

    public void Derrota()
    {
        Time.timeScale = 0f;
        if (painelDerrota != null)
            painelDerrota.SetActive(true);
    }

    public void Vitoria()
    {
        Time.timeScale = 0f;
        if (painelVitoria != null)
            painelVitoria.SetActive(true);
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}