using UnityEngine;

public class PlayerFallReset : MonoBehaviour
{
    public static PlayerFallReset instance;

    [SerializeField] private Transform respawnPoint;

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

            if(rb != null )
            {
                // 速度リセット
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 位置リセット
                rb.position = respawnPoint.position;

                // 回転リセット
                rb.rotation = respawnPoint.rotation;
            }
        }
    }

    // チェックポイント変更時に呼び出す関数
    public void SetCheckPoint(Transform checkPoint)
    {
        respawnPoint = checkPoint;
    }
}
