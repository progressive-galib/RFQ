using System;
using Microsoft.EntityFrameworkCore;

namespace RFQ.Data;

public class RFQContext:DbContext
{
    public RFQContext(DbContextOptions <RFQContext>options) : base(options){}
}
