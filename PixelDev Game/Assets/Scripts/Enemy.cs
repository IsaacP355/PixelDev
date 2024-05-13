using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public void TakeDamage()
    {
        // Reduce enemy health or apply any other effects
        Debug.Log("Enemy takes damage!");
    }
}
