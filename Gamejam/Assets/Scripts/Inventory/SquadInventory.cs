public class SquadInventory : Inventory
{
    private static SquadInventory instance;

    public static SquadInventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SquadInventory>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
