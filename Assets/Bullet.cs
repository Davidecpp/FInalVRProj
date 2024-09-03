using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractible
{
    [SerializeField] private string prompt;
    [SerializeField] private bool shouldDisappear;
    
    public string InteractionPrompt => prompt;
    public bool bonusObj { get; private set; } = true;

    public bool Interact(Interactor interactor)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddBullet();
            Debug.Log("Bullet picked");

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