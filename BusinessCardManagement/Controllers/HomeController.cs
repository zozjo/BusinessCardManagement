using BusinessCardManagement.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using ZXing;



namespace BusinessCardManagement.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {


        public ActionResult Index()
        {


            // var ssss = json["Name"].ToString();


            return View();
        }
        public ActionResult BusinessCard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReaderQrCode(string base64String)
        {
            var reader = new BarcodeReader();
            Bitmap bmpReturn = null;

            // string base64String = "iVBORw0KGgoAAAANSUhEUgAAASwAAAEsCAYAAAB5fY51AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAABmJLR0QAAAAAAAD5Q7t/AAAACXBIWXMAAABgAAAAYADwa0LPAAAVr0lEQVR42u2dzW7cxhKF6bv2vFNi561j56EE7+duEkCSe6Rin/rlfB/AhYEhu7pIFczTxdNf7vf7/QAAGMD/qgMAALBCwQKAMVCwAGAMFCwAGAMFCwDGQMECgDFQsABgDBQsABgDBQsAxkDBAoAxULAAYAwULAAYAwULAMZAwQKAMVCwAGAMFCwAGAMFCwDGQMECgDFQsABgDBQsABgDBQsAxkDBAoAxULAAYAwULAAYAwULAMZAwQKAMaQWrNvtdnz58qXFseLPP/9885vv37+7/m51ZIyhjKtez8ruuMqh5t4b67jd/44i+XK/3+9Zg91ut+PXr1/pk1yxmvbqBnj/LiMW7xwo17NS8fCfyUHGn4l13O5/R5HwSggAY6BgAcAYKFiv+Pbt25t///XXX7/95u+//96+3orVGBGxeMYcQdW4P3/+fPPviJxaqBp3HPdEvn79ej+O483x8vJSMq6VP/7447dzV4fC+zG+ffsmxfLPP/+8Oe/Hjx/bMT86V8mBdb5KfNYceM9NuW/WcSf+HXlRLrq/vLwct9stfVzrtBXh3EonET8jB96idtUChRKfMu7EvyMveCUEgDFQsABgDBSsD7AKoY+EcyueArsaS8a4lvmeoWqBYjc+EMgUzKxi4eEsjlrFwl1Re3WuIiSrsayw5sozB6vjTF6UnFYsUFhjVhcyuv8dRdJSdM8QKe+NRO0VVUK3dw4yYraeq8yjU567/x1FwishAIyBggUAY6BgnUTpTM+KZTe+DGE6S5yP/sogImYlV09DpmDWXSxcXc8qaq9QRGPvWCzxqXm2xuwtznvmwPqVgRqfcn+7/x1Fguj+ybhKejLEW4UMaxrr76xjeONtKeQ97oruf0eR8EoIAGOgYAHAGChYn/DefsTKIwHbcj3l3Kq8WGOu6th/hLIYodyPikWaS5ApmHUXCy3XV61fKo5HMa9+623LYj28xfmMLnkl91f+O4oE0X1jXOXcKqwxV80t435YH3Xv+Xr/iXX/O4qEV0IAGAMFCwDGQMF6hWd3dCcUIbm713hGt7pKp8WS8WQKZs/sRV2N96LAbtf9GfsW67jeXei7WBco1Pie+e+opeieMW7itFvQSUjO2LC26v5mdPE/898Rr4QAMAYKFgCMgYL1JHReFLgS5DmYTMFsJdpVHSu8rUY6/07dgFSJz3o97991yrMybve/o0jKRfcq7gkiL7/jdxG/6/53FAmvhAAwBgoWAIyBgvUKpTta6RDv4reukmGZsuoat45b8bsz1jRYzhhIV80uwJEgUlo7063xZfxOyd9unr03rPUW4q1HVXf+NFJF96vgbduhjHFvJAZb6eT9npErK/wpfg6vhAAwBgoWAIyBgrXBbjezVUQ9I7BHi9DefubKYsSZHOyOEZErcCRTMKvyyvYUTL1FXu+je3yPDguKv3yEqO2dK8sYGd70WZvJ7pAquldZnHQXeb3pHp8ScydRO+N5rvKm947FC14JAWAMFCwAGMNTFKxdYXWF0n2cJcpmCOKeZPmte3ure1rJeHvTK/cywiffjUzB7FgIfBZv8AiLDm8BURFglRxYDrUr22o5s+vz7n0/VG/13VjO3CPFmz7Dn7+DwL6iXHS3Dj+xe9uKkgNljKqNSq1zy7gf3nOr+kJBIeP+evEUr4QAcA0oWAAwhtSClSHa0ZH8OxGLDN73Mvp6U4ne2La1wL6iWkSzcmx2R9/vmlWL4lNuPZQcHAkiavQCRYagq27gakW558ozVPVsZDPGXubKQmineVjHVR6bKkE3Y0HG+x554z3fbNCwAGAMFCwAGMPogmXtXI4Wl1Uh1DIP6xgTFhmqBN2KRZ8VE+5RW6pFtGhRu9JWxNohrhwWEfXRYcV6boY9Twa7882wflGfK2W+HSgX3TuJ2krM1nGrupSVvHSy58mgkzi/wjv3iO4AAAFQsABgDGMK1kRbkaouZW88xeo2HdMPyBC/M6xflHNb36NqEe1wFPwiRO33x0pYzRLTlZxa86wIydZ5ZHj7e1u1KM+ut/VQxlcLXRdQxojuyvW8uReJ6d45sM7DGstEe5SJX0F0ukfZ5WPMKyEAAAULAMZQWrAqN+ncRRE9s6jotq7s1PbcTHaFmqvojXefikzBrGqTTkXAjuaM7YklpxHd1t7n7t4j1atdwfvrAQVlXOUeVc33NeWe7hkkTnGLKuuXjI5967kZMSt0EqER3QEABkDBAoAxXL5gTRAulU5jz3OVmFdEdFZXLW5Eb7Krsmu1NGER6Q2ZgtkRLLY+up7Stbu7UJCxMat3h7O6ALA77pViziDj+XtPRJ53KBfdrcN3EoitKKntZBHjPY8rx5xBp4WH7Lxc/pUQAK4DBQsAxnCpgjVOQHyA4t/ejaoFhaqYoznTYe9tq9SCTMHscO7Q3bXeUDfVtB5Wdj3iI3LvOY+szVqtMXfaENbb/mb3iMhzJKNF97uz5Yf1d1aUuSnXs46h5EUZo+rcTuJ8lRXPiox75MWlXgkB4NpQsABgDKkFK1qAtdrVdLMGqVoUsObFu4t6N76IuSlEz7fb5qotFiPSVTNHvAVJC48Ee88O+zOLAtEb0VpjVlHG9b7nGX7mnvdj9fypm6t27fYv93RXqPJR7+4XrsTinSsrnb5GqFoEUah6/rJBwwKAMVCwAGAMFKxB7HqXdxNvV1TZt1QJ3d4LLbvzGLe5arWIpnA4C5LKuEp8u+eq3dGKtc/uuFmCvSJCK/dtV6zO2AT40T3f7bp/yo1UFTr5ilvHUOLLOLdqvgqdFh4yLJSqYu4gxPNKCABjoGABwBhGF6wKX3HvLnlFzMzoJPf+eqBSvI0WuldjRDxXuyhfhyhzcyVTMKsS9zKsPLw5jIKp9dyqwxpzhr3MLuoGrt5fZFgXFHa/vjgzt2xa2stU2YBU+bcrMVvPrUKJOeNcK1Wd+MoY3Z/xHUa/EgLAc0HBAoAxlBesDHEvuhvcW3xUYpnQ1b6iu/e7VbCP7pxXF5GUhYcWHvGZgtmRIO5lCOyeYuaZQ5mHItS+p9K6pOpcJQfKc6UQ3XV/+U737t7lVryvlzGu9Vxr7juNm3GukgMr3n+KVfc3kvJXQgAAKxQsABhDqaf7IxRxr8puJdqXXRFRq0RotbPaMm6lOO95zzPukfc+A5fvdN8V9w5R8Kvwfj8z34xu8BWec/POfZWYrnDmefa0eblK/iyU28t02qjUe1xrLMo8qmJRxrBSlZeM+d4TutUn5u8z0LAAYAwULAAYQ3nByui87SKOdutC79QhvqJFZ3UQ0V94dPtSwI1qEe093h3EZzYqPYzi6IoMaxrruVUialVn9W5ezowRvWGtaqejiPie9zKactF9RfdNJr1jVsawzi3jNnfvnO80hnVca668n4OuQnz5KyEAgBUKFgCMoWXB6rLJZEbMEb7sFSKqdwf7CsVaJWtj0ejnQPXY9/TxLyFTMOu+yeTq2EXx2VYE3Sxfcc8jQgz23ExWIcNvfcJ986LcXsY6fJVPuZIeb3G0k6+4N8pz4J0rb6o2es0gsXwcx9H0lRAAYAUFCwDGMKZgRdu3PGJXaIwQRz2F6al4it9ZHd1VlkfRXN5e5nDs1FatWqzX63JkdClHfAFgHaMTGTYv0YsCEzznd2gpunfacLUT1rl19/Lu2kV9Nr6MRZBOz3OHezTmlRAAgIIFAGMo9XTP6Fy+EtG+55U5bdFFHZCDKz27Le5RtYhmxSpSWs71tgY5GgmmWZ3u0XlWbV68c2Cdh3cHe8bzbN1k13qPImlpL7OiuzWINRbv+So58J5bJ5uXFRk59c59pzxbz40EDQsAxkDBAoAxjClYSqf2KM9qh/l2idnbkzzDwiYiFu972eV5vnyn+24H8ZlOd4XDWaSM5sy4lnmoYyh5jrZbUQXi6A1hK59n5R5lQ6f7J9dbkRGLErN3fFUbuF55EUTJi3fMnZ7nzxjzSggAQMECgDGUFyxvT3JvO5grU+HvPTXPV7E3qojFlUzB7HAWRy3XizissVhRureV+Dod1pirFgosqON2z73yt+pFuei+whpSJ5/3KlH7Kn7hnXKqcJV9C+h0BwAQoWABwBjaFawzAnv3bnBFpPQWODvnKsKCpdNmst3x3j8glEzB7HDscK70ebfOzRJzxLm7VPqAZ9gHZXiwK/cjOs+dbWOslIvu90b2I51i7jRfBWssneyDlHMzcpAxRmJZOEW7V0IAgEdQsABgDCM83VcofvDeMZ8RW73tRzznm+Fnbp2H9XrdN6LNWESa4NnvRrWI5unRHSG27l7PekRY53j7t1vtdHYF9ozcZ2xEewbvMTI8+ztQ7uneqctbGVfBGvOV48uYW0Z8Vq6yWJINGhYAjIGCBQBjKC9YimicIeLvXq9TXhS8u5m9O9jP0MIe5Zhh2dMlV7+RKZhVCOzegq7SYZ8hpmd1KVvnoXSw76J2dO/OLWsT24qjS/d7y0535dwMQVcZtyo+b6oWPJT4rONm7D0wkcRS8ZDyV0IAACsULAAYQ8tOd+XcDLFa8aGPji/L8qNqwcMT5R6dEbo7W/tYabP5cLWI1rnTfaodRydxfjdmNb7VnC1WRqpwbkH5UkBd4LEeSq4iodM98NwqrhKzEl+VcO797HrPzYqSq0jQsABgDBQsABhDacHK6tCNFn7bCJIn5nscvbqZK2yBvMettHl5fy+VMbo9z2/IFMy8u54t18oSb3dz4G0rMmHxIGOhxftc5TmoeO7PHNZ5KDF7Ud7p/qCIllzPOsbE63US4jtZ01Sdu8L7elYycuAFGhYAjIGCBQBjaFmwrGJwhric4ZkeHZ9VgO3WJW9l93mJ+NLCcq6K9/U6Lb58SqZgdgTbXVSKy9E+76sx1K7s3U1sI+YW/QxF2BHtXu+MzZByvd3u94xNe3dpKbpbuTcSlzv5qFdtGKrMzUr37u2MDvtOz0Zi+TiOo+krIQDACgoWAIxhbMHqJi5nCKFXtnSx4unpXrUIUvk7C3S6fyDaKZt0VojL3oJ9ty5073u0ItqfX7VW8c59B1uW/1Du2+rcbMpF93sjAdGKd8omdqFfxZ/fipL7DmK1Rywd5jH2lRAAng8KFgCMoaWnu6e4fBzXsN5oLYQG0am7vMvXEiqdYtkiXTV7h6cg6S22HkbBXhWNd8ddHRGxKL/zPtdbiPfuBs+gSsRX7psXoz3drddTuD9Zl3In0b3q3E6LIN45mDjua9CwAGAMFCwAGMPlCpan2PqMXcrem4h6zrdq3BUI50Wkq2YNhbyPiO7KVnOV0aWseJJ7W+JkPC9KnivsaiLGteYlm8uJ7lXxZcyjk1i9QslL9y8PMvKsXK/78+fF5V4JAeC6ULAAYAwtN1Lt5DFdIZyrsUSf+4hoS5wV3nlWn8mKPQAiNma1xpJOpmAW7RduPa6+YWj0GN26wT2/AIh4rqqe+4xYsim3l6kiY9qdxNuM+FZU5dkaS6d9Brypmm8kaFgAMAYKFgCModReporKzmCLeFu5GKHE955OHdjeYj8U3d901ewV3pYfq3Mf4dmBfcaqpeLIis+aeyve9jyenNkMteq+7+ZK9eyPZEynu7fI29033puM+LwfpU5d3t7xZaDkqkNX+wo0LAAYAwULAMYwpmB5bw56hU1JrURYxETn4MzCQ5XdSnQXv0pFR3w41SLa4dw9m+F7rsztKBIzdzedrdo01XpkfAGQ8QxF7EdgPVZ4/116MUZ0V6634p4gNF65G9wa81XsZazjdtr81UrG34IXY14JAQAoWAAwhpYFy3PTyhVZFjGdBPuqvER7xGfY+JwZt9Pmr545aLOwVC2iHc7d21Y6icGdfLszvgDYXQBQY+50rgX1SxDr7ybRUnS3ooTeSQzu1H3c6QsA7/x1OtdKpw1wO9DylRAAYAUFCwDG0NLT3cIEr2xvO5guvvFn8rKb027d6t7nWlCf093ftSZTMFP8pJVucG8f6137mzP2KLtzqxTJPXN6Zm7W/GXkSqHLngfWI8uz/zVjPN2VMDv5dlvPtc63k0iuoOQl43fWmBU62RFZSSwfx3GgYQHAIChYADCGEZ7uGR3naixduqOVXGXY0Dyik22MhU6xPFUO0lWzppyxUZnYWb07t0fH7iJIxCasq/O9f7ebq4h7pOQ5Y7+ESMo73TuR0S3c/dwMwbmTP7/37zLmYaXTPfcCDQsAxkDBAoAxULBeEW2FonQaZ53byeLEm10Pdu+FjEq6f3nwKZmC2devX8u7c/87rFhFaOXcLsdKID5jSaJ0piv3yPP+VonkZ+bhnWfveUSSKrrfbrfj169fWcN9iHXa3TvEvbHOw/t31lhWdF/IsFKV5xWJZeEUvBICwBgoWAAwBgrWJygi9EQBW7HYqRKrq74U6L6B6y5tBPYVmYLZSnR/eXkpGXeXK/hifzQP66FwJIi8Gf73lnFXR4TNkHeeqyx2PqNcdH95eTlut1v6uMq0p/tifzQPK93zV3WPum/ka71e1+53XgkBYAwULAAYAwXrJJfwxf4XZVFgd75Z+buKNY13nj3Pvby9jFV0P5zFYEV077yRqnJY41PF+ej4zuQvg9VcrEJ3Rl6V56pD93tL0d1bDFZE9+4bqSpk5KAqvsTH2i2WTt751nOz4ZUQAMZAwQKAMVCwPiBro9dd2xMFa3xZnu4rJi5k7JKRZ/W5anE/MgWz7qK7t/gY7d8e4ce9m4Mznvi786jciNaKNZbozX0VInz3vUB03xjXmrKJHuydNoStGlf5k+i+gKLMI2Pcz+CVEADGQMECgDFQsDawio9drEuUcc9cr8r2pEuenw063f/lEMRH67grdsdUxcdOvuJVeM9DsUfxvh+78z2zqJIxjw7PFaJ74LhWOonGVWSI3yusz4H3/chYVLnic8UrIQCMgYIFAGOgYDmgiI9qh/NVROOrzKNivp02/A0nUzDr7ul+GAXOaDH4EEXUTn7c0d3+j7DeS+u5yrgK1uevYsNfOt0Tx70X2WxkiKidupQ7bXJqzUEnsbpKnLeSWD6O4+CVEAAGQcECgDFQsE6iisGdOskzRNTdmFXhdzfPExZBVjnwfK5akymYrcTvqmPF6ncWgTNCfMwQnN8f3l7ynbzprXk+87zsLoJ438tK26JsykX3Ku7OorZ3Gqu8wa3zrbqedQwrVYsg3vdSuZ5yjxLLx3EcvBICwCAoWAAwBgrWK3a91SOEVU/LFCveomwnb/pHZCyCRN/Lqv0DKr5GSNWwAAAU+B8WAIyBggUAY6BgAcAYKFgAMAYKFgCMgYIFAGOgYAHAGChYADAGChYAjIGCBQBjoGABwBgoWAAwBgoWAIyBggUAY6BgAcAYKFgAMAYKFgCMgYIFAGOgYAHAGChYADAGChYAjIGCBQBjoGABwBgoWAAwBgoWAIyBggUAY6BgAcAY/g9Lp2SpHtozvAAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAyMC0wMy0wMlQwODo1NDoyMSswMDowMO6HFqQAAAAldEVYdGRhdGU6bW9kaWZ5ADIwMjAtMDMtMDJUMDg6NTQ6MjErMDA6MDCf2q4YAAAAAElFTkSuQmCC";
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);


            memoryStream.Position = 0;


            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);


            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;
            var result = reader.Decode(bmpReturn);
            string resultText = "false";
            if (result != null)
            {
                resultText = result.Text;
                resultText = resultText.Replace("[", "");
                resultText = resultText.Replace("]", "");
                if (resultText.Contains("Name") && resultText.Contains("Email"))
                {
                    BusinessCard business = new BusinessCard();
                    JObject json = JObject.Parse(resultText);
                    business.Name = json["Name"].ToString();
                    business.Address = json["Address"].ToString();
                    business.Email = json["Email"].ToString();
                    business.Phone = json["Phone"].ToString();
                    business.Gender = json["Gender"].ToString();
                    business.DateOfBirth = json["DOB"].ToString();
                    return Json(business);
                }

                else
                {
                    return Json("flase");
                }


            }
            return Json(resultText);



        }
        [HttpPost]
        public ActionResult ReadXML(HttpPostedFileBase postedFile)
        {
            List<BusinessCard> customers = new List<BusinessCard>();
            string filePath = string.Empty;
            XmlDocument doc = new XmlDocument();
            BusinessCard businessCardXML = new BusinessCard();
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
                string openThis = "~/Uploads/" + postedFile.FileName;
                //Read the contents of CSV file.

                doc.Load(Server.MapPath(openThis));

                foreach (XmlNode node in doc.SelectNodes("/BusinessCard"))
                {

                    businessCardXML.Name = node["Name"].InnerText;
                    businessCardXML.Gender = node["Gender"].InnerText;
                    businessCardXML.DateOfBirth = node["DOB"].InnerText;
                    businessCardXML.Email = node["Email"].InnerText;
                    businessCardXML.Phone = node["Phone"].InnerText;
                    businessCardXML.Photo = node["Photo"].InnerText;
                    businessCardXML.Address = node["Address"].InnerText;

                }
            }




            return Json(businessCardXML, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ReadCSV(HttpPostedFileBase postedFile)
        {
            List<BusinessCard> customers = new List<BusinessCard>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        customers.Add(new BusinessCard
                        {
                            Name = row.Split(',')[0],
                            Gender = row.Split(',')[1],
                            DateOfBirth = row.Split(',')[2],
                            Email = row.Split(',')[3],
                            Phone = row.Split(',')[4],
                            Photo = row.Split(',')[5],
                            Address = row.Split(',')[6],

                        });
                    }
                }
            }

            return Json(customers, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InsertBusinessCard(string Name, string Gender, string DateOfBirth, string Email, string Phone, string Photo, string Address)
        {
            BusinessCard businessCard = new BusinessCard();
            DBContext myDB = new DBContext();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            businessCard.UserID = userId;
            businessCard.Name = Name;

            businessCard.Gender = Gender;
            businessCard.DateOfBirth = DateOfBirth;
            businessCard.Email = Email;
            businessCard.Phone = Phone;
            businessCard.Photo = Photo;
            businessCard.Address = Address;

            myDB.businessCards.Add(businessCard);
            myDB.SaveChanges();
            return Json("Done");

        }

        [HttpPost]
        public JsonResult GetBusinessCard()
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            DBContext myDB = new DBContext();
            List<BusinessCard> businessCards = new List<BusinessCard>();
            businessCards = (from obj in myDB.businessCards
                             where obj.UserID == userId
                             select obj).ToList();
            return Json(businessCards, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBusinessCardFilter(string Filter)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            DBContext myDB = new DBContext();
            List<BusinessCard> businessCards = new List<BusinessCard>();
            businessCards = (from obj in myDB.businessCards

                             where obj.UserID == userId && (obj.Name.Contains(Filter) || obj.Phone.Contains(Filter) ||
                             obj.Gender.Contains(Filter) || obj.DateOfBirth.Contains(Filter) || obj.Email.Contains(Filter))
                             select obj).ToList();
            return Json(businessCards, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteCard(int CardID)
        {

            int userId = int.Parse(HttpContext.User.Identity.Name);
            DBContext myDB = new DBContext();
            BusinessCard obj = new BusinessCard();
            obj = (from businessCard in myDB.businessCards
                   where businessCard.BusinessCardID == CardID && businessCard.UserID == userId
                   select businessCard
                ).FirstOrDefault();
            if (obj != null)
            {
                myDB.businessCards.Remove(obj);
                myDB.SaveChanges();
                return Json("true");
            }
            else
            {
                return Json("false");
            }
        }

        public FileContentResult ExportBusinessCardToCSV(int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            DBContext myDB = new DBContext();
            BusinessCard obj = new BusinessCard();
            obj = (from businessCard in myDB.businessCards
                   where businessCard.BusinessCardID == id && businessCard.UserID == userId
                   select businessCard
                ).FirstOrDefault();
            if (obj != null)
            {

                StringWriter sw = new StringWriter();
                sw.WriteLine("\"Name\",\"Gender\",\"DOB\",\"Email\",\"Phone\",\"Photo\",\"Address\"");
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                                              obj.Name,
                                              obj.Gender,
                                              obj.DateOfBirth,
                                              obj.Email,
                                              obj.Phone,
                                              obj.Photo,
                                              obj.Address));
                var fileName = "Business Card" + obj.Name + ".csv";
                return File(new System.Text.UTF8Encoding().GetBytes(sw.ToString()), "text/csv", fileName);

            }
            return File(new System.Text.UTF8Encoding().GetBytes(""), "text/csv", "notFound.csv");


        }

        public void ExportToXML(int id)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            DBContext myDB = new DBContext();
            BusinessCard data = new BusinessCard();
            data = (from business in myDB.businessCards
                    where business.BusinessCardID == id && business.UserID == userId
                    select business
            ).FirstOrDefault();
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(stream, System.Text.Encoding.ASCII);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("BusinessCard");
                xmlWriter.WriteElementString("Name", data.Email);
                xmlWriter.WriteElementString("Gender", data.Gender);
                xmlWriter.WriteElementString("DOB", data.DateOfBirth);
                xmlWriter.WriteElementString("Email", data.Email);
                xmlWriter.WriteElementString("Phone", data.Phone);
                xmlWriter.WriteElementString("Photo", data.Photo);
                xmlWriter.WriteElementString("Address", data.Address);

                // End the element WebApplication
                xmlWriter.WriteEndElement();

                // End the document WebApplications


                // Finilize the XML document by writing any required closing tag.
                xmlWriter.WriteEndDocument();

                // To be safe, flush the document to the memory stream.
                xmlWriter.Flush();

                // Convert the memory stream to an array of bytes.
                byte[] byteArray = stream.ToArray();

                // Send the XML file to the web browser for download.
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "filename=MyExportedFile.xml");
                Response.AppendHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(byteArray);
                xmlWriter.Close();

            }
        }





        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }



    }

}