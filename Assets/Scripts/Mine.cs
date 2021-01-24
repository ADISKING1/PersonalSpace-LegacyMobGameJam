using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float waitTime = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Trap[] trap = GetComponentsInChildren<Trap>();
        Destroy(gameObject, 0.01f);
        Gamemanager.Instance.AfterMath(waitTime, transform.position, trap[0]);
    }
}
