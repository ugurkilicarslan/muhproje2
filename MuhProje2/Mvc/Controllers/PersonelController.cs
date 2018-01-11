using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class PersonelController : Controller
    {
        //
        // GET: /Personel/ anasayfa
        public ActionResult Index()
        {
            //boş bir emplist oluşturulur
            IEnumerable<mvcPersonelModel> empList;
            //apiye Personel olarak istek atılır gelen response data response a atılır
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Personel").Result;

            //data empliste yüklenir ve View e json döndürülür
            empList = response.Content.ReadAsAsync<IEnumerable<mvcPersonelModel>>().Result;
            return View(empList);
        }

        //kullanıcı ekleme veya editleme butonuna basıldığında bu fonksiyon çalışır
        public ActionResult AddOrEdit(int id = 0)
        {//aldığı int değerini 
            if(id==0)//eğer sıfır ise boş model yollanır
            return View(new mvcPersonelModel());
            else
            {//int değeri api ye yollanır ve gelen data response aktarılır
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Personel/"+id.ToString()).Result;
                //aktarılan data asenkron olarak okunur ve viewe json döndürülür
                return View(response.Content.ReadAsAsync<mvcPersonelModel>().Result);
            }
        }

        //kullanıcı editleme sayfasında post ettiğinde bu method çalışır
        [HttpPost]
        public ActionResult AddOrEdit(mvcPersonelModel emp)
        {//gelen modelin personel id si sıfır ise
            if (emp.Personelid == 0)
            {  //api ye datayla beraber yollanır
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Personel", emp).Result;
                //kullanıcıya başarılı mesajı yollanır
                TempData["SuccessMessage"] = "Kayıt İşlemi Başarılı";
            }//eğer gelen modelde personel id sıfır değilse
            else
            {//tekrardan api ye model id si ve yollanır ve güncelleme yapar
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Personel/"+emp.Personelid, emp).Result;
                //kullanıcıya güncellendi mesajı yollanır
                TempData["SuccessMessage"] = "Güncelleme İşlemi Başarılı";

            }//bu işlemler sonunda kullanıcı index sayfasına yollanır
            return RedirectToAction("Index");
        }
        //kullanıcı delete butonuna bastığında
        public ActionResult Delete(int id)
        {//personelin idsini get methodu ile alır
            //delete işlemi için api ye id yollanır
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Personel/"+id.ToString()).Result;
            //kullanıcıya silindi mesajı yollanır
            TempData["SuccessMessage"] = "Silme İşlemi Başarılı";
            //kullanıcı index sayfasına yönlendirilir.
            return RedirectToAction("Index");
        }
	}
}