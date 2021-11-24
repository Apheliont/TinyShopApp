using System;

namespace TinyShop.Helpers
{
    public class AppState
    {
        public event Action OnCartItemsCountChange;
        public void NotifyCartItemsCountChange() => OnCartItemsCountChange?.Invoke();
    }
}
