using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    List<GameObject> Triggers = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Triggers.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Triggers.Remove(collision.gameObject);
    }
    public List<GameObject> GetCollisions()
    {
        return Triggers;
    }
}
