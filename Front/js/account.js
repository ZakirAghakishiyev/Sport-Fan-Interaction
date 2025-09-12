// Account Management Module
class AccountManager {
    constructor() {
        this.userData = null;
        this.cards = [];
        this.initializeAccount();
    }

    initializeAccount() {
        // Check authentication
        if (!Utils.requireAuth()) {
            return;
        }

        this.setupEventListeners();
        this.setupTabs();
        this.loadUserProfile();
        this.loadUserCards();
    }

    setupEventListeners() {
        // Tab navigation
        const tabContainer = document.querySelector('.account-nav');
        if (tabContainer) {
            const tabs = tabContainer.querySelectorAll('.nav-tab');
            tabs.forEach(tab => {
                tab.addEventListener('click', () => {
                    const target = tab.getAttribute('data-tab');
                    this.switchTab(target);
                });
            });
        }

        // Profile editing
        const editProfileBtn = document.getElementById('editProfileBtn');
        if (editProfileBtn) {
            editProfileBtn.addEventListener('click', () => {
                this.showEditProfileModal();
            });
        }

        // Card management
        const addCardBtn = document.getElementById('addCardBtn');
        if (addCardBtn) {
            addCardBtn.addEventListener('click', () => {
                this.showAddCardModal();
            });
        }

        // Settings
        const changePasswordBtn = document.getElementById('changePasswordBtn');
        if (changePasswordBtn) {
            changePasswordBtn.addEventListener('click', () => {
                this.showChangePasswordModal();
            });
        }

        // Modal setups
        this.setupModals();

        // Form submissions
        this.setupFormHandlers();
    }

    setupTabs() {
        const tabContainer = document.querySelector('.account-nav');
        if (tabContainer) {
            Utils.setupTabs(tabContainer);
        }
    }

    setupModals() {
        const modals = ['editProfileModal', 'addCardModal', 'changePasswordModal'];
        modals.forEach(modalId => {
            Utils.setupModalClose(modalId);
        });
    }

    setupFormHandlers() {
        // Edit profile form
        const editProfileForm = document.getElementById('editProfileForm');
        if (editProfileForm) {
            editProfileForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.updateProfile();
            });
        }

        // Add card form
        const addCardForm = document.getElementById('addCardForm');
        if (addCardForm) {
            addCardForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.addCard();
            });
        }

        // Change password form
        const changePasswordForm = document.getElementById('changePasswordForm');
        if (changePasswordForm) {
            changePasswordForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.changePassword();
            });
        }

        // Cancel buttons
        const cancelButtons = [
            { id: 'cancelEditProfileBtn', modal: 'editProfileModal' },
            { id: 'cancelAddCardBtn', modal: 'addCardModal' },
            { id: 'cancelChangePasswordBtn', modal: 'changePasswordModal' }
        ];

        cancelButtons.forEach(({ id, modal }) => {
            const btn = document.getElementById(id);
            if (btn) {
                btn.addEventListener('click', () => {
                    Utils.hideModal(modal);
                });
            }
        });

        // Populate expiry year dropdown
        this.populateExpiryYears();
    }

    populateExpiryYears() {
        const yearSelect = document.getElementById('expiryYear');
        if (yearSelect) {
            const currentYear = new Date().getFullYear();
            for (let i = 0; i < 10; i++) {
                const year = currentYear + i;
                const option = document.createElement('option');
                option.value = year;
                option.textContent = year;
                yearSelect.appendChild(option);
            }
        }
    }

    switchTab(tabName) {
        // Hide all content sections
        const contentSections = document.querySelectorAll('.account-content');
        contentSections.forEach(section => {
            section.style.display = 'none';
        });

        // Remove active class from all tabs
        const tabs = document.querySelectorAll('.nav-tab');
        tabs.forEach(tab => {
            tab.classList.remove('active');
        });

        // Show selected content and activate tab
        const targetContent = document.getElementById(`${tabName}Tab`);
        const targetTab = document.querySelector(`[data-tab="${tabName}"]`);
        
        if (targetContent) {
            targetContent.style.display = 'block';
        }
        if (targetTab) {
            targetTab.classList.add('active');
        }

        // Load data for specific tabs
        switch (tabName) {
            case 'cards':
                this.loadUserCards();
                break;
        }
    }

    async loadUserProfile() {
        try {
            this.userData = Utils.getUserData();
            if (!this.userData) {
                // Try to fetch from API - need to get user ID first
                const currentUser = Utils.getUserData();
                if (currentUser && currentUser.id) {
                    this.userData = await Utils.get(`/api/AppUser/${currentUser.id}`);
                    if (this.userData) {
                        Utils.setUserData(this.userData);
                    }
                }
            }

            if (this.userData) {
                this.displayProfile();
            } else {
                Utils.showNotification('Failed to load profile data', 'error');
            }
        } catch (error) {
            console.error('Error loading user profile:', error);
            Utils.showNotification('Failed to load profile data', 'error');
        }
    }

    displayProfile() {
        if (!this.userData) return;

        const elements = {
            'profileName': this.userData.userName || this.userData.firstName || 'User',
            'profileEmail': this.userData.email || 'Not provided',
            'profilePhone': this.userData.phoneNumber || this.userData.phone || 'Not provided',
            'profileRole': this.userData.roles ? this.userData.roles.join(', ') : (this.userData.role || 'User'),
            'profileJoined': this.userData.createdAt ? `Joined: ${Utils.formatDate(this.userData.createdAt)}` : 'Member since: Unknown'
        };

        Object.entries(elements).forEach(([id, value]) => {
            const element = document.getElementById(id);
            if (element) {
                element.textContent = value;
            }
        });
    }

    showEditProfileModal() {
        if (!this.userData) return;

        // Populate form with current data
        document.getElementById('editFirstName').value = this.userData.userName || this.userData.firstName || '';
        document.getElementById('editLastName').value = this.userData.lastName || '';
        document.getElementById('editEmail').value = this.userData.email || '';
        document.getElementById('editPhone').value = this.userData.phoneNumber || this.userData.phone || '';

        Utils.showModal('editProfileModal');
    }

    async updateProfile() {
        const form = document.getElementById('editProfileForm');
        const submitBtn = form.querySelector('button[type="submit"]');

        const formData = {
            userName: document.getElementById('editFirstName').value, // Using firstName field for userName
            email: document.getElementById('editEmail').value,
            phoneNumber: document.getElementById('editPhone').value
        };

        // Validation
        if (!formData.userName || !formData.email) {
            Utils.showNotification('Please fill in all required fields', 'error');
            return;
        }

        if (!Utils.validateEmail(formData.email)) {
            Utils.showNotification('Please enter a valid email address', 'error');
            return;
        }

        try {
            Utils.showLoading(submitBtn);

            // Get current user ID
            const currentUser = Utils.getUserData();
            const userId = currentUser.id || currentUser.userId;
            
            if (!userId) {
                Utils.showNotification('User ID not found', 'error');
                return;
            }

            const response = await Utils.put(`/api/AppUser/${userId}`, formData);
            
            if (response) {
                // Update local user data
                this.userData = { ...this.userData, ...formData };
                Utils.setUserData(this.userData);
                
                Utils.showNotification('Profile updated successfully!', 'success');
                Utils.hideModal('editProfileModal');
                this.displayProfile();
            } else {
                Utils.showNotification('Failed to update profile', 'error');
            }
        } catch (error) {
            console.error('Error updating profile:', error);
            Utils.showNotification('Failed to update profile', 'error');
        } finally {
            Utils.hideLoading(submitBtn);
        }
    }

    async loadUserCards() {
        try {
            // Get current user ID
            const currentUser = Utils.getUserData();
            const userId = currentUser.id || currentUser.userId;
            
            if (!userId) {
                Utils.showNotification('User ID not found', 'error');
                return;
            }

            const cards = await Utils.get(`/api/AppUser/${userId}/cards`);
            this.cards = cards || [];
            this.displayCards();
        } catch (error) {
            console.error('Error loading cards:', error);
            Utils.showNotification('Failed to load payment methods', 'error');
        }
    }

    displayCards() {
        const container = document.getElementById('cardsList');
        if (!container) return;

        container.innerHTML = '';

        if (this.cards.length === 0) {
            container.innerHTML = '<div class="no-cards"><p>No payment methods saved</p><p>Add a card to get started</p></div>';
            return;
        }

        this.cards.forEach(card => {
            const cardElement = this.createCardElement(card);
            container.appendChild(cardElement);
        });
    }

    createCardElement(card) {
        const cardDiv = document.createElement('div');
        cardDiv.className = 'card-item';
        
        const last4 = card.last4 || '****';
        const brand = card.brand || 'Card';
        const expiry = card.expiryMonth && card.expiryYear ? 
            `${card.expiryMonth}/${card.expiryYear}` : 'N/A';

        cardDiv.innerHTML = `
            <div class="card-info">
                <div class="card-brand">
                    <i class="fas fa-credit-card"></i>
                    <span>${brand}</span>
                </div>
                <div class="card-number">
                    **** **** **** ${last4}
                </div>
                <div class="card-expiry">
                    Expires: ${expiry}
                </div>
                ${card.isDefault ? '<div class="default-badge">Default</div>' : ''}
            </div>
            <div class="card-actions">
                <button class="btn btn-small btn-danger" onclick="window.accountManager.deleteCard('${card.id}')">
                    <i class="fas fa-trash"></i> Remove
                </button>
                ${!card.isDefault ? `
                    <button class="btn btn-small btn-secondary" onclick="window.accountManager.setDefaultCard('${card.id}')">
                        <i class="fas fa-star"></i> Set Default
                    </button>
                ` : ''}
            </div>
        `;

        return cardDiv;
    }

    showAddCardModal() {
        // Clear form
        document.getElementById('addCardForm').reset();
        Utils.showModal('addCardModal');
    }

    async addCard() {
        const form = document.getElementById('addCardForm');
        const submitBtn = form.querySelector('button[type="submit"]');

        const formData = {
            cardNumber: document.getElementById('cardNumber').value.replace(/\s/g, ''),
            expiryMonth: document.getElementById('expiryMonth').value,
            expiryYear: document.getElementById('expiryYear').value,
            cvv: document.getElementById('cvv').value,
            cardholderName: document.getElementById('cardholderName').value,
            isDefault: document.getElementById('setAsDefault').checked
        };

        // Validation
        if (!formData.cardNumber || !formData.expiryMonth || !formData.expiryYear || 
            !formData.cvv || !formData.cardholderName) {
            Utils.showNotification('Please fill in all required fields', 'error');
            return;
        }

        if (formData.cardNumber.length < 13 || formData.cardNumber.length > 19) {
            Utils.showNotification('Please enter a valid card number', 'error');
            return;
        }

        if (formData.cvv.length < 3 || formData.cvv.length > 4) {
            Utils.showNotification('Please enter a valid CVV', 'error');
            return;
        }

        try {
            Utils.showLoading(submitBtn);

            const response = await Utils.post('/api/CardDetails', formData);
            
            if (response) {
                Utils.showNotification('Card added successfully!', 'success');
                Utils.hideModal('addCardModal');
                this.loadUserCards();
            } else {
                Utils.showNotification('Failed to add card', 'error');
            }
        } catch (error) {
            console.error('Error adding card:', error);
            Utils.showNotification('Failed to add card', 'error');
        } finally {
            Utils.hideLoading(submitBtn);
        }
    }

    async deleteCard(cardId) {
        if (!confirm('Are you sure you want to remove this payment method?')) {
            return;
        }

        try {
            await Utils.delete(`/api/CardDetails/${cardId}`);
            Utils.showNotification('Card removed successfully!', 'success');
            this.loadUserCards();
        } catch (error) {
            console.error('Error deleting card:', error);
            Utils.showNotification('Failed to remove card', 'error');
        }
    }

    async setDefaultCard(cardId) {
        try {
            await Utils.put(`/api/CardDetails/${cardId}/set-default`);
            Utils.showNotification('Default card updated!', 'success');
            this.loadUserCards();
        } catch (error) {
            console.error('Error setting default card:', error);
            Utils.showNotification('Failed to set default card', 'error');
        }
    }

    showChangePasswordModal() {
        document.getElementById('changePasswordForm').reset();
        Utils.showModal('changePasswordModal');
    }

    async changePassword() {
        const form = document.getElementById('changePasswordForm');
        const submitBtn = form.querySelector('button[type="submit"]');

        const formData = {
            userName: this.userData.userName || this.userData.firstName || '',
            currentPassword: document.getElementById('currentPassword').value,
            newPassword: document.getElementById('newPassword').value,
            confirmNewPassword: document.getElementById('confirmNewPassword').value
        };

        // Validation
        if (!formData.userName) {
            Utils.showNotification('Username not found', 'error');
            return;
        }
        if (!formData.currentPassword || !formData.newPassword || !formData.confirmNewPassword) {
            Utils.showNotification('Please fill in all fields', 'error');
            return;
        }

        if (!Utils.validatePassword(formData.newPassword)) {
            Utils.showNotification('New password must be at least 6 characters long', 'error');
            return;
        }

        if (formData.newPassword !== formData.confirmNewPassword) {
            Utils.showNotification('New passwords do not match', 'error');
            return;
        }

        try {
            Utils.showLoading(submitBtn);

            const response = await Utils.post('/api/AppUser/change-password', {
                userName: formData.userName,
                currentPassword: formData.currentPassword,
                newPassword: formData.newPassword
            });
            
            if (response && response.message) {
                Utils.showNotification('Password changed successfully!', 'success');
                Utils.hideModal('changePasswordModal');
                form.reset();
            } else {
                Utils.showNotification('Failed to change password', 'error');
            }
        } catch (error) {
            console.error('Error changing password:', error);
            Utils.showNotification('Failed to change password', 'error');
        } finally {
            Utils.hideLoading(submitBtn);
        }
    }
}

// Initialize account manager when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.accountManager = new AccountManager();
});

// Export for use in other files
window.AccountManager = AccountManager;
