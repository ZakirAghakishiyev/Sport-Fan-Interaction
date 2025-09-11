// Admin Module
class Admin {
    constructor() {
        this.stats = {};
        this.matches = [];
        this.products = [];
        this.users = [];
        this.orders = [];
        this.initializeAdmin();
    }

    initializeAdmin() {
        // Check admin access
        if (!Utils.requireAdmin()) {
            return;
        }

        this.loadAdminStats();
        this.loadDashboard();
        this.setupEventListeners();
        this.setupTabs();
    }

    setupEventListeners() {
        // Add match button
        const addMatchBtn = document.getElementById('addMatchBtn');
        if (addMatchBtn) {
            addMatchBtn.addEventListener('click', () => {
                this.showAddMatchModal();
            });
        }

        // Add product button
        const addProductBtn = document.getElementById('addProductBtn');
        if (addProductBtn) {
            addProductBtn.addEventListener('click', () => {
                this.showAddProductModal();
            });
        }

        // Modal setups
        const addMatchModal = document.getElementById('addMatchModal');
        const addProductModal = document.getElementById('addProductModal');
        
        if (addMatchModal) {
            Utils.setupModalClose('addMatchModal');
        }
        
        if (addProductModal) {
            Utils.setupModalClose('addProductModal');
        }

        // Form submissions
        const addMatchForm = document.getElementById('addMatchForm');
        const addProductForm = document.getElementById('addProductForm');

        if (addMatchForm) {
            addMatchForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.addMatch();
            });
        }

        if (addProductForm) {
            addProductForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.addProduct();
            });
        }

        // Cancel buttons
        const cancelMatchBtn = document.getElementById('cancelMatchBtn');
        const cancelProductBtn = document.getElementById('cancelProductBtn');

        if (cancelMatchBtn) {
            cancelMatchBtn.addEventListener('click', () => {
                Utils.hideModal('addMatchModal');
            });
        }

        if (cancelProductBtn) {
            cancelProductBtn.addEventListener('click', () => {
                Utils.hideModal('addProductModal');
            });
        }

        // User search
        const userSearch = document.getElementById('userSearch');
        if (userSearch) {
            userSearch.addEventListener('input', Utils.debounce(() => {
                this.searchUsers();
            }, 300));
        }

        // Order status filter
        const orderStatusFilter = document.getElementById('orderStatusFilter');
        if (orderStatusFilter) {
            orderStatusFilter.addEventListener('change', () => {
                this.filterOrders();
            });
        }
    }

    setupTabs() {
        const tabContainer = document.querySelector('.admin-nav');
        if (tabContainer) {
            const tabs = tabContainer.querySelectorAll('.nav-tab');
            tabs.forEach(tab => {
                tab.addEventListener('click', () => {
                    const target = tab.getAttribute('data-tab');
                    this.loadTabData(target);
                });
            });
            Utils.setupTabs(tabContainer);
            this.loadTabData('dashboard');
        }
    }

    async loadAdminStats() {
        try {
            // Compose simple stats from available endpoints
            const [matches, orders, users, payments] = await Promise.all([
                Utils.get('/api/Matches').catch(() => []),
                Utils.get('/api/Orders').catch(() => []),
                Utils.get('/api/AppUser').catch(() => []),
                Utils.get('/api/Payments').catch(() => [])
            ]);
            const totalRevenue = (orders || []).reduce((sum, o) => sum + (o.total || 0), 0) +
                                  (payments || []).reduce((sum, p) => sum + (p.amount || 0), 0);
            const stats = {
                totalTickets: (orders || []).reduce((sum, o) => sum + (o.ticketsCount || 0), 0),
                totalUsers: (users || []).length,
                totalOrders: (orders || []).length,
                totalRevenue
            };
            this.stats = stats || {};
            this.displayStats();
        } catch (error) {
            console.error('Error loading admin stats:', error);
            Utils.showNotification('Failed to load admin statistics', 'error');
        }
    }

    displayStats() {
        // Update stat cards
        const statElements = {
            'totalTickets': this.stats.totalTickets || 0,
            'totalUsers': this.stats.totalUsers || 0,
            'totalOrders': this.stats.totalOrders || 0,
            'totalRevenue': Utils.formatCurrency(this.stats.totalRevenue || 0)
        };

        Object.entries(statElements).forEach(([id, value]) => {
            const element = document.getElementById(id);
            if (element) {
                element.textContent = value;
            }
        });
    }

    async loadMatches() {
        try {
            const matches = await Utils.get('/api/Matches');
            this.matches = matches || [];
            this.renderMatches();
        } catch (error) {
            console.error('Error loading matches:', error);
            Utils.showNotification('Failed to load matches', 'error');
        }
    }

    renderMatches() {
        const container = document.getElementById('matchesList');
        if (!container) return;

        container.innerHTML = '';

        if (this.matches.length === 0) {
            container.innerHTML = '<p class="no-items">No matches found.</p>';
            return;
        }

        this.matches.forEach(match => {
            const matchItem = this.createMatchItem(match);
            container.appendChild(matchItem);
        });
    }

    createMatchItem(match) {
        const item = document.createElement('div');
        item.className = 'admin-item';
        item.innerHTML = `
            <div class="item-info">
                <h4>${match.homeTeam} vs ${match.awayTeam}</h4>
                <p>${Utils.formatDate(match.matchDate)} at ${Utils.formatTime(match.matchDate)}</p>
                <p>${match.stadium} - Capacity: ${match.capacity.toLocaleString()}</p>
            </div>
            <div class="admin-item-actions">
                <button class="btn btn-small btn-primary" onclick="window.admin.editMatch('${match.id}')">
                    <i class="fas fa-edit"></i> Edit
                </button>
                <button class="btn btn-small btn-secondary" onclick="window.admin.viewMatchDetails('${match.id}')">
                    <i class="fas fa-eye"></i> View
                </button>
                <button class="btn btn-small btn-danger" onclick="window.admin.deleteMatch('${match.id}')">
                    <i class="fas fa-trash"></i> Delete
                </button>
            </div>
        `;
        return item;
    }

    async loadProducts() {
        try {
            const products = await Utils.get('/api/Merchandise');
            this.products = products || [];
            this.renderProducts();
        } catch (error) {
            console.error('Error loading products:', error);
            Utils.showNotification('Failed to load products', 'error');
        }
    }

    renderProducts() {
        const container = document.getElementById('productsList');
        if (!container) return;

        container.innerHTML = '';

        if (this.products.length === 0) {
            container.innerHTML = '<p class="no-items">No products found.</p>';
            return;
        }

        this.products.forEach(product => {
            const productItem = this.createProductItem(product);
            container.appendChild(productItem);
        });
    }

    createProductItem(product) {
        const item = document.createElement('div');
        item.className = 'admin-item';
        item.innerHTML = `
            <div class="item-info">
                <h4>${product.name}</h4>
                <p>${product.description}</p>
                <p>${Utils.formatCurrency(product.price)} - Stock: ${product.stockQuantity}</p>
            </div>
            <div class="admin-item-actions">
                <button class="btn btn-small btn-primary" onclick="window.admin.editProduct('${product.id}')">
                    <i class="fas fa-edit"></i> Edit
                </button>
                <button class="btn btn-small btn-secondary" onclick="window.admin.viewProductDetails('${product.id}')">
                    <i class="fas fa-eye"></i> View
                </button>
                <button class="btn btn-small btn-danger" onclick="window.admin.deleteProduct('${product.id}')">
                    <i class="fas fa-trash"></i> Delete
                </button>
            </div>
        `;
        return item;
    }

    async loadUsers() {
        try {
            const users = await Utils.get('/api/AppUser');
            this.users = users || [];
            this.renderUsers();
        } catch (error) {
            console.error('Error loading users:', error);
            Utils.showNotification('Failed to load users', 'error');
        }
    }

    renderUsers() {
        const container = document.getElementById('usersList');
        if (!container) return;

        container.innerHTML = '';

        if (this.users.length === 0) {
            container.innerHTML = '<p class="no-items">No users found.</p>';
            return;
        }

        this.users.forEach(user => {
            const userItem = this.createUserItem(user);
            container.appendChild(userItem);
        });
    }

    createUserItem(user) {
        const item = document.createElement('div');
        item.className = 'admin-item';
        item.innerHTML = `
            <div class="item-info">
                <h4>${user.firstName} ${user.lastName}</h4>
                <p>${user.email}</p>
                <p>Role: ${user.role} - Joined: ${Utils.formatDate(user.createdAt)}</p>
            </div>
            <div class="admin-item-actions">
                <button class="btn btn-small btn-primary" onclick="window.admin.editUser('${user.id}')"><i class="fas fa-edit"></i> Edit</button>
                <button class="btn btn-small btn-secondary" onclick="window.admin.viewUserDetails('${user.id}')"><i class="fas fa-eye"></i> View</button>
                <button class="btn btn-small btn-warning" onclick="window.admin.addUserPoints('${user.id}')"><i class="fas fa-plus-circle"></i> Points</button>
                <button class="btn btn-small btn-info" onclick="window.admin.changeUserTier('${user.id}')"><i class="fas fa-layer-group"></i> Tier</button>
                <button class="btn btn-small btn-secondary" onclick="window.admin.viewUserCards('${user.id}')"><i class="fas fa-credit-card"></i> Cards</button>
                <button class="btn btn-small btn-danger" onclick="window.admin.deleteUser('${user.id}')"><i class="fas fa-trash"></i> Delete</button>
            </div>
        `;
        return item;
    }

    async viewUserDetails(userId) {
        try {
            const user = await Utils.get(`/api/AppUser/${userId}`);
            const loyalty = await Utils.get(`/api/AppUser/${userId}/loyalty`).catch(() => null);
            const info = [
                `Name: ${user.firstName} ${user.lastName}`,
                `Email: ${user.email}`,
                `Phone: ${user.phone || ''}`,
                `Role: ${user.role}`,
                loyalty ? `Tier: ${loyalty.tier} • Points: ${loyalty.points}` : null
            ].filter(Boolean).join('\n');
            alert(info);
        } catch (e) {
            Utils.showNotification('Failed to load user details', 'error');
        }
    }

    async editUser(userId) {
        try {
            const user = await Utils.get(`/api/AppUser/${userId}`);
            const firstName = prompt('First name', user.firstName);
            if (firstName === null) return;
            const lastName = prompt('Last name', user.lastName);
            if (lastName === null) return;
            const email = prompt('Email', user.email);
            if (email === null) return;
            const role = prompt('Role (User/Admin)', user.role);
            if (role === null) return;
            await Utils.put(`/api/AppUser/${userId}`, { ...user, firstName, lastName, email, role });
            Utils.showNotification('User updated', 'success');
            this.loadUsers();
        } catch (e) {
            Utils.showNotification('Failed to update user', 'error');
        }
    }

    async deleteUser(userId) {
        if (!confirm('Delete this user?')) return;
        try {
            await Utils.delete(`/api/AppUser/${userId}`);
            Utils.showNotification('User deleted', 'success');
            this.loadUsers();
        } catch (e) {
            Utils.showNotification('Failed to delete user', 'error');
        }
    }

    async addUserPoints(userId) {
        const amountStr = prompt('Add points amount', '10');
        if (amountStr === null) return;
        const amount = parseInt(amountStr, 10);
        if (Number.isNaN(amount) || amount <= 0) {
            Utils.showNotification('Invalid points amount', 'error');
            return;
        }
        try {
            await Utils.post(`/api/AppUser/${userId}/add-points`, { points: amount });
            Utils.showNotification('Points added', 'success');
        } catch (e) {
            Utils.showNotification('Failed to add points', 'error');
        }
    }

    async changeUserTier(userId) {
        const tier = prompt('Set tier (e.g., Bronze/Silver/Gold/Platinum)');
        if (tier === null || tier.trim() === '') return;
        try {
            await Utils.put(`/api/AppUser/${userId}/tier`, { tier });
            Utils.showNotification('Tier updated', 'success');
        } catch (e) {
            Utils.showNotification('Failed to update tier', 'error');
        }
    }

    async viewUserCards(userId) {
        try {
            const cards = await Utils.get(`/api/AppUser/${userId}/cards`);
            if (!cards || cards.length === 0) {
                alert('No cards on file.');
                return;
            }
            const lines = cards.map(c => `ID: ${c.id} • **** **** **** ${c.last4} • ${c.brand || ''}`);
            const choice = prompt(`Cards for user ${userId}:\n${lines.join('\n')}\nEnter card ID to delete, or leave blank.`);
            if (choice && choice.trim()) {
                await this.deleteUserCard(userId, choice.trim());
            }
        } catch (e) {
            Utils.showNotification('Failed to load user cards', 'error');
        }
    }

    async deleteUserCard(userId, cardId) {
        if (!confirm('Delete this card?')) return;
        try {
            await Utils.delete(`/api/AppUser/${userId}/cards/${cardId}`);
            Utils.showNotification('Card deleted', 'success');
        } catch (e) {
            Utils.showNotification('Failed to delete card', 'error');
        }
    }

    async loadOrders() {
        try {
            const orders = await Utils.get('/api/Orders');
            this.orders = orders || [];
            this.renderOrders();
        } catch (error) {
            console.error('Error loading orders:', error);
            Utils.showNotification('Failed to load orders', 'error');
        }
    }

    renderOrders() {
        const container = document.getElementById('ordersList');
        if (!container) return;

        container.innerHTML = '';

        if (this.orders.length === 0) {
            container.innerHTML = '<p class="no-items">No orders found.</p>';
            return;
        }

        this.orders.forEach(order => {
            const orderItem = this.createOrderItem(order);
            container.appendChild(orderItem);
        });
    }

    createOrderItem(order) {
        const item = document.createElement('div');
        item.className = 'admin-item';
        item.innerHTML = `
            <div class="item-info">
                <h4>Order #${order.id}</h4>
                <p>${order.customer.firstName} ${order.customer.lastName}</p>
                <p>${Utils.formatCurrency(order.total)} - ${order.status}</p>
                <p>${Utils.formatDate(order.orderDate)}</p>
            </div>
            <div class="admin-item-actions">
                <button class="btn btn-small btn-primary" onclick="window.admin.viewOrderDetails('${order.id}')">
                    <i class="fas fa-eye"></i> View
                </button>
                <button class="btn btn-small btn-secondary" onclick="window.admin.updateOrderStatus('${order.id}')">
                    <i class="fas fa-edit"></i> Status
                </button>
            </div>
        `;
        return item;
    }

    showAddMatchModal() {
        Utils.showModal('addMatchModal');
    }

    showAddProductModal() {
        Utils.showModal('addProductModal');
    }

    async addMatch() {
        const form = document.getElementById('addMatchForm');
        const submitBtn = form.querySelector('button[type="submit"]');

        const formData = {
            homeTeam: document.getElementById('homeTeam').value,
            awayTeam: document.getElementById('awayTeam').value,
            matchDate: document.getElementById('matchDate').value,
            matchTime: document.getElementById('matchTime').value,
            stadium: document.getElementById('stadium').value,
            capacity: parseInt(document.getElementById('capacity').value),
            basePrice: parseFloat(document.getElementById('basePrice').value)
        };

        // Validation
        if (!formData.homeTeam || !formData.awayTeam || !formData.matchDate || !formData.stadium) {
            Utils.showNotification('Please fill in all required fields', 'error');
            return;
        }

        try {
            Utils.showLoading(submitBtn);
            let response;
            if (form.dataset.editId) {
                response = await Utils.put(`/api/Matches/${form.dataset.editId}`, {
                    ...formData,
                    matchDate: `${formData.matchDate}T${formData.matchTime}:00`
                });
            } else {
                response = await Utils.post('/api/Matches', {
                    ...formData,
                    matchDate: `${formData.matchDate}T${formData.matchTime}:00`
                });
            }
            Utils.showNotification('Match saved successfully!', 'success');
            Utils.hideModal('addMatchModal');
            form.removeAttribute('data-edit-id');
            form.querySelector('button[type="submit"]').textContent = 'Add Match';
            form.reset();
            this.loadMatches();
        } catch (error) {
            console.error('Error adding match:', error);
            Utils.showNotification('Failed to add match', 'error');
        } finally {
            Utils.hideLoading(submitBtn);
        }
    }

    async addProduct() {
        const form = document.getElementById('addProductForm');
        const submitBtn = form.querySelector('button[type="submit"]');

        const formData = {
            name: document.getElementById('productName').value,
            description: document.getElementById('productDescription').value,
            price: parseFloat(document.getElementById('productPrice').value),
            category: document.getElementById('productCategory').value,
            stockQuantity: parseInt(document.getElementById('productStock').value),
            imageUrl: document.getElementById('productImage').value
        };

        // Validation
        if (!formData.name || !formData.description || !formData.price || !formData.category) {
            Utils.showNotification('Please fill in all required fields', 'error');
            return;
        }

        try {
            Utils.showLoading(submitBtn);
            let response;
            if (form.dataset.editId) {
                response = await Utils.put(`/api/Merchandise/${form.dataset.editId}`, formData);
            } else {
                response = await Utils.post('/api/Merchandise', formData);
            }
            Utils.showNotification('Product saved successfully!', 'success');
            Utils.hideModal('addProductModal');
            form.removeAttribute('data-edit-id');
            form.querySelector('button[type="submit"]').textContent = 'Add Product';
            form.reset();
            this.loadProducts();
        } catch (error) {
            console.error('Error adding product:', error);
            Utils.showNotification('Failed to add product', 'error');
        } finally {
            Utils.hideLoading(submitBtn);
        }
    }

    async editMatch(matchId) {
        try {
            const match = await Utils.get(`/api/Matches/${matchId}`);
            if (match) {
                // Populate form with match data
                document.getElementById('homeTeam').value = match.homeTeam;
                document.getElementById('awayTeam').value = match.awayTeam;
                document.getElementById('matchDate').value = match.matchDate.split('T')[0];
                document.getElementById('matchTime').value = match.matchDate.split('T')[1].substring(0, 5);
                document.getElementById('stadium').value = match.stadium;
                document.getElementById('capacity').value = match.capacity;
                document.getElementById('basePrice').value = match.basePrice;

                // Change form action to update
                const form = document.getElementById('addMatchForm');
                form.dataset.editId = matchId;
                form.querySelector('button[type="submit"]').textContent = 'Update Match';

                Utils.showModal('addMatchModal');
            }
        } catch (error) {
            console.error('Error loading match for edit:', error);
            Utils.showNotification('Failed to load match details', 'error');
        }
    }

    async editProduct(productId) {
        try {
            const product = await Utils.get(`/api/Merchandise/${productId}`);
            if (product) {
                // Populate form with product data
                document.getElementById('productName').value = product.name;
                document.getElementById('productDescription').value = product.description;
                document.getElementById('productPrice').value = product.price;
                document.getElementById('productCategory').value = product.category;
                document.getElementById('productStock').value = product.stockQuantity;
                document.getElementById('productImage').value = product.imageUrl;

                // Change form action to update
                const form = document.getElementById('addProductForm');
                form.dataset.editId = productId;
                form.querySelector('button[type="submit"]').textContent = 'Update Product';

                Utils.showModal('addProductModal');
            }
        } catch (error) {
            console.error('Error loading product for edit:', error);
            Utils.showNotification('Failed to load product details', 'error');
        }
    }

    async deleteMatch(matchId) {
        if (confirm('Are you sure you want to delete this match?')) {
            try {
                await Utils.delete(`/api/Matches/${matchId}`);
                Utils.showNotification('Match deleted successfully!', 'success');
                this.loadMatches();
            } catch (error) {
                console.error('Error deleting match:', error);
                Utils.showNotification('Failed to delete match', 'error');
            }
        }
    }

    async deleteProduct(productId) {
        if (confirm('Are you sure you want to delete this product?')) {
            try {
                await Utils.delete(`/api/Merchandise/${productId}`);
                Utils.showNotification('Product deleted successfully!', 'success');
                this.loadProducts();
            } catch (error) {
                console.error('Error deleting product:', error);
                Utils.showNotification('Failed to delete product', 'error');
            }
        }
    }

    searchUsers() {
        const searchTerm = document.getElementById('userSearch').value.toLowerCase();
        const filteredUsers = this.users.filter(user => 
            user.firstName.toLowerCase().includes(searchTerm) ||
            user.lastName.toLowerCase().includes(searchTerm) ||
            user.email.toLowerCase().includes(searchTerm)
        );
        this.renderFilteredUsers(filteredUsers);
    }

    renderFilteredUsers(users) {
        const container = document.getElementById('usersList');
        if (!container) return;

        container.innerHTML = '';

        if (users.length === 0) {
            container.innerHTML = '<p class="no-items">No users found matching your search.</p>';
            return;
        }

        users.forEach(user => {
            const userItem = this.createUserItem(user);
            container.appendChild(userItem);
        });
    }

    filterOrders() {
        const statusFilter = document.getElementById('orderStatusFilter').value;
        const filteredOrders = statusFilter 
            ? this.orders.filter(order => order.status === statusFilter)
            : this.orders;
        this.renderFilteredOrders(filteredOrders);
    }

    renderFilteredOrders(orders) {
        const container = document.getElementById('ordersList');
        if (!container) return;

        container.innerHTML = '';

        if (orders.length === 0) {
            container.innerHTML = '<p class="no-items">No orders found matching your criteria.</p>';
            return;
        }

        orders.forEach(order => {
            const orderItem = this.createOrderItem(order);
            container.appendChild(orderItem);
        });
    }

    // Load data for specific tabs
    loadTabData(tabName) {
        switch (tabName) {
            case 'dashboard':
                this.loadDashboard();
                break;
            case 'matches':
                this.loadMatches();
                break;
            case 'merchandise':
                this.loadProducts();
                break;
            case 'users':
                this.loadUsers();
                break;
            case 'orders':
                this.loadOrders();
                break;
            case 'tickets':
                this.loadTickets();
                break;
            case 'stadiums':
                this.loadStadiums();
                break;
            case 'sectors':
                this.loadSectors();
                break;
            case 'seats':
                this.loadSeats();
                break;
            case 'payments':
                this.loadPayments();
                break;
            case 'cards':
                this.loadCards();
                break;
        }
    }

    // Dashboard loaders
    async loadDashboard() {
        try {
            const [matches, orders, payments] = await Promise.all([
                Utils.get('/api/Matches').catch(() => []),
                Utils.get('/api/Orders').catch(() => []),
                Utils.get('/api/Payments').catch(() => [])
            ]);
            const now = new Date();
            const upcoming = (matches || []).filter(m => new Date(m.matchDate) > now).slice(0, 5);
            const recentOrders = (orders || []).slice(0, 5);
            const recentPayments = (payments || []).slice(0, 5);
            this.renderSimpleList('upcomingMatchesList', upcoming, (m) => `${m.homeTeam} vs ${m.awayTeam} • ${Utils.formatDateTime(m.matchDate)}`);
            this.renderSimpleList('recentOrdersList', recentOrders, (o) => `#${o.id} • ${Utils.formatCurrency(o.total)} • ${o.status}`);
            this.renderSimpleList('recentPaymentsList', recentPayments, (p) => `#${p.id} • ${Utils.formatCurrency(p.amount)} • ${Utils.formatDateTime(p.date || p.createdAt)}`);
        } catch (e) {
            console.error('Failed to load dashboard', e);
        }
    }

    renderSimpleList(containerId, items, mapFn) {
        const container = document.getElementById(containerId);
        if (!container) return;
        container.innerHTML = '';
        if (!items || items.length === 0) {
            container.innerHTML = '<p class="no-items">No data found.</p>';
            return;
        }
        items.forEach(i => {
            const el = document.createElement('div');
            el.className = 'admin-item';
            el.innerHTML = `<div class="item-info"><p>${mapFn(i)}</p></div>`;
            container.appendChild(el);
        });
    }

    // Tickets
    async loadTickets() {
        try {
            const tickets = await Utils.get('/api/Tickets');
            this.renderSimpleList('ticketsList', tickets || [], (t) => `#${t.id} • ${t.match?.homeTeam || ''} vs ${t.match?.awayTeam || ''} • ${t.status || 'issued'}`);
        } catch (e) {
            Utils.showNotification('Failed to load tickets', 'error');
        }
    }

    // Stadiums/Sectors/Seats
    async loadStadiums() {
        try {
            const stadiums = await Utils.get('/api/Stadiums');
            this.renderSimpleList('stadiumsList', stadiums || [], (s) => `${s.name} • Capacity: ${s.capacity}`);
        } catch (e) { Utils.showNotification('Failed to load stadiums', 'error'); }
    }
    async loadSectors() {
        try {
            const sectors = await Utils.get('/api/Sectors');
            this.renderSimpleList('sectorsList', sectors || [], (s) => `${s.name} • Stadium: ${s.stadiumName || s.stadiumId}`);
        } catch (e) { Utils.showNotification('Failed to load sectors', 'error'); }
    }
    async loadSeats() {
        try {
            const seats = await Utils.get('/api/Seat');
            this.renderSimpleList('seatsList', seats || [], (s) => `Row ${s.row}, Seat ${s.number} • Sector: ${s.sectorName || s.sectorId}`);
        } catch (e) { Utils.showNotification('Failed to load seats', 'error'); }
    }

    // Payments
    async loadPayments() {
        try {
            const payments = await Utils.get('/api/Payments');
            this.renderSimpleList('paymentsList', payments || [], (p) => `#${p.id} • ${Utils.formatCurrency(p.amount)} • ${p.method || ''}`);
        } catch (e) { Utils.showNotification('Failed to load payments', 'error'); }
    }

    // Cards
    async loadCards() {
        try {
            const cards = await Utils.get('/api/CardDetails');
            this.renderSimpleList('cardsList', cards || [], (c) => `**** **** **** ${String(c.last4 || '').padStart(4, '•')}`);
        } catch (e) { Utils.showNotification('Failed to load cards', 'error'); }
    }
}

// Initialize admin when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.admin = new Admin();
});

// Export for use in other files
window.Admin = Admin; 