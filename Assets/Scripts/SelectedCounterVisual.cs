using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

  [SerializeField] ClearCounter clearCounter;
  [SerializeField] GameObject visualGameObject;

  private void Start()
  {
    Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
  }

  private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeArgs e)
  {
    if (e.selectedCounter == clearCounter)
    {
      Show();
    }
    else
    {
      Hide();
    }
  }

  private void Show() { visualGameObject.SetActive(true); }
  private void Hide() { visualGameObject.SetActive(false); }
}
