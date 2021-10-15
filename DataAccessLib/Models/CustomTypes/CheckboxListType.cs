using System.Collections.Generic;
using System.Linq;

namespace DataAccessLib.Models
{
    public record CheckboxListType
    {
        private Dictionary<string, bool> _checkboxesState = new Dictionary<string, bool>();
        public List<string> Names { get; init; }

        public void Init()
        {
            if (Names is not null)
            {
                foreach (string item in Names)
                {
                    _checkboxesState.TryAdd(item, false);
                }
            }

        }

        public List<string> GetCheckedItems()
        {
            return _checkboxesState
                .Where(kvp => kvp.Value == true)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public bool ChangeCheckboxState(string name)
        {
            if (_checkboxesState.ContainsKey(name))
            {
                _checkboxesState[name] = !_checkboxesState[name];
                return true;
            }
            return false;
        }
    }
}
