﻿using System.Xml.Serialization;

namespace Gateway.Core.Models.Garanti
{
    /// <summary>
    /// Prize type
    /// <para lang="tr">Ödül tipi</para>
    /// <para>Alfanumeric</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSRewardTypeEnum
    {
        [XmlEnum("")]
        Unspecified,

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("FBB")]
        FBB
    }
}