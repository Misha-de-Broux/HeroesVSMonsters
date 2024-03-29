using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Inventory {
        private Dictionary<LootType, int> _inventory;
        private List<Weapon> _weapons;

        public Inventory() { 
            _inventory = new Dictionary<LootType, int>();
            _weapons = new List<Weapon>();
        }

        public int this[LootType type] {
            get {
                if (_inventory.ContainsKey(type))
                    return _inventory[type];
                else return 0;
            }
        }
        public Weapon? this[int i] {
            get {
                if(i < _weapons.Count) {
                    return _weapons[i];
                }
                return null;
            }
        }
        public ICollection<LootType> Content { get { return _inventory.Keys; } }

        public void Add(LootType type, int ammount) {
            if (_inventory.ContainsKey(type)) {
                _inventory[type] += ammount;
            } else {
                _inventory.Add(type, ammount);
            }
        }

        public override string ToString() {
            if(Content.Count == 0 && _weapons.Count == 0) { return "\tnothing"; }
            string result = "";
            foreach (LootType type in Content) {
                result += $"\t{type} : {_inventory[type]}";
            }
            return result;
        }

        public static Inventory operator +(Inventory receiver, Inventory items) {
            foreach (LootType itemType in items.Content) {
                receiver.Add(itemType, items[itemType]);
            }
            receiver._weapons.AddRange(items._weapons);
            return receiver;
        }
    }
}
