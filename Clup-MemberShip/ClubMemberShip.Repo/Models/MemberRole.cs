﻿using System;
using System.Collections.Generic;

namespace ClubMemberShip.Repo.Models
{
    public partial class MemberRole
    {
        public int MembershipId { get; set; }
        public int ClubBoardId { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public Status? Status { get; set; }

        public virtual ClubBoard ClubBoard { get; set; } = null!;
        public virtual Membership Membership { get; set; } = null!;
    }
}
