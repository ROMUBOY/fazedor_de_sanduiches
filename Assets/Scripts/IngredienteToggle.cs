using UnityEngine;
using UnityEngine.UI;

public class IngredienteToggle : MonoBehaviour
{
    public SanduicheScriptableObject.Ingrediente ingrediente;

    private GameManager _gameManager;

    private Toggle _toggle;

    // Start is called before the first frame update
    void Start()
    {
        _toggle = GetComponent<Toggle>();
        _gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    public void SelecionarIngrediente()
    {
        if(_toggle.isOn){
            _gameManager.AdicionarIngrediente(ingrediente);
        }

        else {
            _gameManager.RemoverIngrediente(ingrediente);
        }
    }
}
