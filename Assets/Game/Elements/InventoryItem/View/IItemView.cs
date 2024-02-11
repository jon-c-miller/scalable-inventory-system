/// <summary> Provides a way to use different types of view implementations to display items. </summary>
public interface IItemView
{
    void IUpdateEntryBasedOnItem(InventoryItem item);
    void IInitialize();
    IItemView Interface { get; }
}