namespace Frame.Models.interfaces
{
    public interface IMenus
    {
        int Id { get; set; }
        string DisplayName { get; set; }
        int ParentId { get; set; }
        int Sort { get; set; }
    }
}
