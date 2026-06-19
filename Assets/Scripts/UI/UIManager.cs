using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{ 
    public static UIManager Instance;

    [Header("Start UI")]
    public TextMeshProUGUI startText;

    [Header("TimeUI")]
    public TextMeshProUGUI timeText;

    [Header("CheckPointUI")]
    public TextMeshProUGUI checkpointText;

    private void Awake()
    {
        Instance = this;
    }

    // ==== Start UI ====
    public void ShowStartUI(bool show)
    {
        startText?.gameObject.SetActive(show);
    }

    // ==== Time UI ====
    public void UpdateTimeUI(float time)
    {
        if (timeText != null)
        {
            timeText.text = $"Time : {time:F1}";
        }
    }

    // ==== CheckPoint Count ====
    public void UpdateCheckPointUI(int count)
    {
        checkpointText.text = $"CheckPoint :  {count} ";
    }
}
