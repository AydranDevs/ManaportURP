using Manapotion.Items;

namespace Manapotion.PartySystem.Inventory
{    
    public class Bag
    {
        private Party _party;

        private BagScriptableObject _bagScriptableObject;

        public Bag(Party party)
        {
            _party = party;

            _bagScriptableObject = _party.bagScriptableObject;

            _bagScriptableObject.bagItemListChangedEvent.Invoke();
        }

        public void AddItem(Item item)
        {
            _bagScriptableObject.AddItem(item);
        }
    }
}
