using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤー操作全般を管理
    /// </summary>

    //変数宣言
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 3.0f;
    [Header("ジャンプ力")]
    [SerializeField] private float jumpForce = 5.0f;

    bool isGrounded = false;

    private Vector2 moveInput;
    private Rigidbody rb;

    /// <summary>
    /// 初期化
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        isGrounded = true;
    }

    /// <summary>
    /// 球を移動させる処理
    /// 今回は球なのでAddForceを使用
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // ★ 入力があるときだけ力を加える
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            rb.AddForce(moveDirection * moveSpeed);
        }

        // ==== 最大加速制限 ====

        // 現在の速度から横移動だけ抜き出す
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if(horizontalVelocity.magnitude> moveSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void ResetMovement()
    {
        moveInput = Vector2.zero;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Bounce(float power)
    {
        // 現在の速度を保持
        Vector3 vec = rb.linearVelocity;

        // Y座標だけ更新
        vec.y = power;

        rb.linearVelocity = vec;

        isGrounded = false;
    }
}
