﻿using ClubMemberShip.Repo.Models;
using ClubMemberShip.Repo.Repository.Interface;

namespace ClubMemberShip.Repo.Repository;

public class ClubActivityRepo : GenericRepo<ClubActivity>, IClubActivityRepo
{
    public override void Delete(object? id)
    {
        throw new NotImplementedException();
    }
}