using System.Collections.Generic;

namespace SkillProfiWebApplication.ViewModels
{
    public class ContactsPageModel
    {
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string ContactsImage { get; set; }

        public List<SocialNetwork> SocialNetworks { get; set; }
    }
}
