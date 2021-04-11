namespace Games.Api.Models.Category
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        internal static Category Create(Core.Entities.Categories.Category entity)
        {
            return new Category
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
