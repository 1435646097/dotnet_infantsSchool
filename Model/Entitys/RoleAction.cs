namespace Model.Entitys
{
    public partial class RoleAction
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ActionId { get; set; }

        public virtual Action Action { get; set; }
        public virtual Role Role { get; set; }
    }
}