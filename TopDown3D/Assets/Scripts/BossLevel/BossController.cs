using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    //public GameObject ring;
    public GameObject horizontalRing;
    public GameObject bossIdleEffect;
    public GameObject attackCubesParent;
    public GameObject smallCube;
    public GameObject bossDeathEffect;
    public GameObject bossCanvasHolder;

    public Transform player;

    public float animRotateSpeed;
    public float smallCubeRotateSpeed;
    public float changeInterval;
    public float launchForce;
    public float scaleDuration;
    public float distanceToAttack; //for horizontal ring.

    public Vector3 horizontalRingMinScale = new Vector3(2, 1.5f, 2);
    public Vector3 horizontalRingMaxScale = new Vector3(6, 4.5f, 6);

    private bool isBossDead = false;

    private Camera cam;
    
    [SerializeField]private float rotationVal = -360f;

    public Healthbar bossHealthBar;

    public int bossCurrentHealth;
    public int bossMaxHealth;
    public int smallCubesToLaunch;

    private Material bossMaterial;

    private Color targetColor = Color.red;
    private Color initColor;

    public List<GameObject> attackCubes = new List<GameObject>();

    private void Awake()
    {
        bossMaterial = GetComponent<Renderer>().material;

    }

    private void Start()
    {
        bossCurrentHealth = bossMaxHealth;
        initColor = bossMaterial.color;
        bossHealthBar.UpdateHealthBar(bossMaxHealth, bossCurrentHealth);
        cam = Camera.main;
        
    }

    private void Update()
    {
        Vector3 lookPos = cam.transform.position - new Vector3(0, 15, 0);

        bossCanvasHolder.transform.rotation = Quaternion.LookRotation(bossCanvasHolder.transform.position - lookPos);

        HorizontalRingAttack();
    }


    public void BossTakeDamage(int damageAmount)
    {   
        bossCurrentHealth -= damageAmount;
        bossHealthBar.UpdateHealthBar(bossMaxHealth, bossCurrentHealth);
        bossMaterial.DOColor(targetColor, 0.2f).OnComplete(delegate
        {
            bossMaterial.DOColor(initColor, 0.2f);
        });

        if(bossCurrentHealth <= 0 && !isBossDead)
        {
            bossCurrentHealth = 0;
            StartCoroutine(BossDeath());
            isBossDead = true;
        }
    }

    IEnumerator BossDeath()
    {
        bossCanvasHolder.GetComponent<CanvasGroup>().DOFade(0f, 1f);
        foreach (GameObject attackCube in attackCubes)
        {
            attackCube.transform.DOScale(0f, 1f).OnComplete(delegate
            {
                attackCube.gameObject.SetActive(false);
            });
            
        }

        horizontalRing.transform.DOScale(0, 1f);

        yield return new WaitForSeconds(1f);

        bossMaterial.DOColor(Color.black, 1f);
        Instantiate(bossDeathEffect, transform.position, Quaternion.identity);

        gameObject.transform.DOScale(0, 1f).OnComplete(delegate
        {
            
            gameObject.SetActive(false);
            CameraShake.instance.ShakeCamera(3.2f);

            for (int i = 0; i < smallCubesToLaunch; i++)
            {
                GameObject cube = Instantiate(smallCube, transform.position, Quaternion.identity);
                Rigidbody rb = cube.GetComponent<Rigidbody>();

                Vector3 randomDirection = Random.insideUnitSphere;
                rb.AddForce(randomDirection * launchForce, ForceMode.Impulse);
            }
        });

        GameManager.instance.Victory();
    }

    public void BossAnim()
    {
        bossCanvasHolder.transform.parent = null;
        bossCanvasHolder.GetComponent<CanvasGroup>().DOFade(1f, 1f);

        transform.DORotate(new Vector3(0f, 360f, 360f), animRotateSpeed, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        bossIdleEffect.gameObject.SetActive(true);

        horizontalRing.gameObject.SetActive(true);

        horizontalRing.transform.DORotate(new Vector3(0f, 360f, 0f), animRotateSpeed / 2, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);



        StartCoroutine(AttackCubes());
    }

    private void HorizontalRingAttack()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        Debug.Log(distance);

        if (distance < distanceToAttack)
        {
            horizontalRing.transform.DOScale(horizontalRingMaxScale, scaleDuration);
        }
        if (distance > distanceToAttack)
        {
            horizontalRing.transform.DOScale(horizontalRingMinScale, scaleDuration);
        }

    }

    IEnumerator AttackCubes()
    {
        attackCubesParent.transform.parent = null;

        foreach(GameObject attackCube in attackCubes)
        {
            attackCube.gameObject.SetActive(true);
            attackCube.transform.DOScale(0.22f, 2f);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(ChangeRotation());
    }

    IEnumerator ChangeRotation()
    {
        changeInterval = Random.Range(4, 8);
        smallCubeRotateSpeed = Random.Range(6, 8);

        rotationVal = -rotationVal;

        attackCubesParent.transform.DORotate(new Vector3(0f, rotationVal, 0f), smallCubeRotateSpeed, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        yield return new WaitForSeconds(changeInterval);

        StartCoroutine(ChangeRotation());
    }
}
