// API Configuration
const API_BASE_URL = 'https://localhost:7001/api'; // Update this to match your ASP.NET Core API URL
const AUTH_DISABLED = true; // Temporary: disable authentication gating

// Utility Functions
class Utils {
    // API Request Helper
    static async apiRequest(endpoint, options = {}) {
        const token = localStorage.getItem('jwt_token');
        
        const defaultOptions = {
            headers: {
                'Content-Type': 'application/json',
                ...(token && { 'Authorization': `Bearer ${token}` })
            }
        };

        const config = {
            ...defaultOptions,
            ...options,
            headers: {
                ...defaultOptions.headers,
                ...options.headers
            }
        };

        try {
            const response = await fetch(`${API_BASE_URL}${endpoint}`, config);
            
            if (!AUTH_DISABLED && response.status === 401) {
                // Token expired or invalid
                localStorage.removeItem('jwt_token');
                localStorage.removeItem('user_data');
                window.location.href = 'login.html';
                return null;
            }

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('API Request Error:', error);
            throw error;
        }
    }

    // GET Request
    static async get(endpoint) {
        return this.apiRequest(endpoint, { method: 'GET' });
    }

    // POST Request
    static async post(endpoint, data) {
        return this.apiRequest(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
    }

    // PUT Request
    static async put(endpoint, data) {
        return this.apiRequest(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
    }

    // DELETE Request
    static async delete(endpoint) {
        return this.apiRequest(endpoint, { method: 'DELETE' });
    }

    // Token Management
    static getToken() {
        return localStorage.getItem('jwt_token');
    }

    static setToken(token) {
        localStorage.setItem('jwt_token', token);
    }

    static removeToken() {
        localStorage.removeItem('jwt_token');
    }

    static isAuthenticated() {
        if (AUTH_DISABLED) return true;
        return !!this.getToken();
    }

    // User Data Management
    static getUserData() {
        const userData = localStorage.getItem('user_data');
        return userData ? JSON.parse(userData) : null;
    }

    static setUserData(userData) {
        localStorage.setItem('user_data', JSON.stringify(userData));
    }

    static removeUserData() {
        localStorage.removeItem('user_data');
    }

    // Form Validation
    static validateEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    static validatePassword(password) {
        return password.length >= 6;
    }

    static validatePhone(phone) {
        const phoneRegex = /^[\+]?[1-9][\d]{0,15}$/;
        return phoneRegex.test(phone.replace(/\s/g, ''));
    }

    // Date Formatting
    static formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }

    static formatDateTime(dateString) {
        const date = new Date(dateString);
        return date.toLocaleString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    }

    static formatTime(dateString) {
        const date = new Date(dateString);
        return date.toLocaleTimeString('en-US', {
            hour: '2-digit',
            minute: '2-digit'
        });
    }

    // Currency Formatting
    static formatCurrency(amount) {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD'
        }).format(amount);
    }

    // Show/Hide Loading States
    static showLoading(button) {
        button.classList.add('loading');
        button.disabled = true;
    }

    static hideLoading(button) {
        button.classList.remove('loading');
        button.disabled = false;
    }

    // Show Notifications
    static showNotification(message, type = 'info') {
        const notification = document.createElement('div');
        notification.className = `notification notification-${type}`;
        notification.innerHTML = `
            <div class="notification-content">
                <i class="fas fa-${this.getNotificationIcon(type)}"></i>
                <span>${message}</span>
            </div>
        `;

        // Add styles if not already added
        if (!document.getElementById('notification-styles')) {
            const styles = document.createElement('style');
            styles.id = 'notification-styles';
            styles.textContent = `
                .notification {
                    position: fixed;
                    top: 20px;
                    right: 20px;
                    padding: 1rem 1.5rem;
                    border-radius: 5px;
                    color: white;
                    z-index: 10000;
                    animation: slideIn 0.3s ease-out;
                    max-width: 300px;
                }
                .notification-success { background: #27ae60; }
                .notification-error { background: #e74c3c; }
                .notification-warning { background: #f39c12; }
                .notification-info { background: #3498db; }
                .notification-content {
                    display: flex;
                    align-items: center;
                    gap: 0.5rem;
                }
                @keyframes slideIn {
                    from { transform: translateX(100%); opacity: 0; }
                    to { transform: translateX(0); opacity: 1; }
                }
            `;
            document.head.appendChild(styles);
        }

        document.body.appendChild(notification);

        // Auto remove after 5 seconds
        setTimeout(() => {
            notification.remove();
        }, 5000);
    }

    static getNotificationIcon(type) {
        const icons = {
            success: 'check-circle',
            error: 'exclamation-circle',
            warning: 'exclamation-triangle',
            info: 'info-circle'
        };
        return icons[type] || 'info-circle';
    }

    // Modal Management
    static showModal(modalId) {
        const modal = document.getElementById(modalId);
        if (modal) {
            modal.style.display = 'block';
            document.body.style.overflow = 'hidden';
        }
    }

    static hideModal(modalId) {
        const modal = document.getElementById(modalId);
        if (modal) {
            modal.style.display = 'none';
            document.body.style.overflow = 'auto';
        }
    }

    // Close modal when clicking outside
    static setupModalClose(modalId) {
        const modal = document.getElementById(modalId);
        if (modal) {
            modal.addEventListener('click', (e) => {
                if (e.target === modal) {
                    this.hideModal(modalId);
                }
            });

            const closeBtn = modal.querySelector('.close');
            if (closeBtn) {
                closeBtn.addEventListener('click', () => {
                    this.hideModal(modalId);
                });
            }
        }
    }

    // Tab Management
    static setupTabs(tabContainer) {
        const tabs = tabContainer.querySelectorAll('.tab-btn, .nav-tab');
        const panes = tabContainer.querySelectorAll('.tab-pane, .admin-content');

        tabs.forEach(tab => {
            tab.addEventListener('click', () => {
                const target = tab.getAttribute('data-tab');
                
                // Remove active class from all tabs and panes
                tabs.forEach(t => t.classList.remove('active'));
                panes.forEach(p => p.classList.remove('active'));
                panes.forEach(p => p.style.display = 'none');

                // Add active class to clicked tab
                tab.classList.add('active');

                // Show corresponding pane
                const targetPane = document.getElementById(target) || 
                                 document.querySelector(`[data-tab="${target}"]`);
                if (targetPane) {
                    targetPane.classList.add('active');
                    targetPane.style.display = 'block';
                }
            });
        });
    }

    // Mobile Menu Toggle
    static setupMobileMenu() {
        const hamburger = document.querySelector('.hamburger');
        const navMenu = document.querySelector('.nav-menu');

        if (hamburger && navMenu) {
            hamburger.addEventListener('click', () => {
                hamburger.classList.toggle('active');
                navMenu.classList.toggle('active');
            });

            // Close menu when clicking on a link
            navMenu.querySelectorAll('.nav-link').forEach(link => {
                link.addEventListener('click', () => {
                    hamburger.classList.remove('active');
                    navMenu.classList.remove('active');
                });
            });
        }
    }

    // Debounce Function
    static debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    // Generate QR Code (Simple implementation)
    static generateQRCode(text, containerId) {
        const container = document.getElementById(containerId);
        if (!container) return;

        // Simple QR code representation using CSS
        container.innerHTML = `
            <div style="
                width: 200px;
                height: 200px;
                background: white;
                border: 2px solid #333;
                display: flex;
                align-items: center;
                justify-content: center;
                font-family: monospace;
                font-size: 12px;
                text-align: center;
                word-break: break-all;
                padding: 10px;
            ">
                ${text}
            </div>
        `;
    }

    // Check if user is admin
    static isAdmin() {
        const userData = this.getUserData();
        return userData && userData.role === 'Admin';
    }

    // Redirect if not authenticated
    static requireAuth() {
        if (AUTH_DISABLED) return true;
        if (!this.isAuthenticated()) {
            window.location.href = 'login.html';
            return false;
        }
        return true;
    }

    // Redirect if not admin
    static requireAdmin() {
        if (AUTH_DISABLED) return true;
        if (!this.isAuthenticated()) {
            window.location.href = 'login.html';
            return false;
        }
        if (!this.isAdmin()) {
            window.location.href = 'dashboard.html';
            return false;
        }
        return true;
    }
}

// Export for use in other files
window.Utils = Utils; 