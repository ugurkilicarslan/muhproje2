using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcPersonelModel
    {
        //model clasımız database de yer alan alanlar buraya aktarılmıştır
        //get ve set methodları oluşturulmuştur
        public int Personelid { get; set; }
        [Required(ErrorMessage="Bu alan gereklidir")]
        public string İsim { get; set; }
        public string Soyisim { get; set; }
        public string Meslek { get; set; }
        public Nullable<int> Yaş { get; set; }
        public Nullable<int> Maaş { get; set; }
    
    }
}