namespace DataAccessLib.Models
{
    public record CheckboxType
    {
        public bool IsChecked { get; private set; }

        public void ChangeCheckboxState()
        {
            IsChecked = !IsChecked;
        }
    }
}
