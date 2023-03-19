using UnityEngine;

public class ClearCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      if (player.HasKitchenObject())
      {
        player.GetKitchenObject().SetKitchenObjectParent(this);
      }
    }
    else
    {
      if (player.HasKitchenObject())
      {
        // Player is holding a plate
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          GetKitchenObject().DestroySelf();
        }
        else
        {
          // Player is not carrying plate but something else
          if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
          {
            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            {
              player.GetKitchenObject().DestroySelf();
            }
          }
        }
      }
      else
      {
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }
}
