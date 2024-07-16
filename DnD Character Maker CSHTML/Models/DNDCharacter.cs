namespace DnD_Character_Maker_CSHTML.Models
{
    public class DNDCharacter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string creator { get; internal set; }
        public string classLevel { get; internal set; }
        public string race { get; internal set; }
        public string background { get; internal set; }

        public DNDCharacter(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
