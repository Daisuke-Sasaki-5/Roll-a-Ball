using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("エフェクト")]
    [SerializeField] private GameObject effectPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // プレイヤーリスポーン地点更新
            PlayerFallReset.instance.SetCheckPoint(transform);
            Debug.Log("チェックポイント変更");

            GameManager.instance.AddCheckPoint();

            // エフェクト生成
            if(effectPrefab != null )
            {
                Instantiate(effectPrefab, transform.position + Vector3.up * 1, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
