using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.Models
{
    public class User
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsVerified { get; set; }

        public List<string> Groups { get; set; } = new List<string>();

        public bool Guest
        {
            get
            {
                return ID == null;
            }
        }

        public User()
        {
        }

        public User(string id)
        {
            ID = id;
        }
    }
}
