using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project.Classes.Person
{
    public class Person
    {
        [DisplayName("קוד")]
        public string id { get; set; }
        [DisplayName("מין")]
        public string gender { get; set; }

        [DisplayName("שם")]
        public string name { get; set; }
        [DisplayName("גיל")]
        public string age { get; set; }
        [DisplayName("גובה")]
        public string height { get; set; }

        [DisplayName("סטטוס")]
        public string status { get; set; }
        [DisplayName("עדה")]
        public string eda { get; set; }
        [DisplayName("מגזר")]
        public string migzar { get; set; }

        [DisplayName("מקום לימוד")]
        public string yeshivaOrSeminar { get; set; }
        [DisplayName("עיסוק")]
        public string isook { get; set; }
        [DisplayName("כיסוי ראש")]
        public string kisooyRosh { get; set; }
        [DisplayName("לומד/עובד")]
        public string learnOrWork { get; set; }
        [DisplayName("עבודה")]
        public string job { get; set; }
        [DisplayName("טלפון")]
        public string phone { get; set; }
        [DisplayName("טלפון בית")]
        public string homePhone { get; set; }
        [DisplayName("מייל")]
        public string email { get; set; }
        [DisplayName("טלפונים לבירורים")]
        public string friends { get; set; }
        [DisplayName("עיר")]
        public string city { get; set; }
        [DisplayName("רקע")]
        public string background { get; set; }

        [DisplayName("תמונה")]
        public string imageUrl { get; set; }

        public Person()
        {
            this.id = "";
            this.gender = "";
            this.name = "";
            this.age = "";
            this.height = "";
            this.status = "";
            this.eda = "";
            this.migzar = "";
            this.yeshivaOrSeminar = "";
            this.isook = "";
            this.kisooyRosh = "";
            this.learnOrWork = "";
            this.job = "";
            this.phone = "";
            this.homePhone = "";
            this.email = "";
            this.friends = "";
            this.city = "";
            this.background = "";
            this.imageUrl = "";
        }
    }
}
