using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject energyPrefab;

    public GameObject Generate(float size, Vector3 position)
    {
        var instantiate = Instantiate(energyPrefab, position, Quaternion.identity);
        instantiate.transform.localScale = Vector3.one * size;
        return instantiate;
    }
}