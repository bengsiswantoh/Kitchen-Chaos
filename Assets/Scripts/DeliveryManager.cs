using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
  public static DeliveryManager Instance { get; private set; }

  [SerializeField] private RecipeListSO recipeListSO;
  private List<RecipeSO> waitingRecipeSOList;
  private float spawnRecipeTimer;
  private float spawnRecipeTimerMax = 4f;
  private int waitingRecipeMax = 4;

  public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

      if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        // Has the same number of ingredients
        bool plateContentMatchesRecipe = true;
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        {
          // Cycling through all ingredients in the recipe
          bool ingredientFound = false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            // Cycling through all ingredients in the plate
            if (plateKitchenObjectSO == recipeKitchenObjectSO)
            {
              // Ingredient matches!
              ingredientFound = true;
              break;
            }
          }

          if (!ingredientFound)
          {
            // This Recipe  ingredient was not found on the Plate
            plateContentMatchesRecipe = false;
          }
        }

        if (plateContentMatchesRecipe)
        {
          // Player delivered the correct recipe!
          waitingRecipeSOList.RemoveAt(i);
          return;
        }
      }
    }

    // No matches found!
    // Player did not deliver the correct recipe!
  }

  private void Awake()
  {
    Instance = this;

    waitingRecipeSOList = new List<RecipeSO>();
  }

  private void Update()
  {
    spawnRecipeTimer -= Time.deltaTime;
    if (spawnRecipeTimer <= 0f)
    {
      spawnRecipeTimer = spawnRecipeTimerMax;

      if (waitingRecipeSOList.Count < waitingRecipeMax)
      {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        waitingRecipeSOList.Add(waitingRecipeSO);
      }
    }
  }
}
