using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

  [SerializeField] BaseCounter baseCounter;
  [SerializeField] GameObject[] visualGameObjectArray;

  private void Start()
  {
    Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
  }

  private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeArgs e)
  {
    if (e.selectedCounter == baseCounter)
    {
      Show();
    }
    else
    {
      Hide();
    }
  }

  private void Show()
  {
    foreach (GameObject visualGameObject in visualGameObjectArray)
    {
      visualGameObject.SetActive(true);
    }
  }
  private void Hide()
  {
    foreach (GameObject visualGameObject in visualGameObjectArray)
    {
      visualGameObject.SetActive(false);
    }
  }
}
