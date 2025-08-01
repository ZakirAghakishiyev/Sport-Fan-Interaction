using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Application.Dtos.MatchSectorPrice;
using TicketSelling.Application.Dtos.Stadium;

namespace TicketSelling.Application.Dtos.Match
{
    public class MatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Opponent { get; set; } = string.Empty;

        public StadiumDto? Stadium { get; set; }
        public List<MatchSectorPriceDto> SectorPrices { get; set; } = new();
    }


}
