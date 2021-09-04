using Newtonsoft.Json;

namespace DataAccessLib.Models
{
    public record BreadcrumbItemModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }

        // Если в jsone нет поля Uri и не указать декаратор то получим рантайм ошибку
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uri { get; set; }
    }
}
