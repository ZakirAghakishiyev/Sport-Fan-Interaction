// Matches Module
const USE_DEMO_DATA = true;

const DEMO_MATCHES = [
  {
    id: 1,
    homeTeam: "İmişli FK",
    awayTeam: "Qarabağ FK",
    matchDate: "2024-07-15T19:00:00",
    stadium: "İmişli Şəhər Stadionu",
    capacity: 8000,
    basePrice: 10
  },
  {
    id: 2,
    homeTeam: "İmişli FK",
    awayTeam: "Neftçi Baku",
    matchDate: "2024-07-22T20:30:00",
    stadium: "İmişli Şəhər Stadionu",
    capacity: 8000,
    basePrice: 12
  },
  {
    id: 3,
    homeTeam: "Sabah FK",
    awayTeam: "İmişli FK",
    matchDate: "2024-08-01T18:00:00",
    stadium: "Bank Respublika Arena",
    capacity: 13000,
    basePrice: 15
  },
  {
    id: 4,
    homeTeam: "Kəpəz FK",
    awayTeam: "İmişli FK",
    matchDate: "2024-08-06T17:15:00",
    stadium: "Gəncə Şəhər Stadionu",
    capacity: 12000,
    basePrice: 9
  }
];

const DEMO_SECTOR_PRICES = {
  north1: 1,
  north2: 1.1,
  north3: 1.2,
  north4: 1.1,
  north5: 1.2,
  north6: 1.1,
  south1: 1,
  south2: 1.1,
  south3: 1.2,
  south4: 1.1,
  south5: 1.2,
  south6: 1.1,
  east1: 1.3,
  east2: 1.3,
  east3: 1.4,
  east4: 1.3,
  east5: 1.4,
  east6: 1.3,
  west1: 1.5,
  west2: 1.5,
  west3: 1.6,
  west4: 1.5,
  west5: 1.6,
  west6: 1.5
};

class Matches {
    constructor() {
        this.matches = [];
        this.currentMatch = null;
        this.selectedSeat = null;
        this.sectorPrices = {};
        this.initializeMatches();
    }

    initializeMatches() {
        this.loadMatches();
        this.setupEventListeners();
    }

    setupEventListeners() {
        // Book ticket button on dashboard
        const bookTicketBtn = document.getElementById('bookTicketBtn');
        if (bookTicketBtn) {
            bookTicketBtn.addEventListener('click', () => {
                window.location.href = 'match.html';
            });
        }

        // Match modal setup
        const matchModal = document.getElementById('matchModal');
        if (matchModal) {
            Utils.setupModalClose('matchModal');
        }

        // Tab functionality for match info
        const infoTabs = document.querySelector('.info-tabs');
        if (infoTabs) {
            Utils.setupTabs(infoTabs);
        }
    }

    async loadMatches() {
        if (USE_DEMO_DATA) {
            this.matches = DEMO_MATCHES;
            this.renderMatches();
            return;
        }
        try {
            const matches = await Utils.get('/matches');
            this.matches = matches || [];
            this.renderMatches();
        } catch (error) {
            this.matches = DEMO_MATCHES;
            this.renderMatches();
        }
    }

    renderMatches() {
        const containers = [
            document.getElementById('matches-container'),
            document.getElementById('upcomingMatches')
        ];

        containers.forEach(container => {
            if (container) {
                container.innerHTML = '';
                
                if (this.matches.length === 0) {
                    container.innerHTML = '<p class="no-matches">No matches available at the moment.</p>';
                    return;
                }

                this.matches.forEach(match => {
                    const matchCard = this.createMatchCard(match);
                    container.appendChild(matchCard);
                });
            }
        });
    }

    createMatchCard(match) {
        const card = document.createElement('div');
        card.className = 'match-card';
        card.innerHTML = `
            <div class="match-header">
                <h3>${match.homeTeam} vs ${match.awayTeam}</h3>
                <div class="match-teams">
                    <div class="team home-team">
                        <span class="team-name">${match.homeTeam}</span>
                    </div>
                    <div class="vs">VS</div>
                    <div class="team away-team">
                        <span class="team-name">${match.awayTeam}</span>
                    </div>
                </div>
                <div class="match-meta">
                    <span class="match-date">${Utils.formatDate(match.matchDate)}</span>
                    <span class="match-time">${Utils.formatTime(match.matchDate)}</span>
                    <span class="match-stadium">${match.stadium}</span>
                </div>
            </div>
            <div class="match-content">
                <div class="match-info">
                    <p><i class="fas fa-map-marker-alt"></i> ${match.stadium}</p>
                    <p><i class="fas fa-users"></i> Capacity: ${match.capacity.toLocaleString()}</p>
                    <p><i class="fas fa-ticket-alt"></i> Starting from ${Utils.formatCurrency(match.basePrice)}</p>
                </div>
                <div class="match-actions">
                    <button class="btn btn-primary book-match-btn" data-match-id="${match.id}">
                        Book Tickets
                    </button>
                    <button class="btn btn-secondary view-match-btn" data-match-id="${match.id}">
                        View Details
                    </button>
                </div>
            </div>
        `;

        // Add event listeners
        const bookBtn = card.querySelector('.book-match-btn');
        const viewBtn = card.querySelector('.view-match-btn');

        if (bookBtn) {
            bookBtn.addEventListener('click', () => {
                this.bookMatch(match.id);
            });
        }

        if (viewBtn) {
            viewBtn.addEventListener('click', () => {
                this.viewMatchDetails(match.id);
            });
        }

        return card;
    }

    async bookMatch(matchId) {
        if (!(typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED)) {
            if (!Utils.isAuthenticated()) {
                Utils.showNotification('Please login to book tickets', 'error');
                window.location.href = '/login.html';
                return;
            }
        }
        window.location.href = `match.html?id=${matchId}`;
    }

    async viewMatchDetails(matchId) {
        // Navigate directly to the match details page
        window.location.href = `match.html?id=${matchId}`;
    }

    showMatchModal(match) {
        const modal = document.getElementById('matchModal');
        const content = document.getElementById('matchModalContent');
        
        if (modal && content) {
            content.innerHTML = `
                <div class="match-details-modal">
                    <h2>${match.homeTeam} vs ${match.awayTeam}</h2>
                    <div class="match-info-grid">
                        <div class="info-item">
                            <i class="fas fa-calendar"></i>
                            <span>${Utils.formatDate(match.matchDate)}</span>
                        </div>
                        <div class="info-item">
                            <i class="fas fa-clock"></i>
                            <span>${Utils.formatTime(match.matchDate)}</span>
                        </div>
                        <div class="info-item">
                            <i class="fas fa-map-marker-alt"></i>
                            <span>${match.stadium}</span>
                        </div>
                        <div class="info-item">
                            <i class="fas fa-users"></i>
                            <span>Capacity: ${match.capacity.toLocaleString()}</span>
                        </div>
                    </div>
                    <div class="match-actions">
                        <button class="btn btn-primary" onclick="window.location.href='match.html?id=${match.id}'">
                            Book Tickets
                        </button>
                        <button class="btn btn-secondary" onclick="Utils.hideModal('matchModal')">
                            Close
                        </button>
                    </div>
                </div>
            `;
            
            Utils.showModal('matchModal');
        }
    }

    async loadMatchDetails(matchId) {
        if (USE_DEMO_DATA) {
            const match = DEMO_MATCHES.find(m => m.id == matchId);
            if (match) {
                this.currentMatch = match;
                this.displayMatchDetails(match);
                await this.loadSectorPrices(matchId);
                this.setupStadiumLayout();
            }
            return;
        }
        try {
            const match = await Utils.get(`/matches/${matchId}`);
            if (match) {
                this.currentMatch = match;
                this.displayMatchDetails(match);
                await this.loadSectorPrices(matchId);
                this.setupStadiumLayout();
            }
        } catch (error) {
            const match = DEMO_MATCHES.find(m => m.id == matchId);
            if (match) {
                this.currentMatch = match;
                this.displayMatchDetails(match);
                await this.loadSectorPrices(matchId);
                this.setupStadiumLayout();
            }
        }
    }

    displayMatchDetails(match) {
        // Update match header
        document.getElementById('matchTitle').textContent = `${match.homeTeam} vs ${match.awayTeam}`;
        document.getElementById('matchDate').textContent = Utils.formatDate(match.matchDate);
        document.getElementById('matchTime').textContent = Utils.formatTime(match.matchDate);
        document.getElementById('matchStadium').textContent = match.stadium;
        document.getElementById('homeTeam').textContent = match.homeTeam;
        document.getElementById('awayTeam').textContent = match.awayTeam;

        // Update info tabs
        document.getElementById('infoDate').textContent = Utils.formatDate(match.matchDate);
        document.getElementById('infoTime').textContent = Utils.formatTime(match.matchDate);
        document.getElementById('infoVenue').textContent = match.stadium;
        document.getElementById('infoCapacity').textContent = match.capacity.toLocaleString();
    }

    async loadSectorPrices(matchId) {
        if (USE_DEMO_DATA) {
            this.sectorPrices = DEMO_SECTOR_PRICES;
            return;
        }
        try {
            const prices = await Utils.get(`/matches/${matchId}/sector-prices`);
            this.sectorPrices = prices || {};
        } catch (error) {
            this.sectorPrices = DEMO_SECTOR_PRICES;
        }
    }

    setupStadiumLayout() {
        const stands = ['north', 'south', 'east', 'west'];
        
        stands.forEach(stand => {
            const standElement = document.getElementById(`${stand}Stand`);
            const sectorsElement = document.getElementById(`${stand}Sectors`);
            
            if (sectorsElement) {
                sectorsElement.innerHTML = '';
                
                // Create sectors for each stand
                for (let i = 1; i <= 6; i++) {
                    const sector = document.createElement('div');
                    sector.className = 'sector';
                    sector.textContent = `${stand.toUpperCase()}${i}`;
                    sector.dataset.sector = `${stand}${i}`;
                    
                    sector.addEventListener('click', () => {
                        this.selectSector(sector);
                    });
                    
                    sectorsElement.appendChild(sector);
                }
            }
        });
    }

    selectSector(sectorElement) {
        // Remove previous selection
        document.querySelectorAll('.sector.selected').forEach(s => {
            s.classList.remove('selected');
        });

        // Add selection to clicked sector
        sectorElement.classList.add('selected');
        
        const sectorName = sectorElement.dataset.sector;
        this.selectedSeat = sectorName;
        
        this.updateBookingSummary(sectorName);
    }

    updateBookingSummary(sectorName) {
        const selectedSeatInfo = document.getElementById('selectedSeatInfo');
        const ticketPrice = document.getElementById('ticketPrice');
        const serviceFee = document.getElementById('serviceFee');
        const totalPrice = document.getElementById('totalPrice');
        const bookTicketBtn = document.getElementById('bookTicketBtn');

        if (selectedSeatInfo) {
            selectedSeatInfo.innerHTML = `
                <p><strong>Selected Seat:</strong> ${sectorName}</p>
                <p><strong>Match:</strong> ${this.currentMatch.homeTeam} vs ${this.currentMatch.awayTeam}</p>
            `;
        }

        // Calculate prices
        const basePrice = this.currentMatch.basePrice;
        const sectorMultiplier = this.sectorPrices[sectorName] || 1;
        const finalPrice = basePrice * sectorMultiplier;
        const serviceFeeAmount = finalPrice * 0.1; // 10% service fee
        const total = finalPrice + serviceFeeAmount;

        if (ticketPrice) ticketPrice.textContent = Utils.formatCurrency(finalPrice);
        if (serviceFee) serviceFee.textContent = Utils.formatCurrency(serviceFeeAmount);
        if (totalPrice) totalPrice.textContent = Utils.formatCurrency(total);
        if (bookTicketBtn) {
            bookTicketBtn.disabled = false;
            bookTicketBtn.addEventListener('click', () => {
                this.bookTicket(sectorName, total);
            });
        }
    }

    async bookTicket(sectorName, totalPrice) {
        if (!this.selectedSeat) {
            Utils.showNotification('Please select a seat first', 'error');
            return;
        }

        try {
            const bookingData = {
                matchId: this.currentMatch.id,
                sector: sectorName,
                price: totalPrice,
                userId: Utils.getUserData().id
            };

            const response = await Utils.post('/tickets', bookingData);

            if (response && response.success) {
                this.showBookingConfirmation(response.ticket);
            } else {
                Utils.showNotification('Booking failed. Please try again.', 'error');
            }
        } catch (error) {
            console.error('Booking error:', error);
            Utils.showNotification('Booking failed. Please try again.', 'error');
        }
    }

    showBookingConfirmation(ticket) {
        const modal = document.getElementById('bookingModal');
        const content = document.getElementById('matchModalContent');
        
        if (modal && content) {
            // Update confirmation details
            document.getElementById('confirmMatch').textContent = `${this.currentMatch.homeTeam} vs ${this.currentMatch.awayTeam}`;
            document.getElementById('confirmSeat').textContent = this.selectedSeat;
            document.getElementById('confirmDate').textContent = Utils.formatDate(this.currentMatch.matchDate);
            document.getElementById('confirmTime').textContent = Utils.formatTime(this.currentMatch.matchDate);

            // Generate QR code
            Utils.generateQRCode(`TICKET-${ticket.id}`, 'qrCode');

            Utils.showModal('bookingModal');
        }
    }

    // Get matches for dashboard
    getUpcomingMatches() {
        return this.matches.filter(match => new Date(match.matchDate) > new Date());
    }

    // Get match by ID
    getMatchById(matchId) {
        return this.matches.find(match => match.id === matchId);
    }
}

// Initialize matches when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.matches = new Matches();
    
    // Load specific match if on match page
    const urlParams = new URLSearchParams(window.location.search);
    const matchId = urlParams.get('id');
    
    if (matchId && window.matches) {
        window.matches.loadMatchDetails(matchId);
    }
});

// Export for use in other files
window.Matches = Matches; 