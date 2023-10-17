using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    public GameObject ring;
    public GameObject bossIdleEffect;
    public GameObject attackCubesParent;
    public GameObject smallCube;

    public float animRotateSpeed;
    public float changeInterval;
    public float launchForce = 10f;
    

    [SerializeField]private float rotationVal = -360f;

    public int bossCurrentHealth;
    public int bossMaxHealth;
    public int smallCubesToLaunch = 10;

    private Material cubeMaterial;

    private Color targetColor = Color.red;
    private Color initColor;

    public List<GameObject> attackCubes = new List<GameObject>();

    private void Awake()
    {
        cubeMaterial = GetComponent<Renderer>().material;

    }

    private void Start()
    {
        bossCurrentHealth = bossMaxHealth;
        initColor = cubeMaterial.color;
    }

    private void Update()
    {
        
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

    IEnumerator BossDeath()
    {
        foreach(GameObject attackCube in attackCubes)
        {
            attackCube.transform.DOScale(0f, 1f);
        }

        yield return new WaitForSeconds(1f);

        cubeMaterial.DOColor(Color.black, 1f);

        gameObject.transform.DOScale(0, 1f).OnComplete(delegate
        {
            gameObject.SetActive(false);

            for (int i = 0; i < smallCubesToLaunch; i++)
            {
                GameObject cube = Instantiate(smallCube, transform.position, Quaternion.identity);
                Rigidbody rb = cube.GetComponent<Rigidbody>();

                Vector3 randomDirection = Random.insideUnitSphere;
                rb.AddForce(randomDirection * launchForce, ForceMode.Impulse);
            }
        });
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


        StartCoroutine(AttackCubes());
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

        rotationVal = -rotationVal;

        attackCubesParent.transform.DORotate(new Vector3(0f, rotationVal, 0f), animRotateSpeed * 2, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        yield return new WaitForSeconds(changeInterval);

        StartCoroutine(ChangeRotation());
    }
}
