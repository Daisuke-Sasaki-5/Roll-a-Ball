using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 DeltaPosition { get; private set; }

    private Vector3 previousPosition;

    [Header("à⁄ìÆêÊÇ∆ÇÃãóó£")]
    [SerializeField] private Vector3 moveOffset = new Vector3(5f, 0f, 0f);

    [Header("à⁄ìÆë¨ìx")]
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;

        previousPosition = transform.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time *  moveSpeed, 1f);

        transform.position = Vector3.Lerp(startPos,targetPos, t);

        DeltaPosition = transform.position - previousPosition;
        previousPosition = transform.position;
    }
}
