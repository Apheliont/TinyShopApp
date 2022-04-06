namespace TinyShop.Web.Models
{
    public record RatingModel
    {
        private int _currentRating = 0;
        public int UpperBound { get; set; } = 5;
        public int CurrentRating
        {
            get => _currentRating; set
            {
                if (value > UpperBound)
                {
                    _currentRating = UpperBound;
                }
                else
                {
                    _currentRating = value;
                }

            }
        }

        public void Reset()
        {
            _currentRating = 0;
        }
    }
}
