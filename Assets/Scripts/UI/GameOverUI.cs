using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI recipesDeliveredText;

  private void Start()
  {
    KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;

    Hide();
  }

  private void KitchenGameManager_OnStateChange(object sender, System.EventArgs e)
  {
    if (KitchenGameManager.Instance.IsGameOver())
    {
      Show();

      recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeAmount().ToString();
    }
    else
    {
      Hide();
    }
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
