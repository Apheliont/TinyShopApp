using System;
using System.Globalization;

namespace TinyShop.Web.Models
{
    public class RangeModel<T>
    {
        private T _from = default(T)!;
        private T _to = default(T)!;
        public T LowerBound { get; set; } = default!;
        public T UpperBound { get; set; } = default!;
        public T From
        {
            get => _from;
            set => _from = value;
        }

        public T To
        {
            get => _to;
            set => _to = value;
        }

        public string FromAsString
        {
            get {
                switch (typeof(T))
                {
                    case Type t when t == typeof(double):
                        {
                            return ((double)(object)_from).ToString("0.0", new CultureInfo("en-US"));
                        }
                        
                    default:
                        {
                            return _from!.ToString()!;
                        } 
                }
            }
            set
            {
                try
                {
                    switch (typeof(T))
                    {
                        case Type t when t == typeof(int):
                            _from = (T)(object)Int32.Parse(value);
                            break;
                        case Type t when t == typeof(double):
                            _from = (T)(object)Double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                    }

                }
                catch
                {
                    _from = LowerBound;
                }
            }
        }

        public string ToAsString
        {
            get
            {
                switch (typeof(T))
                {
                    case Type t when t == typeof(double):
                        {
                            return ((double)(object)_to).ToString("0.0", new CultureInfo("en-US"));
                        }

                    default:
                        {
                            return _to!.ToString()!;
                        }
                }
            }
            set
            {
                try
                {
                    switch (typeof(T))
                    {
                        case Type t when t == typeof(int):
                            _to = (T)(object)Int32.Parse(value);
                            break;
                        case Type t when t == typeof(double):
                            _to = (T)(object)Double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                    }

                }
                catch
                {
                    _to = UpperBound;
                }
            }
        }
        public void Reset()
        {
            _from = LowerBound;
            _to = UpperBound;
        }
    }
}
