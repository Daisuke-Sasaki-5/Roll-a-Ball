using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // プレイヤーリスポーン地点更新
            PlayerFallReset.instance.SetCheckPoint(transform);
            Debug.Log("チェックポイント変更");

            GameManager.instance.AddCheckPoint();
            Destroy(gameObject);
        }
    }
}
