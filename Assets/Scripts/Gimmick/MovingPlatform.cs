using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 DeltaPosition { get; private set; }

    private Vector3 previousPosition;

    [Header("ˆÚ“®æ‚Æ‚Ì‹——£")]
    [SerializeField] private Vector3 moveOffset = new Vector3(5f, 0f, 0f);

    [Header("ˆÚ“®‘¬“x")]
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 startPos;
    private Vector3 targetPos;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;

        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);

        Vector3 nextPos = Vector3.Lerp(startPos, targetPos, t);

        // ¡‰ñˆÚ“®‚·‚é—Ê‚ğŒvZ
        DeltaPosition = nextPos - rb.position;
        
        rb.MovePosition(nextPos);
    }
}
