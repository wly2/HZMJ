public class UIPanel_Recharge : UIWindow
{
    public void Buyclick()
    {
        GamePayManager.instance.BuyRoomCard(1);
    }
}