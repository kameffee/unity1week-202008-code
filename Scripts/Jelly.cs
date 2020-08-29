using System;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField]
    private int amount = 1;

    [SerializeField]
    private GameObject effect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}