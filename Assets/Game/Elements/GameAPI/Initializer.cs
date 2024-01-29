// Initializes all managers in the game API
[System.Serializable] public partial class GameAPI
{
    public void InitializeAPI()
    {
        inventoryManager.Initialize();
    }
}