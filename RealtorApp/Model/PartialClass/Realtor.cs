using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealtorApp.Model
{
    public partial class Realtor
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
