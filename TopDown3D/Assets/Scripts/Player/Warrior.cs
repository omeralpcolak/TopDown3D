using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private Health mHealth;
    public Health health
    {
        get
        {
            if (mHealth == null)
            {
                mHealth = GetComponent<Health>();
                if (mHealth == null)
                {
                    mHealth = gameObject.AddComponent<Health>();
                    mHealth.hp = 100;
                }
                mHealth.listener = HealthListener;
            }
            return mHealth;
        }
    }

    public virtual void HealthListener(bool _isDead, int _damage)
    {

    }
}
