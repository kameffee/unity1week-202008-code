using DG.Tweening;
using UnityEngine;

public class BreakEnergyEmitter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    public void Emit(int count)
    {
        var index = Random.Range(0, prefabs.Length);
        var instantiate = Instantiate(prefabs[index], transform.position, Quaternion.identity);
        var toPosition = instantiate.transform.position + new Vector3(Random.Range(-2f, 2f),Random.Range(-2f, 2f), 0);
        instantiate.transform.DOMove(toPosition, 0.5f)
            .SetEase(Ease.OutCirc);
        
    }
}