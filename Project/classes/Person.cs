using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Classes.Person
{
    public class Person
    {
       //בוא נראה אם זה שומר שינוים
       /// <summary>
       /// /kjsdhfsdjkfhkdjh
       /// </summary>
        [DisplayName("קוד")]
        public string id { get; set; }
        [DisplayName("בחור/ה")]
        public string gender { get; set; }

        [DisplayName("שם")]
        public string name { get; set; }
        [DisplayName("גיל")]
        public string age { get; set; }

        [DisplayName("סטטוס")]
        public string status { get; set; }
        [DisplayName("עיר")]
        public string city { get; set; }
        [DisplayName("עבודה")]
        public string job { get; set; }
        [DisplayName("ישיבה/סמינר")]
        public string yeshivaOrSeminar { get; set; }

        [DisplayName("טלפון")]
        public string phone { get; set; }
        [DisplayName("הורים")]
        public string parents { get; set; }

        [DisplayName("מייל")]
        public string email { get; set; }
        [DisplayName("עדה")]
        public string eda { get; set; }
        [DisplayName("מוצא")]
        public string motsa { get; set; }
        [DisplayName("בירורים")]
        public string friends { get; set; }
        [DisplayName("פרטים")]
        public string details { get; set; }
        [DisplayName("פאה/מטפחת")]
        public string peaOrMitpachat { get; set; }
        [DisplayName("לומד/עובד")]
        public string learnOrWork { get; set; }

        [DisplayName("תמונה")]
        public string imageUrl { get; set; }


        public Person() { }
        
    }
}
