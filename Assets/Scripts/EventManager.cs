using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void Damage(float health, float damage);
    public static Damage TakeDamage;

    public delegate void Die();
    public static Die GameOver;
}
