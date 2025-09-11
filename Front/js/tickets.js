// Tickets Module
const USE_DEMO_DATA = true;

const DEMO_TICKETS = [
  {
    id: 101,
    match: {
      id: 1,
      homeTeam: "İmişli FK",
      awayTeam: "Qarabağ FK",
      matchDate: "2024-07-15T19:00:00",
      stadium: "İmişli Şəhər Stadionu",
      capacity: 8000,
      basePrice: 10
    },
    sector: "north1",
    price: 11,
    status: "confirmed"
  },
  {
    id: 102,
    match: {
      id: 2,
      homeTeam: "İmişli FK",
      awayTeam: "Neftçi Baku",
      matchDate: "2024-07-22T20:30:00",
      stadium: "İmişli Şəhər Stadionu",
      capacity: 8000,
      basePrice: 12
    },
    sector: "west2",
    price: 18,
    status: "pending"
  },
  {
    id: 103,
    match: {
      id: 3,
      homeTeam: "Sabah FK",
      awayTeam: "İmişli FK",
      matchDate: "2024-08-01T18:00:00",
      stadium: "Bank Respublika Arena",
      capacity: 13000,
      basePrice: 15
    },
    sector: "east3",
    price: 21,
    status: "confirmed"
  },
  {
    id: 104,
    match: {
      id: 4,
      homeTeam: "İmişli FK",
      awayTeam: "Zirə FK",
      matchDate: "2025-09-10T19:30:00",
      stadium: "İmişli Şəhər Stadionu",
      capacity: 8000,
      basePrice: 13
    },
    sector: "east1",
    price: 15,
    status: "confirmed"
  },
  {
    id: 105,
    match: {
      id: 5,
      homeTeam: "Qəbələ FK",
      awayTeam: "İmişli FK",
      matchDate: "2025-09-18T20:00:00",
      stadium: "Qəbələ City Stadium",
      capacity: 9000,
      basePrice: 14
    },
    sector: "west1",
    price: 19,
    status: "used"
  },
  {
    id: 106,
    match: {
      id: 6,
      homeTeam: "İmişli FK",
      awayTeam: "Sumqayıt FK",
      matchDate: "2025-10-01T18:30:00",
      stadium: "İmişli Şəhər Stadionu",
      capacity: 8000,
      basePrice: 16
    },
    sector: "north4",
    price: 20,
    status: "pending"
  }
];

class Tickets {
    constructor() {
        this.tickets = [];
        this.initializeTickets();
    }

    initializeTickets() {
        this.loadTickets();
        this.setupEventListeners();
    }

    setupEventListeners() {
        // Ticket-related event listeners can be added here
    }

    async loadTickets() {
        if (USE_DEMO_DATA) {
            this.tickets = DEMO_TICKETS;
            this.renderRecentTickets();
            return;
        }
        try {
            const tickets = await Utils.get('/api/tickets');
            this.tickets = tickets || DEMO_TICKETS;
            this.renderRecentTickets();
        } catch (error) {
            this.tickets = DEMO_TICKETS;
            this.renderRecentTickets();
        }
    }

    renderRecentTickets() {
        const container = document.getElementById('recentTickets');
        if (!container) return;

        container.innerHTML = '';

        if (this.tickets.length === 0) {
            container.innerHTML = '<p class="no-tickets">No tickets purchased yet.</p>';
            return;
        }

        // Show only recent tickets (last 3)
        const recentTickets = this.tickets.slice(0, 3);
        
        recentTickets.forEach(ticket => {
            const ticketCard = this.createTicketCard(ticket);
            container.appendChild(ticketCard);
        });
    }

    createTicketCard(ticket) {
        const card = document.createElement('div');
        card.className = 'ticket-card';
        
        const statusClass = this.getTicketStatusClass(ticket.status);
        const statusText = this.getTicketStatusText(ticket.status);
        
        card.innerHTML = `
            <div class="ticket-header">
                <h4>${ticket.match.homeTeam} vs ${ticket.match.awayTeam}</h4>
                <span class="ticket-status ${statusClass}">${statusText}</span>
            </div>
            <div class="ticket-content">
                <div class="ticket-info">
                    <p><i class="fas fa-calendar"></i> ${Utils.formatDate(ticket.match.matchDate)}</p>
                    <p><i class="fas fa-clock"></i> ${Utils.formatTime(ticket.match.matchDate)}</p>
                    <p><i class="fas fa-map-marker-alt"></i> ${ticket.match.stadium}</p>
                    <p><i class="fas fa-chair"></i> Seat: ${ticket.sector}</p>
                    <p><i class="fas fa-dollar-sign"></i> ${Utils.formatCurrency(ticket.price)}</p>
                </div>
                <div class="ticket-actions">
                    <button class="btn btn-small btn-primary" onclick="window.tickets.viewTicket('${ticket.id}')">
                        <i class="fas fa-eye"></i> View
                    </button>
                    <button class="btn btn-small btn-secondary" onclick="window.tickets.downloadTicket('${ticket.id}')">
                        <i class="fas fa-download"></i> Download
                    </button>
                </div>
            </div>
        `;

        return card;
    }

    getTicketStatusClass(status) {
        const statusClasses = {
            'confirmed': 'status-confirmed',
            'pending': 'status-pending',
            'cancelled': 'status-cancelled',
            'used': 'status-used'
        };
        return statusClasses[status] || 'status-pending';
    }

    getTicketStatusText(status) {
        const statusTexts = {
            'confirmed': 'Confirmed',
            'pending': 'Pending',
            'cancelled': 'Cancelled',
            'used': 'Used'
        };
        return statusTexts[status] || 'Pending';
    }

    async viewTicket(ticketId) {
        try {
            const ticket = await Utils.get(`/api/tickets/${ticketId}`);
            if (ticket) {
                this.showTicketModal(ticket);
            }
        } catch (error) {
            console.error('Error loading ticket details:', error);
            Utils.showNotification('Failed to load ticket details', 'error');
        }
    }

    showTicketModal(ticket) {
        const modal = document.getElementById('matchModal');
        const content = document.getElementById('matchModalContent');
        
        if (modal && content) {
            content.innerHTML = `
                <div class="ticket-detail-modal">
                    <h2>Ticket Details</h2>
                    <div class="ticket-qr-section">
                        <div id="ticketQR"></div>
                        <p>Scan this QR code at the entrance</p>
                    </div>
                    <div class="ticket-details">
                        <div class="detail-row">
                            <span class="label">Match:</span>
                            <span class="value">${ticket.match.homeTeam} vs ${ticket.match.awayTeam}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Date:</span>
                            <span class="value">${Utils.formatDate(ticket.match.matchDate)}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Time:</span>
                            <span class="value">${Utils.formatTime(ticket.match.matchDate)}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Venue:</span>
                            <span class="value">${ticket.match.stadium}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Seat:</span>
                            <span class="value">${ticket.sector}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Price:</span>
                            <span class="value">${Utils.formatCurrency(ticket.price)}</span>
                        </div>
                        <div class="detail-row">
                            <span class="label">Status:</span>
                            <span class="value ${this.getTicketStatusClass(ticket.status)}">${this.getTicketStatusText(ticket.status)}</span>
                        </div>
                    </div>
                    <div class="ticket-actions">
                        <button class="btn btn-primary" onclick="window.tickets.downloadTicket('${ticket.id}')">
                            <i class="fas fa-download"></i> Download Ticket
                        </button>
                        <button class="btn btn-secondary" onclick="Utils.hideModal('matchModal')">
                            Close
                        </button>
                    </div>
                </div>
            `;
            
            // Generate QR code
            Utils.generateQRCode(`TICKET-${ticket.id}`, 'ticketQR');
            
            Utils.showModal('matchModal');
        }
    }

    async downloadTicket(ticketId) {
        try {
            const response = await Utils.get(`/api/tickets/${ticketId}/download`);
            
            if (response && response.downloadUrl) {
                // Create a temporary link to download the ticket
                const link = document.createElement('a');
                link.href = response.downloadUrl;
                link.download = `ticket-${ticketId}.pdf`;
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                
                Utils.showNotification('Ticket downloaded successfully!', 'success');
            } else {
                Utils.showNotification('Download failed. Please try again.', 'error');
            }
        } catch (error) {
            console.error('Error downloading ticket:', error);
            Utils.showNotification('Download failed. Please try again.', 'error');
        }
    }

    async cancelTicket(ticketId) {
        try {
            const response = await Utils.put(`/api/tickets/${ticketId}/cancel`);
            
            if (response && response.success) {
                // Update local tickets list
                const ticket = this.tickets.find(t => t.id === ticketId);
                if (ticket) {
                    ticket.status = 'cancelled';
                    this.renderRecentTickets();
                }
                
                Utils.showNotification('Ticket cancelled successfully!', 'success');
                return true;
            } else {
                Utils.showNotification('Failed to cancel ticket', 'error');
            }
        } catch (error) {
            console.error('Error cancelling ticket:', error);
            Utils.showNotification('Failed to cancel ticket', 'error');
        }
        return false;
    }

    // Get tickets for dashboard
    getRecentTickets() {
        return this.tickets.slice(0, 3);
    }

    // Get ticket by ID
    getTicketById(ticketId) {
        return this.tickets.find(ticket => ticket.id === ticketId);
    }

    // Check if ticket is valid for entry
    isTicketValid(ticket) {
        const matchDate = new Date(ticket.match.matchDate);
        const now = new Date();
        
        // Ticket is valid if match hasn't started yet
        return matchDate > now && ticket.status === 'confirmed';
    }

    // Get upcoming tickets
    getUpcomingTickets() {
        return this.tickets.filter(ticket => {
            const matchDate = new Date(ticket.match.matchDate);
            const now = new Date();
            return matchDate > now && ticket.status === 'confirmed';
        });
    }

    // Get past tickets
    getPastTickets() {
        return this.tickets.filter(ticket => {
            const matchDate = new Date(ticket.match.matchDate);
            const now = new Date();
            return matchDate <= now;
        });
    }

    // Calculate total spent on tickets
    getTotalSpent() {
        return this.tickets.reduce((sum, ticket) => sum + ticket.price, 0);
    }

    // Get ticket statistics
    getTicketStats() {
        const total = this.tickets.length;
        const upcoming = this.getUpcomingTickets().length;
        const past = this.getPastTickets().length;
        const totalSpent = this.getTotalSpent();

        return {
            total,
            upcoming,
            past,
            totalSpent
        };
    }

    showAllTicketsModal() {
        const modal = document.getElementById('matchModal');
        const content = document.getElementById('matchModalContent');
        if (!modal || !content) return;
        const all = this.tickets;
        if (!all || all.length === 0) {
            content.innerHTML = '<p class="no-tickets">No tickets to display.</p>';
            Utils.showModal('matchModal');
            return;
        }
        const list = all.map(t => `
            <div class="ticket-item">
                <div class="ticket-item-main">
                    <strong>${t.match.homeTeam} vs ${t.match.awayTeam}</strong>
                    <span class="muted">${Utils.formatDate(t.match.matchDate)} • ${Utils.formatTime(t.match.matchDate)}</span>
                </div>
                <div class="ticket-item-meta">
                    <span class="badge ${this.getTicketStatusClass(t.status)}">${this.getTicketStatusText(t.status)}</span>
                    <span>${Utils.formatCurrency(t.price)}</span>
                </div>
            </div>
        `).join('');
        content.innerHTML = `
            <div class="tickets-modal">
                <h2>All Tickets</h2>
                <div class="tickets-list">${list}</div>
                <div style="margin-top: 1rem; text-align: right;">
                    <button class="btn btn-secondary" onclick="Utils.hideModal('matchModal')">Close</button>
                </div>
            </div>
        `;
        Utils.showModal('matchModal');
    }
}

// Initialize tickets when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.tickets = new Tickets();
});

// Export for use in other files
window.Tickets = Tickets; 