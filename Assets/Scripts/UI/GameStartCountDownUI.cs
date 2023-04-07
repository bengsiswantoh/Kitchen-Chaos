using UnityEngine;
using TMPro;

public class GameStartCountDownUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;

  private void Start()
  {
    KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;

    Hide();
  }

  private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
  {
    if (KitchenGameManager.Instance.IsCountdownToStartActive())
    {
      Show();
    }
    else
    {
      Hide();
    }
  }

  private void Update()
  {
    countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}