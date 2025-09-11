// Loyalty Module
class Loyalty {
    constructor() {
        this.loyaltyData = null;
        this.rewards = [];
        this.initializeLoyalty();
    }

    initializeLoyalty() {
        this.loadLoyaltyData();
        this.setupEventListeners();
    }

    setupEventListeners() {
        // View rewards button on dashboard
        const viewRewardsBtn = document.getElementById('viewRewardsBtn');
        if (viewRewardsBtn) {
            viewRewardsBtn.addEventListener('click', () => {
                this.showRewardsModal();
            });
        }
    }

    async loadLoyaltyData() {
        try {
            const loyaltyData = await Utils.get('/api/loyalty/profile');
            this.loyaltyData = loyaltyData || this.getDefaultLoyaltyData();
            this.displayLoyaltyStatus();
        } catch (error) {
            console.error('Error loading loyalty data:', error);
            // Use default data if API fails
            this.loyaltyData = this.getDefaultLoyaltyData();
            this.displayLoyaltyStatus();
        }
    }

    getDefaultLoyaltyData() {
        return {
            points: 420,
            tier: 'Silver',
            tierName: 'Silver Member',
            pointsToNextTier: 500,
            totalPointsEarned: 1260,
            rewards: []
        };
    }

    displayLoyaltyStatus() {
        if (!this.loyaltyData) return;

        // Update tier badge
        const tierBadge = document.getElementById('tierBadge');
        if (tierBadge) {
            tierBadge.textContent = this.loyaltyData.tier;
            tierBadge.className = `tier-badge tier-${this.loyaltyData.tier.toLowerCase()}`;
        }

        // Update tier name
        const tierName = document.getElementById('tierName');
        if (tierName) {
            tierName.textContent = this.loyaltyData.tierName;
        }

        // Update points display
        const pointsNumber = document.getElementById('pointsNumber');
        if (pointsNumber) {
            pointsNumber.textContent = this.loyaltyData.points.toLocaleString();
        }

        // Update progress bar
        this.updateProgressBar();

        // Update progress text
        const progressText = document.getElementById('progressText');
        if (progressText) {
            const nextTierPoints = this.getNextTierPoints();
            const currentTierPoints = this.getCurrentTierPoints();
            const progress = this.loyaltyData.points - currentTierPoints;
            const needed = nextTierPoints - currentTierPoints;
            progressText.textContent = `${progress} / ${needed} points to next tier`;
        }
    }

    updateProgressBar() {
        const progressFill = document.getElementById('pointsProgress');
        if (!progressFill) return;

        const currentTierPoints = this.getCurrentTierPoints();
        const nextTierPoints = this.getNextTierPoints();
        const progress = this.loyaltyData.points - currentTierPoints;
        const total = nextTierPoints - currentTierPoints;
        const percentage = Math.min((progress / total) * 100, 100);

        progressFill.style.width = `${percentage}%`;
    }

    getCurrentTierPoints() {
        const tierPoints = {
            'Bronze': 0,
            'Silver': 100,
            'Gold': 500,
            'Platinum': 1000,
            'Diamond': 2000
        };
        return tierPoints[this.loyaltyData.tier] || 0;
    }

    getNextTierPoints() {
        const tierPoints = {
            'Bronze': 100,
            'Silver': 500,
            'Gold': 1000,
            'Platinum': 2000,
            'Diamond': 5000
        };
        return tierPoints[this.loyaltyData.tier] || 100;
    }

    async loadRewards() {
        try {
            const rewards = await Utils.get('/api/loyalty/rewards');
            this.rewards = rewards || DEMO_REWARDS;
            this.displayRecentRewards();
        } catch (error) {
            console.error('Error loading rewards:', error);
            this.rewards = DEMO_REWARDS;
            this.displayRecentRewards();
        }
    }

    displayRecentRewards() {
        const rewardsContainer = document.getElementById('recentRewards');
        if (!rewardsContainer) return;

        rewardsContainer.innerHTML = '';

        if (this.rewards.length === 0) {
            rewardsContainer.innerHTML = '<p class="no-rewards">No rewards yet. Start earning points!</p>';
            return;
        }

        // Show only recent rewards (last 3)
        const recentRewards = this.rewards.slice(0, 3);
        
        recentRewards.forEach(reward => {
            const rewardCard = this.createRewardCard(reward);
            rewardsContainer.appendChild(rewardCard);
        });
    }

    createRewardCard(reward) {
        const card = document.createElement('div');
        card.className = 'reward-card';
        
        const rewardIcon = this.getRewardIcon(reward.type);
        const rewardStatus = this.getRewardStatus(reward.status);
        
        card.innerHTML = `
            <div class="reward-icon">
                <i class="fas fa-${rewardIcon}"></i>
            </div>
            <h4>${reward.title}</h4>
            <p>${reward.description}</p>
            <div class="reward-meta">
                <span class="reward-date">${Utils.formatDate(reward.earnedDate)}</span>
                <span class="reward-status ${rewardStatus.class}">${rewardStatus.text}</span>
            </div>
        `;

        return card;
    }

    getRewardIcon(type) {
        const icons = {
            'seat_upgrade': 'arrow-up',
            'merchandise': 'tshirt',
            'discount': 'percent',
            'free_ticket': 'ticket-alt',
            'vip_access': 'crown',
            'default': 'gift'
        };
        return icons[type] || icons.default;
    }

    getRewardStatus(status) {
        const statuses = {
            'active': { text: 'Active', class: 'status-active' },
            'used': { text: 'Used', class: 'status-used' },
            'expired': { text: 'Expired', class: 'status-expired' },
            'pending': { text: 'Pending', class: 'status-pending' }
        };
        return statuses[status] || statuses.pending;
    }

    showRewardsModal() {
        // Create modal content for all rewards
        const modalContent = `
            <div class="rewards-modal">
                <h2>My Rewards</h2>
                <div class="rewards-summary">
                    <div class="summary-item">
                        <span class="label">Total Points:</span>
                        <span class="value">${this.loyaltyData.points.toLocaleString()}</span>
                    </div>
                    <div class="summary-item">
                        <span class="label">Current Tier:</span>
                        <span class="value">${this.loyaltyData.tier}</span>
                    </div>
                    <div class="summary-item">
                        <span class="label">Total Rewards:</span>
                        <span class="value">${this.rewards.length}</span>
                    </div>
                </div>
                <div class="rewards-list">
                    ${this.rewards.map(reward => `
                        <div class="reward-item">
                            <div class="reward-info">
                                <i class="fas fa-${this.getRewardIcon(reward.type)}"></i>
                                <div>
                                    <h4>${reward.title}</h4>
                                    <p>${reward.description}</p>
                                </div>
                            </div>
                            <div class="reward-actions">
                                <span class="reward-date">${Utils.formatDate(reward.earnedDate)}</span>
                                <span class="reward-status ${this.getRewardStatus(reward.status).class}">
                                    ${this.getRewardStatus(reward.status).text}
                                </span>
                                ${reward.status === 'active' ? '<button class="btn btn-small btn-primary">Use Reward</button>' : ''}
                            </div>
                        </div>
                    `).join('')}
                </div>
            </div>
        `;

        // Show modal
        const modal = document.getElementById('matchModal');
        const content = document.getElementById('matchModalContent');
        
        if (modal && content) {
            content.innerHTML = modalContent;
            Utils.showModal('matchModal');
        }
    }

    async earnPoints(amount, reason) {
        try {
            const response = await Utils.post('/api/loyalty/earn-points', {
                points: amount,
                reason: reason
            });

            if (response && response.success) {
                // Update local loyalty data
                this.loyaltyData.points += amount;
                this.displayLoyaltyStatus();
                
                Utils.showNotification(`Earned ${amount} loyalty points!`, 'success');
                return true;
            }
        } catch (error) {
            console.error('Error earning points:', error);
            Utils.showNotification('Failed to earn points', 'error');
        }
        return false;
    }

    async redeemReward(rewardId) {
        try {
            const response = await Utils.post(`/api/loyalty/redeem/${rewardId}`);
            
            if (response && response.success) {
                // Update rewards list
                const reward = this.rewards.find(r => r.id === rewardId);
                if (reward) {
                    reward.status = 'used';
                    this.displayRecentRewards();
                }
                
                Utils.showNotification('Reward redeemed successfully!', 'success');
                return true;
            }
        } catch (error) {
            console.error('Error redeeming reward:', error);
            Utils.showNotification('Failed to redeem reward', 'error');
        }
        return false;
    }

    getTierBenefits(tier) {
        const benefits = {
            'Bronze': [
                'Earn 1 point per $1 spent',
                'Basic customer support'
            ],
            'Silver': [
                'Earn 1.5 points per $1 spent',
                'Priority customer support',
                '10% discount on merchandise'
            ],
            'Gold': [
                'Earn 2 points per $1 spent',
                'VIP customer support',
                '15% discount on merchandise',
                'Free seat upgrades'
            ],
            'Platinum': [
                'Earn 2.5 points per $1 spent',
                'Dedicated customer support',
                '20% discount on merchandise',
                'Priority seat selection',
                'Exclusive merchandise access'
            ],
            'Diamond': [
                'Earn 3 points per $1 spent',
                'Personal account manager',
                '25% discount on merchandise',
                'VIP seating guaranteed',
                'Exclusive events access',
                'Free parking'
            ]
        };
        return benefits[tier] || benefits['Bronze'];
    }

    calculatePointsEarned(amount) {
        const tierMultipliers = {
            'Bronze': 1,
            'Silver': 1.5,
            'Gold': 2,
            'Platinum': 2.5,
            'Diamond': 3
        };
        
        const multiplier = tierMultipliers[this.loyaltyData.tier] || 1;
        return Math.floor(amount * multiplier);
    }

    // Get loyalty data for other modules
    getLoyaltyData() {
        return this.loyaltyData;
    }

    // Check if user can afford a reward
    canAffordReward(pointsCost) {
        return this.loyaltyData.points >= pointsCost;
    }

    // Get available rewards for purchase
    async getAvailableRewards() {
        try {
            const rewards = await Utils.get('/api/loyalty/available-rewards');
            return rewards || [];
        } catch (error) {
            console.error('Error loading available rewards:', error);
            return [];
        }
    }
}

// Demo rewards for visualization on dashboard
const DEMO_REWARDS = [
	{
		id: 1,
		title: '10% Merchandise Discount',
		description: 'Save on official club gear at checkout.',
		type: 'discount',
		status: 'active',
		earnedDate: '2024-07-10T12:00:00'
	},
	{
		id: 2,
		title: 'Seat Upgrade Voucher',
		description: 'One-time upgrade to a better seat for your next match.',
		type: 'seat_upgrade',
		status: 'used',
		earnedDate: '2024-06-28T09:30:00'
	},
	{
		id: 3,
		title: 'Free Match Ticket',
		description: 'Redeem for a free ticket to a home match.',
		type: 'free_ticket',
		status: 'pending',
		earnedDate: '2024-07-05T17:45:00'
	}
];

// Initialize loyalty when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.loyalty = new Loyalty();
    
    // Load rewards after loyalty data is loaded
    setTimeout(() => {
        if (window.loyalty) {
            window.loyalty.loadRewards();
        }
    }, 1000);
});

// Export for use in other files
window.Loyalty = Loyalty; 