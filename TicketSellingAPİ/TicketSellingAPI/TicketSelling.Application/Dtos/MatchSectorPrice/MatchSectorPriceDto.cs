using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Dtos.Sector;

namespace TicketSelling.Application.Dtos.MatchSectorPrice;

public class MatchSectorPriceDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    public SectorDto? Sector { get; set; }
    public MatchDto? Match { get; set; }
}
