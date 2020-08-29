using UnityEngine;

public class PlayerDamageEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject effectPrefab;

    public void Emit(Vector3 position)
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}