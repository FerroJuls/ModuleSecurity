namespace Entity.Model.Security
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public bool Estado { get; set; }

        // Relación con State
        public int StateId { get; set; }
        public State state { get; set; }
    }
}
