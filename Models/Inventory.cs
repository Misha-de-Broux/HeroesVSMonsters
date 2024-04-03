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

        public int WeaponsCount { get { return _weapons.Count; } }

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
        public int Value {
            get {
                int value = 0;
                foreach(Weapon weapon in _weapons) {
                    value += weapon.Value;
                }
                foreach(LootType type in _inventory.Keys) {
                    value += ((int)type) * _inventory[type] / 100;
                }
                return value;
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

        public bool Add(Weapon? weapon) {
            bool isAdded = false;
            if(weapon is not null) {
                _weapons.Add(weapon);
                isAdded = true;
            }
            return isAdded;
        }

        public bool Remove(Weapon weapon) {  return _weapons.Remove(weapon); }



        public override string ToString() {
            if(Content.Count == 0 && _weapons.Count == 0) { return "\tnothing"; }
            string result = "";
            foreach (LootType type in Content) {
                result += $"\t{type} : {_inventory[type]}";
            }
            foreach (Weapon weapon in _weapons)
            {
                result += $"\n\t{weapon}";
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
