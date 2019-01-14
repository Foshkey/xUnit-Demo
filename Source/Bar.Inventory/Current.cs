using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bar.Models;
using Newtonsoft.Json;

namespace Bar.Inventory {
    internal class Current : IInventory {
        private List<Drink> _currentInventory;

        public List<Drink> GetDrinks() {
            if (_currentInventory == null) {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("Bar.Inventory.Current.json"))
                using (var reader = new StreamReader(stream)) {
                    var serializer = new JsonSerializer();
                    _currentInventory = (List<Drink>)serializer.Deserialize(reader, typeof(List<Drink>));
                }
            }

            return _currentInventory;
        }
    }
}
