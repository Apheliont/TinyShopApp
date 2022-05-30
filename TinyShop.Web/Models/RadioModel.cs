using System.Collections.Generic;

namespace TinyShop.Web.Models
{
    public class RadioModel
    {
        private List<string> RadioItems { get; set; }
        public string SelectedItem = "";
        public RadioModel(List<string> radioItems)
        {
            RadioItems = radioItems;
        }

        public List<string> ItemNames => RadioItems;

        public void Reset()
        {
            SelectedItem = "";
        }
    }
}
