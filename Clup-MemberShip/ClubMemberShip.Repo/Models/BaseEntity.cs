﻿namespace ClubMemberShip.Repo.Models;

public abstract class BaseEntity
{
    public Status? Status { get; set; }
}

public enum TimeLineStatus
{
    Pending = 0,
    OnGoing = 0,
    Finished = 0,
}

public enum Status
{
    Deleted = -1,
    Na = 0,
    Active = 1,
    Pending = 2,
}