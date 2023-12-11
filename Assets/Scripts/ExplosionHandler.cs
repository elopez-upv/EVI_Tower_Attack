using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    private void FixedUpdate()
    {
        Destroy(gameObject, 0.5f);
    }
}
