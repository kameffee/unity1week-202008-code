using System;
using UnityEngine;

public class AttractArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Jelly"))
        {
            var vector = transform.position - other.transform.position;
            other.transform.Translate(vector.normalized * 5f * Time.deltaTime);
        }
    }
}