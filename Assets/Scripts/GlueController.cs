using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueController : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private Vector3 deltaMousePosition;
    private float motionTime = 0f;
    private Animator animator;
    bool isGlueSpawnable = true;
    [SerializeField] GameObject snapPoint,glueObject,spawnPoint,rayPoint;
    [SerializeField] float height;
    [SerializeField] Camera camera;
    [SerializeField] LayerMask maskRay;
    RaycastHit raycastHit;
    private ObjectPool objectPool;
    public bool isGlueOpened = false;
    int maxGlueParticle,glueParticleSpawned;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        objectPool = GameManager.instance.gameObject.GetComponent<ObjectPool>();
    }


    private void Update()
    {
        if (!isGlueOpened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition;
                deltaMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                var diffvec = Input.mousePosition - deltaMousePosition;
                deltaMousePosition = Input.mousePosition;
                motionTime += diffvec.x * Time.deltaTime / 100;
                motionTime = Mathf.Clamp01(motionTime);
                animator.SetFloat("MotionTime", motionTime);
            }
        }
        else
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var ray2 = new Ray(rayPoint.transform.position, rayPoint.transform.forward);
            Debug.DrawRay(rayPoint.transform.position, rayPoint.transform.forward, Color.red);
            if (Physics.Raycast(ray, out raycastHit,10,maskRay.value))
            {
                var diff = snapPoint.transform.position - raycastHit.point;
                Vector3 locationVector = transform.position - diff;
                if(Physics.Raycast(ray2,out raycastHit, 10,maskRay.value))
                {
                    locationVector.y = raycastHit.point.y +height;
                }
                
                transform.position = locationVector;
                isGlueSpawnable = true;
            }
            else
            {
                isGlueSpawnable = false;
            }
        }

    }

    IEnumerator SpawnGlues()
    {
        float duration = 0f;
        while (true)
        {
            duration += Time.deltaTime;
            if (isGlueSpawnable && duration >= 0.2f && Input.GetMouseButton(0) && glueParticleSpawned<= maxGlueParticle)
            {
                GameObject go = objectPool.GetPooledObject();
                go.SetActive(true);
                go.transform.position = spawnPoint.transform.position;
                duration = 0f;

                glueParticleSpawned += 1;
                GameManager.instance.IncreaseGlueSpawned();
            }

            if(glueParticleSpawned == maxGlueParticle)
            {
                GameManager.instance.GlueOver();
            }
            yield return null;
        }
    }

    IEnumerator ChangeIsGlueOpened()
    {
        isGlueOpened = !isGlueOpened;
        yield return null;
    }

    public void SetMaximumGlueParticleSpawn(int max)
    {
        maxGlueParticle = max;
    }
}
