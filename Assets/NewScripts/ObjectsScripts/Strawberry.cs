using UnityEngine;

public class Strawberry : MonoBehaviour, IInteractible
{
    [SerializeField] private string prompt;
    [SerializeField] private bool shouldDisappear; 
    [SerializeField] private bool _bonusObj;

    public string InteractionPrompt => prompt;
    public bool bonusObj => _bonusObj;
    
    // Object interaction
    // Pick strawberry
    public bool Interact(Interactor interactor)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        
        if (inventory != null && playerStats != null)
        {
            inventory.AddStrawberry(1);
            playerStats.AddExperience(50);

            if (shouldDisappear)
            {
                Destroy(gameObject); 
            }
            return true;
        }
        else
        {
            Debug.LogError("Inventory not found in the scene.");
            return false;
        }
    }
}