public class DeliveryCounter : BaseCounter
{
  public static DeliveryCounter Instance { get; private set; }

  public override void Interact(Player player)
  {
    if (player.HasKitchenObject())
    {
      if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
      {
        // Only accept plate
        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

        player.GetKitchenObject().DestroySelf();
      }
    }
  }

  private void Awake()
  {
    Instance = this;
  }
}
