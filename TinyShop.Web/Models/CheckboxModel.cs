namespace TinyShop.Web.Models
{
    public record CheckboxModel
    {
        public bool IsChecked { get; private set; }

        public void ChangeCheckboxState()
        {
            IsChecked = !IsChecked;
        }

        public void Reset()
        {
            IsChecked = false;
        }
    }
}
