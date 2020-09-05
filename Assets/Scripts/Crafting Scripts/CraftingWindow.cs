using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
	[Header("References")]
    [SerializeField] CraftingRecipeUI recipeUIPrefab;
    [SerializeField] RectTransform recipeUIParent;
    [SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

    [Header("Public Variables")]
    public ItemContainer itemContainer;
    public List<CraftingRecipe> craftingRecipes;

    public event Action<BaseItemSlot> OnItemPointerEnterEvent;
    public event Action<BaseItemSlot> OnItemPointerExitEvent;

    private void OnValidate()
    {
        Init();
    }

    private void Start()
    {
        Init();

        foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipeUIs)
        {
            craftingRecipeUI.OnPointerEnterEvent += slot => OnItemPointerEnterEvent(slot);
            craftingRecipeUI.OnPointerExitEvent += slot => OnItemPointerExitEvent(slot);
        }
    }

    private void Init()
    {
        recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
        UpdateCraftingRecipes();
    }

    public void UpdateCraftingRecipes()
    {
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            if (craftingRecipeUIs.Count == i)
            {
                craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
            }
            else if (craftingRecipeUIs[i] == null)
            {
                craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
            }

            craftingRecipeUIs[i].itemContainer = itemContainer;
            craftingRecipeUIs[i].CraftingRecipe = craftingRecipes[i];
        }

        for (int i = craftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
        {
            craftingRecipeUIs[i].CraftingRecipe = null;
        }
    }
}
