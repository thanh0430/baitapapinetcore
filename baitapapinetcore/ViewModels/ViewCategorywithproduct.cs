namespace baitapapinetcore.ViewModels
{
    public class ViewCategorywithproduct
    {
        public int Id { get; set; }
        public string producttype { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public List<ViewProducts>? Products { get; set; }
    }
}
