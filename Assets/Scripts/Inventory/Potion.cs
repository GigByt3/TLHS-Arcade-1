public abstract class Potion : Item
{
    public Potion()
    {
        type = ItemType.POTION;
    }

    public abstract void onConsume();
}