namespace Pugster
{
    public partial class RootController : DbController<RootDatabase>
    {
        public RootController(RootDatabase db) : base(db) { }
    }
}
