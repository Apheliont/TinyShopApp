using System;

namespace TinyShop.Web.Helpers
{
    public class AppState
    {
        public event Action OnCartItemsCountChange;
        public void NotifyCartItemsCountChange() => OnCartItemsCountChange?.Invoke();
    }
}
