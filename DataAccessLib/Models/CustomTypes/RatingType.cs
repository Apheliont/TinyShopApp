using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Models
{
    public record RatingType
    {
        private int _currentRating = 0;
        public int UpperBound { get; set; }
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
