// Main Application Module
class App {
    constructor() {
        this.initializeApp();
    }

    initializeApp() {
        this.setupGlobalEventListeners();
        this.setupMobileMenu();
        this.initializePageSpecificFeatures();
        this.setupGlobalModals();
    }

    setupGlobalEventListeners() {
        // Global click handlers
        document.addEventListener('click', (e) => {
            // Handle external links
            if (e.target.matches('a[href^="http"]')) {
                e.preventDefault();
                window.open(e.target.href, '_blank');
            }

            // Prevent default navigation for View All links
            const viewAll = e.target.closest('a.view-all-link');
            if (viewAll) {
                e.preventDefault();
                e.stopPropagation();
            }
        });

        // Handle form submissions globally
        document.addEventListener('submit', (e) => {
            const form = e.target;
            if (form.classList.contains('auth-form')) {
                // Auth forms are handled by auth.js
                return;
            }
            
            // Prevent default for forms that don't have specific handlers
            if (!form.dataset.handler) {
                e.preventDefault();
            }
        });

        // Handle navigation active states
        this.setupNavigationActiveStates();
    }

    setupMobileMenu() {
        Utils.setupMobileMenu();
    }

    setupNavigationActiveStates() {
        const currentPath = window.location.pathname;
        const navLinks = document.querySelectorAll('.nav-link');
        
        navLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href') === currentPath) {
                link.classList.add('active');
            }
        });
    }

    initializePageSpecificFeatures() {
        const currentPage = window.location.pathname;
        
        switch (currentPage) {
            case '/index.html':
            case '/':
                this.initializeHomePage();
                break;
            case '/dashboard.html':
                this.initializeDashboard();
                break;
            case '/match.html':
                this.initializeMatchPage();
                break;
            case '/merchandise.html':
                this.initializeMerchandisePage();
                break;
            case '/admin.html':
                this.initializeAdminPage();
                break;
        }
    }

    initializeHomePage() {
        // Home page specific initialization
        console.log('Home page initialized');
    }

    initializeDashboard() {
        // Dashboard specific initialization
        this.loadDashboardData();

        // Handle View All links
        document.addEventListener('click', (e) => {
            const viewAll = e.target.closest('.view-all-link');
            if (!viewAll) return;
            e.preventDefault();
            const section = viewAll.getAttribute('data-section');
            if (section === 'matches') {
                window.location.href = 'match.html';
                return;
            }
            if (section === 'tickets') {
                if (window.tickets && typeof window.tickets.showAllTicketsModal === 'function') {
                    window.tickets.showAllTicketsModal();
                }
                return;
            }
            if (section === 'rewards') {
                if (window.loyalty) {
                    window.loyalty.showRewardsModal();
                }
                return;
            }
        });
    }

    async loadDashboardData() {
        try {
            // Load dashboard statistics and data
            await this.loadDashboardStats();
        } catch (error) {
            console.error('Error loading dashboard data:', error);
        }
    }

    async loadDashboardStats() {
        // Load various dashboard statistics
        const stats = {
            upcomingMatches: window.matches ? window.matches.getUpcomingMatches().length : 0,
            recentTickets: window.tickets ? window.tickets.getRecentTickets().length : 0,
            loyaltyPoints: window.loyalty ? (window.loyalty.getLoyaltyData() ? window.loyalty.getLoyaltyData().points : 0) : 0,
            cartItems: window.merchandise ? window.merchandise.getCartItemCount() : 0
        };

        // Update dashboard stats if elements exist
        Object.entries(stats).forEach(([key, value]) => {
            const element = document.getElementById(`${key}Stat`);
            if (element) {
                element.textContent = value;
            }
        });
    }

    initializeMatchPage() {
        // Match page specific initialization
        console.log('Match page initialized');
    }

    initializeMerchandisePage() {
        // Merchandise page specific initialization
        console.log('Merchandise page initialized');
    }

    initializeAdminPage() {
        // Admin page specific initialization
        console.log('Admin page initialized');
    }

    setupGlobalModals() {
        // Setup global modal functionality
        const modals = document.querySelectorAll('.modal');
        modals.forEach(modal => {
            Utils.setupModalClose(modal.id);
        });
    }

    // Global utility functions
    showLoading(element) {
        if (element) {
            element.classList.add('loading');
            element.disabled = true;
        }
    }

    hideLoading(element) {
        if (element) {
            element.classList.remove('loading');
            element.disabled = false;
        }
    }

    // Handle API errors globally
    handleApiError(error, context = '') {
        console.error(`API Error in ${context}:`, error);
        
        if (error.status === 401) {
            Utils.showNotification('Session expired. Please login again.', 'error');
            setTimeout(() => {
                window.location.href = 'login.html';
            }, 2000);
        } else if (error.status === 403) {
            Utils.showNotification('Access denied. You do not have permission for this action.', 'error');
        } else if (error.status >= 500) {
            Utils.showNotification('Server error. Please try again later.', 'error');
        } else {
            Utils.showNotification('An error occurred. Please try again.', 'error');
        }
    }

    // Handle network errors
    handleNetworkError() {
        Utils.showNotification('Network error. Please check your connection and try again.', 'error');
    }

    // Global success handler
    handleSuccess(message, context = '') {
        Utils.showNotification(message, 'success');
    }

    // Handle page transitions
    setupPageTransitions() {
        // Add loading states for page transitions
        document.addEventListener('click', (e) => {
            const link = e.target.closest('a');
            if (!link) return;
            const hrefAttr = link.getAttribute('href') || '';
            if (!hrefAttr || hrefAttr === '#' || hrefAttr.startsWith('#') || hrefAttr.startsWith('javascript:') || link.classList.contains('view-all-link')) {
                return;
            }
            // Add loading state for internal links
            this.showPageLoading();
        });
    }

    showPageLoading() {
        // Create loading overlay
        const overlay = document.createElement('div');
        overlay.id = 'pageLoadingOverlay';
        overlay.innerHTML = `
            <div class="loading-spinner">
                <i class="fas fa-spinner fa-spin"></i>
                <p>Loading...</p>
            </div>
        `;
        overlay.style.cssText = `
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 10000;
            color: white;
        `;
        
        document.body.appendChild(overlay);
    }

    hidePageLoading() {
        const overlay = document.getElementById('pageLoadingOverlay');
        if (overlay) {
            overlay.remove();
        }
    }

    // Handle browser back/forward
    setupBrowserNavigation() {
        window.addEventListener('popstate', () => {
            this.initializePageSpecificFeatures();
        });
    }

    // Handle offline/online status
    setupOfflineHandling() {
        window.addEventListener('online', () => {
            Utils.showNotification('Connection restored!', 'success');
        });

        window.addEventListener('offline', () => {
            Utils.showNotification('You are offline. Some features may not work.', 'warning');
        });
    }

    // Handle service worker updates
    setupServiceWorker() {
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/sw.js')
                .then(registration => {
                    console.log('Service Worker registered:', registration);
                })
                .catch(error => {
                    console.error('Service Worker registration failed:', error);
                });
        }
    }

    // Initialize all global features
    setupGlobalFeatures() {
        this.setupPageTransitions();
        this.setupBrowserNavigation();
        this.setupOfflineHandling();
        this.setupServiceWorker();
    }

    // Handle theme switching (if implemented)
    setupThemeSwitcher() {
        const themeToggle = document.getElementById('themeToggle');
        if (themeToggle) {
            themeToggle.addEventListener('click', () => {
                document.body.classList.toggle('dark-theme');
                const isDark = document.body.classList.contains('dark-theme');
                localStorage.setItem('theme', isDark ? 'dark' : 'light');
            });
        }

        // Load saved theme
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme === 'dark') {
            document.body.classList.add('dark-theme');
        }
    }

    // Handle language switching (if implemented)
    setupLanguageSwitcher() {
        const languageSelect = document.getElementById('languageSelect');
        if (languageSelect) {
            languageSelect.addEventListener('change', (e) => {
                const language = e.target.value;
                localStorage.setItem('language', language);
                // Reload page to apply language change
                window.location.reload();
            });
        }

        // Load saved language
        const savedLanguage = localStorage.getItem('language');
        if (savedLanguage && languageSelect) {
            languageSelect.value = savedLanguage;
        }
    }

    // Handle accessibility features
    setupAccessibility() {
        // Keyboard navigation
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') {
                // Close any open modals
                const openModal = document.querySelector('.modal[style*="display: block"]');
                if (openModal) {
                    Utils.hideModal(openModal.id);
                }
            }
        });

        // Focus management
        document.addEventListener('focusin', (e) => {
            // Handle focus for modals
            if (e.target.closest('.modal')) {
                const modal = e.target.closest('.modal');
                const focusableElements = modal.querySelectorAll('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])');
                const firstElement = focusableElements[0];
                const lastElement = focusableElements[focusableElements.length - 1];

                if (e.target === firstElement && e.shiftKey) {
                    e.preventDefault();
                    lastElement.focus();
                } else if (e.target === lastElement && !e.shiftKey) {
                    e.preventDefault();
                    firstElement.focus();
                }
            }
        });
    }

    // Initialize performance monitoring
    setupPerformanceMonitoring() {
        // Monitor page load performance
        window.addEventListener('load', () => {
            const loadTime = performance.now();
            console.log(`Page loaded in ${loadTime.toFixed(2)}ms`);
        });

        // Monitor API response times
        const originalFetch = window.fetch;
        window.fetch = async (...args) => {
            const start = performance.now();
            try {
                const response = await originalFetch(...args);
                const end = performance.now();
                console.log(`API call to ${args[0]} took ${(end - start).toFixed(2)}ms`);
                return response;
            } catch (error) {
                const end = performance.now();
                console.error(`API call to ${args[0]} failed after ${(end - start).toFixed(2)}ms`);
                throw error;
            }
        };
    }
}

// Initialize app when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.app = new App();
    
    // Setup global features
    window.app.setupGlobalFeatures();
    window.app.setupThemeSwitcher();
    window.app.setupLanguageSwitcher();
    window.app.setupAccessibility();
    window.app.setupPerformanceMonitoring();
});

// Handle page visibility changes
document.addEventListener('visibilitychange', () => {
    if (document.visibilityState === 'visible') {
        // Page became visible - refresh data if needed
        if (window.app && window.app.loadDashboardData) {
            window.app.loadDashboardData();
        }
    }
});

// Handle beforeunload
window.addEventListener('beforeunload', (e) => {
    // Save any unsaved data
    if (window.merchandise && window.merchandise.cart.length > 0) {
        window.merchandise.saveCart();
    }
});

// Export for use in other files
window.App = App; 