using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SlingShotHandler : MonoBehaviour
{
    [Header("LineRender")]
    public LineRenderer LEFTlineRenderer;
    public LineRenderer RIGHTlineRenderer;

    [Header("Transform Reference")]
    [SerializeField] private Transform LeftStartPosition;
    [SerializeField] private Transform RightStartPosition;
    [SerializeField] private Transform Centerposition;
    [SerializeField] private Transform Idealposition;
    [SerializeField] private Transform elasticTransform;

    [Header("SlingShot Stats")]
    [SerializeField] private float MaximumDistance = 3.5f;
    [SerializeField] private float shotForce = 5f;
    [SerializeField] private float timeBetweenBirdSpawn=2f;
    [SerializeField] private float elasticDivider = 1.2f;
    [SerializeField] private AnimationCurve elasticCurve;

    [Header("Scripts")]
    [SerializeField] private SlingShotAreaToShoot shotAreaToShoot;

    [Header("Birds")]
    [SerializeField] private AngryBird angryBirdPrefab;
    [SerializeField] private float angryBirdPositionOffset =2f;


    [Header("Sounds")]

    [SerializeField] private AudioClip elsasticpulled;
    [SerializeField]private AudioClip[]elasticRelesedClips;
    public AudioSource audioSource;
   

    private AngryBird spawnedAngryBird;

    private Vector2 SlingShotLinePosition;

    private Vector2 _direction;
    private Vector2 directionNormalized;



    private bool isDragging;
    private bool birdOnSlingShot;

    private void Awake()
    {
        audioSource =GetComponent<AudioSource>();

        LEFTlineRenderer.enabled = false;
        RIGHTlineRenderer.enabled = false;
        spawnAngryBird();
    }

    public void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame && shotAreaToShoot.isWithInSliceSlingShotArea())
        {
            isDragging = true;

            if(birdOnSlingShot)
            {
              //  SoundManager.instance.playclip(elsasticpulled, audioSource);

            }
        }

      
        if (Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingShot && isDragging)
        {
            if (GameManager.Instance.HasIneafShots())
            {

                isDragging = false;

                spawnedAngryBird.Launchbird(_direction, shotForce);

               //    SoundManager.instance.PlayRandomClip(elasticRelesedClips, audioSource);

                GameManager.Instance.UseShot();

                birdOnSlingShot = false;

                AnimateSlingShoot();

            //    SetLines(Centerposition.position);


                if (GameManager.Instance.HasIneafShots())
                {
                StartCoroutine(spawnAfterTime());
                }
            }
        }

      
        if (isDragging && birdOnSlingShot)
        {
            DrawSlingshot();
            PositionAndRotateAngryBird();
            
        }

    }

    #region SlingShotMethods
    private void DrawSlingshot()
    {
        
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        touchPosition.z = 0; 

       
        SlingShotLinePosition = Centerposition.position + Vector3.ClampMagnitude(touchPosition - Centerposition.position, MaximumDistance);

        SetLines(SlingShotLinePosition);

        _direction = (Vector2)Centerposition.position - SlingShotLinePosition;
        directionNormalized =_direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!LEFTlineRenderer.enabled && !RIGHTlineRenderer.enabled)
        {
            LEFTlineRenderer.enabled = true;
            RIGHTlineRenderer.enabled = true;
        }

        LEFTlineRenderer.SetPosition(0, position);
        LEFTlineRenderer.SetPosition(1, LeftStartPosition.position); // Left

        RIGHTlineRenderer.SetPosition(0, position);
        RIGHTlineRenderer.SetPosition(1, RightStartPosition.position); // Right
    }
    #endregion

    #region AngryBirdsMethods
    private void spawnAngryBird()
    {
        SetLines(Idealposition.position);

        Vector2 dir = (Centerposition.position - Idealposition.position).normalized;
        Vector2 Spawnposition =(Vector2)Idealposition.position + dir * angryBirdPositionOffset;

        spawnedAngryBird = Instantiate(angryBirdPrefab, Spawnposition, Quaternion.identity);
        spawnedAngryBird.transform.right = dir;

        birdOnSlingShot = true;
    }

    private void PositionAndRotateAngryBird()
    {
        spawnedAngryBird.transform.position = SlingShotLinePosition + directionNormalized * angryBirdPositionOffset;
        spawnedAngryBird.transform.right = directionNormalized;
    }

    private IEnumerator spawnAfterTime()
    {
        yield return new WaitForSeconds(timeBetweenBirdSpawn);

        spawnAngryBird();
        
    }

    #endregion


    #region AnimateSlingShot

    private void AnimateSlingShoot()
    {
        elasticTransform.position = LEFTlineRenderer.GetPosition(0);

        float dist = Vector2.Distance(elasticTransform.position,Centerposition.position);

        float time = dist / elasticDivider;

        elasticTransform.DOMove(Centerposition.position,time).SetEase(elasticCurve);
        StartCoroutine(AnimateSlingShotLines(elasticTransform, time));
    }

    private IEnumerator AnimateSlingShotLines(Transform trans, float time)
    {
        float elapstedTime = 0f;

        while (elapstedTime <time)
        {
            elapstedTime += Time.deltaTime;

            SetLines(trans.position);
            yield return null;
        }
    }
    #endregion
}
