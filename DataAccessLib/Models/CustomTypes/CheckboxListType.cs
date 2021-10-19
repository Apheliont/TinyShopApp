using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLib.Models
{
    public record CheckboxListType
    {
        private Dictionary<string, bool> _checkboxesState = new Dictionary<string, bool>();
        public CheckboxListType(List<string> items)
        {
            if (items is not null)
            {
                foreach (string item in items)
                {
                    _checkboxesState.TryAdd(item, false);
                }
            }
        }

        public CheckboxListType() { }

        public List<string> GetCheckedItems()
        {
            return _checkboxesState
                .Where(kvp => kvp.Value == true)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public List<string> ItemNames => _checkboxesState.Select(kvp => kvp.Key).ToList();

        public bool ChangeCheckboxState(string name)
        {
            if (_checkboxesState.ContainsKey(name))
            {
                _checkboxesState[name] = !_checkboxesState[name];
                return true;
            }
            return false;
        }

        public bool IsChecked(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && _checkboxesState.ContainsKey(name))
            {
                return _checkboxesState[name];
            }

            throw new ArgumentException("Wrong checkbox name");
        }
        public void Reset()
        {
            foreach (string key in _checkboxesState.Keys)
            {
                _checkboxesState[key] = false;
            }
        }
    }
}
