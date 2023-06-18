using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SanduicheScriptableObject", order = 1)]
public class SanduicheScriptableObject : ScriptableObject
{
    public enum Ingrediente {Hamburguer, Bacon, Salada, Queijo, Presunto}

    public string nome;

    public Sprite icone;

    public Ingrediente[] ingredientes;
}
