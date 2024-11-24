namespace SkillProfiWebApplication
{
    public class SocialNetwork
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public SocialNetwork(string Name, string Link)
        {
            this.Name = Name;
            this.Link = Link;
        }
    }
}
