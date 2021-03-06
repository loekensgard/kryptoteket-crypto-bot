﻿using Kryptoteket.Bot.Models;
using Kryptoteket.Bot.Models.Reflinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptoteket.Bot.CosmosDB.Mappings
{
    class PlacedBetMap : IEntityTypeConfiguration<PlacedBet>
    {
        public void Configure(EntityTypeBuilder<PlacedBet> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
