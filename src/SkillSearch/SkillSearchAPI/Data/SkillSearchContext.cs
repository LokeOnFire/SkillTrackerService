﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SkillSearchAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSearchAPI.Data
{
    public class SkillSearchContext : ISkillSearchContext
    {
        public SkillSearchContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            AssociateSkills = database.GetCollection<AssociateSkill>(configuration.GetValue<string>("DatabaseSettings:AssociateCollectionName"));

            Skills = database.GetCollection<Skill>(configuration.GetValue<string>("DatabaseSettings:SkillCollectionName"));

            SeedInitialData.SeedData(AssociateSkills, Skills);
            
        }
        public IMongoCollection<AssociateSkill> AssociateSkills { get; }

        public IMongoCollection<Skill> Skills { get; }
    }
}
