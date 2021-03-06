﻿using System;
using System.Collections.Generic;

namespace ArkuszDataBase.Models
{
    public partial class Plan
    {
        public int PlanId { get; set; }
        public DateTime? Miesiąc { get; set; }
        public decimal? ZalozonaKwota { get; set; }
        public decimal? FaktycznaKwota { get; set; }
        public int IdUzytkownika { get; set; }
        public bool? Active { get; set; }
        public int? Warning { get; set; }

        public virtual Uzytkownik IdUzytkownikaNavigation { get; set; }
    }
}
