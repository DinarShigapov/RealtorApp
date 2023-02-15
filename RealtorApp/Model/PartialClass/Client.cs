using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtorApp.Model
{
    public partial class Client
    {
        public string StrFullName
        {
            get 
            {
                return $"{MiddleName} {FirstName.ToCharArray()[0]}. {LastName.ToCharArray()[0]}.";
            }
        }
    }
}
