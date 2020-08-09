using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Limits : BaseEntity
    {
        public Clients Client { get; protected set; }
        [ForeignKey("ClientId")]
        public virtual int ClientId { get; protected set; }

        /// <summary>
        /// Bu Müşteri için 3D ödeme zorunlu mu ? (True, False)
        /// </summary>
        public virtual bool IsThreeDRequired { get; protected set; }


        /// <summary>
        /// Günlük ödeme yapma tutar limiti (TL cinsinden)
        /// </summary>
        public virtual decimal DailyTrxAmountLimit { get; protected set; }

        /// <summary>
        /// Günlük ödeme yapma adet limiti
        /// </summary>
        public virtual int DailyTrxNumberLimit { get; protected set; }


        /// <summary>
        /// Günlük Non-3D ödeme yapma tutar limiti(TL) – 3D zorunluysa
        /// </summary>
        public virtual decimal DailyTrxAmountLimitNon3D { get; protected set; }


        /// <summary>
        ///  Günlük Non-3D ödeme yapma adet limiti – 3D zorunluysa
        /// </summary>
        public virtual int DailyTrxNumberLimitNon3D { get; protected set; }

        /// <summary>
        /// Her bir ödeme işlemi tutar limiti (TL cinsinden)
        /// </summary>
        public virtual decimal EachTrxAmountLimit { get; protected set; }


        /// <summary>
        ///  Her bir Non-3D ödeme işlemi tutar limiti(TL) – 3D zorunluysa
        /// </summary>
        public virtual decimal EachTrxAmountLimitNon3D { get; protected set; }

        /// <summary>
        /// Günlük aynı karttan ödeme yapma tutar limiti(TL cinsinden)
        /// </summary>
        public virtual decimal DailyCardAmountLimit { get; protected set; }


        /// <summary>
        /// Günlük aynı karttan ödeme yapma adet limiti
        /// </summary>
        public virtual int DailyCardNumberLimit { get; protected set; }

        /// <summary>
        /// Günlük aynı karttan ödeme yapma adet limiti(sadece alarm oluşur)
        /// </summary>
        public virtual int DailyCardNumberAlertLimit { get; protected set; }

        /// <summary>
        /// Aylık ödeme yapma tutar limiti (TL cinsinden)
        /// </summary>
        public virtual decimal MonthlyTrxAmountLimit { get; protected set; }

        /// <summary>
        /// Aylık ödeme yapma adet limiti
        /// </summary>
        public virtual int MonthlyTrxNumberLimit { get; protected set; }

        /// <summary>
        /// Aylık Non-3D ödeme yapma tutar limiti(TL) – 3D zorunluysa
        /// </summary>
        public virtual decimal MonthlyTrxAmountLimitNon3D { get; protected set; }

        /// <summary>
        ///  Aylık Non-3D ödeme yapma adet limiti – 3D zorunluysa
        /// </summary>
        public virtual int MonthlyTrxNumberLimitNon3D { get; protected set; }
    }
}
