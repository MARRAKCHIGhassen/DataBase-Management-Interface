namespace DataBase_Interface
{
    public class Article
    {
        public int Reference { get; set; }
        public string Designation { get; set; }
        public float Price { get; set; }

        public Article(int Ref, string Des, float P)
        {
            Reference = Ref;
            Designation = Des;
            Price = P;
        }
        public Article() : this(0, "", 0) { }
    }
}
