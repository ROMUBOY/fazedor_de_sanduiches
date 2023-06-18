using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public SanduicheScriptableObject[] sanduiches;

    public Image sanduicheIconeUi;

    public TextMeshProUGUI nome;

    public TextMeshProUGUI receita;

    public TextMeshProUGUI pontuacaoUi;

    public TextMeshProUGUI pontuacaoFinalUi;

    public Toggle[] ingredientesToggles;

    public Button prepararSanduicheButton;

    public float tempoEmSegundos = 120;

    public TextMeshProUGUI tempoUi;

    public GameObject telaReiniciar;

    public GameObject gameplayElements;

    private int _pontuação = 0;

    private SanduicheScriptableObject _proximoSanduiche;

    private List<SanduicheScriptableObject.Ingrediente> ingredientesSelecionados;
    
    // Start is called before the first frame update
    void Start()
    {
        SelecionarProximoSanduiche();
    }

    // Update is called once per frame
    void Update()
    {
        if(tempoEmSegundos <= 0)
        {
            gameplayElements.SetActive(false);
            pontuacaoFinalUi.text = _pontuação.ToString();
            telaReiniciar.SetActive(true);
            return;
        }

        tempoEmSegundos -= Time.deltaTime;
        var ts = System.TimeSpan.FromSeconds(tempoEmSegundos);
         
        tempoUi.text = "Tempo: " + string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    void SelecionarProximoSanduiche()
    {
        _proximoSanduiche = sanduiches[Random.Range(0, sanduiches.Length)];

        sanduicheIconeUi.sprite = _proximoSanduiche.icone;

        nome.text = _proximoSanduiche.nome;

        receita.text = string.Empty;

        foreach(SanduicheScriptableObject.Ingrediente ingrediente in _proximoSanduiche.ingredientes)
        {            
            receita.text = receita.text + ingrediente.ToString() + "\n";
        }

        ingredientesSelecionados = new List<SanduicheScriptableObject.Ingrediente>();

        foreach(Toggle toggle in ingredientesToggles)
        {
            toggle.interactable = true;
            toggle.isOn = false;
        }

        prepararSanduicheButton.interactable = false;
    }

    public void AdicionarIngrediente(SanduicheScriptableObject.Ingrediente ingrediente){
        ingredientesSelecionados.Add(ingrediente);

        if(ingredientesSelecionados.Count == 3){
            foreach(Toggle toggle in ingredientesToggles)
            {
                if(!toggle.isOn){
                    toggle.interactable = false;
                }
            }

            prepararSanduicheButton.interactable = true;
        }
    }

    public void RemoverIngrediente(SanduicheScriptableObject.Ingrediente ingrediente){
        ingredientesSelecionados.Remove(ingrediente);

        if(ingredientesSelecionados.Count == 2){
            foreach(Toggle toggle in ingredientesToggles)
            {
                toggle.interactable = true;             
            }

            prepararSanduicheButton.interactable = false;
        }
    }

    public void PrepararSanduiche(){
        foreach(SanduicheScriptableObject.Ingrediente ingrediente in _proximoSanduiche.ingredientes)
        {
            if(!ingredientesSelecionados.Contains(ingrediente))
            {
                AtualizarPontuacao(-1);
                SelecionarProximoSanduiche();
                return;
            }
        }

        AtualizarPontuacao(1);
        SelecionarProximoSanduiche();
    }

    void AtualizarPontuacao(int valor){
        _pontuação += valor;
        pontuacaoUi.text = "Pontuação: " + _pontuação.ToString();

        if(_pontuação < 0){
            _pontuação = 0;
        }
    }
}
