using Domain.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Clients : BaseEntity
    {

        /// <summary>
        /// Müşteri sistemindeki Kodu (API işlemlerinde kullanılacak, 32 Karakter Guid)
        /// </summary>
        public string DealerCode { get; set; }


        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string DealerName { get; set; }


        /// <summary>
        /// Müşteri tipi (1: Gerçek Kişi, 2: Tüzel LTD,AŞ, 3: Şahıs Firması)
        /// </summary>
        public int DealerType { get; set; }


        /// <summary>
        /// Müşteri Ünvanı
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Müşteri yetkili kişi ismi
        /// </summary>
        public string ContactName { get; set; }


        /// <summary>
        /// Müşteri Adres Bilgileri
        /// </summary>
        public string Address { get; set; }


        /// <summary>
        /// Müşteri İl
        /// </summary>
        public int City { get; set; }

        /// <summary>
        /// Vergi No
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Müşteri veya yetkili kişinin telefonu
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Müşteri veya yetkili kişinin email adresi
        /// </summary>

        public string Email { get; set; }


        /// <summary>
        /// Yetkili kişinin TC nosu
        /// </summary>

        public string IdentityNumber { get; set; }


        /// <summary>
        /// Müşteri web adresi
        /// </summary>

        public string WebSiteURL { get; set; }

       
        /// <summary>
        /// Ortak ödemede dönüş adresi
        /// </summary>
        public string CallBackAddress { get; set; }

        /// <summary>
        ///  Müşteri vergi levhasında yazan ana faaliyet kodu (6 hane)
        /// </summary>
        public string NaceCode { get; set; }
    }
}
