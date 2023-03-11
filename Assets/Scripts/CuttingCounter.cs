using UnityEngine;

public class CuttingCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

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
      }
      else
      {
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObject())
    {
      // There is a KitchenObject here
      GetKitchenObject().DestroySelf();

      KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
  }
}