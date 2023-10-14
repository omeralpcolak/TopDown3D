using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    public GameObject ring;
    public GameObject bossIdleEffect;

    public float animRotateSpeed;

    public int bossCurrentHealth, bossMaxHealth;

    private Material cubeMaterial;

    private Color targetColor = Color.red;
    private Color initColor;

    private void Awake()
    {
        cubeMaterial = GetComponent<Renderer>().material;

    }

    private void Start()
    {
        bossCurrentHealth = bossMaxHealth;
        initColor = cubeMaterial.color;
    }



    public void BossTakeDamage(int damageAmount)
    {
        bossCurrentHealth -= damageAmount;

        cubeMaterial.DOColor(targetColor, 0.2f).OnComplete(delegate
        {
            cubeMaterial.DOColor(initColor, 0.2f);
        });

        if(bossCurrentHealth <= 0)
        {
            bossCurrentHealth = 0;
            gameObject.transform.DOScale(0, 1f).OnComplete(delegate
            {
                gameObject.SetActive(false);
                //Instantiate death effect;
            });
        }
    }

    public void BossAnim()
    {
        transform.DORotate(new Vector3(0f, 360f, 360f), animRotateSpeed, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        bossIdleEffect.gameObject.SetActive(true);

        ring.transform.DORotate(new Vector3(360f, 360f, 0f), animRotateSpeed/2, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
