using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]float effecTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, effecTime);
    }

    
}
