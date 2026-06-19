using System;
using System.Collections;
using UnityEngine;

public class PlayerFallReset : MonoBehaviour
{
    public static PlayerFallReset instance {  get; private set; }

    // リスポーン位置の保存
    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// プレイヤーが落下時任意の場所にリスポーンさせる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Player player = other.GetComponent<Player>();

            if(rb != null )
            {
                // 一旦物理挙動を止める
                rb.isKinematic = true;

                // 速度リセット
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 位置リセット
                rb.position = respawnPosition;

                // 回転リセット
                rb.rotation = Quaternion.identity;

                if(player != null )
                {
                    player.ResetMovement();
                }

                // 次のフレームで物理再開
                StartCoroutine(ResetPhysics(rb));
            }
        }
    }

    private IEnumerator ResetPhysics(Rigidbody rb)
    {
        yield return null;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = false;
    }

    // チェックポイント変更時に呼び出す関数
    public void SetCheckPoint(Transform checkPoint)
    {
        respawnPosition = checkPoint.position;
    }
}
