using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
  public static OptionsUI Instance { get; private set; }

  [SerializeField] private Button soundEffectsButton;
  [SerializeField] private Button musicButton;
  [SerializeField] private Button closeButton;
  [SerializeField] private TextMeshProUGUI soundEffectsText;
  [SerializeField] private TextMeshProUGUI musicText;

  private void Awake()
  {
    Instance = this;

    soundEffectsButton.onClick.AddListener(() =>
    {
      SoundManager.Instance.ChangeVolume();
      UpdateVisual();
    });

    musicButton.onClick.AddListener(() =>
    {
      MusicManager.Instance.ChangeVolume();
      UpdateVisual();
    });

    closeButton.onClick.AddListener(() =>
    {
      Hide();
    });
  }

  public void Show()
  {
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }

  private void Start()
  {
    KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

    UpdateVisual();

    Hide();
  }

  private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
  {
    Hide();
  }

  private void UpdateVisual()
  {
    soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
  }
}