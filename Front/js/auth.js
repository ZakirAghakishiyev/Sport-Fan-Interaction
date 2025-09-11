// Authentication Module
class Auth {
    constructor() {
        this.initializeAuth();
    }

    initializeAuth() {
        this.setupLoginForm();
        this.setupRegisterForm();
        this.setupForgotPasswordForm();
        this.setupResetPasswordForm();
        this.setupLogout();
        this.updateNavigation();
    }

    setupLoginForm() {
        const loginForm = document.getElementById('loginForm');
        if (loginForm) {
            loginForm.addEventListener('submit', async (e) => {
                e.preventDefault();
                await this.handleLogin();
            });
        }
    }

    setupForgotPasswordForm() {
        const forgotForm = document.getElementById('forgotPasswordForm');
        if (forgotForm) {
            forgotForm.addEventListener('submit', async (e) => {
                e.preventDefault();
                await this.handleForgotPassword();
            });
        }
    }

    setupResetPasswordForm() {
        const resetForm = document.getElementById('resetPasswordForm');
        if (resetForm) {
            resetForm.addEventListener('submit', async (e) => {
                e.preventDefault();
                await this.handleResetPassword();
            });
        }
    }

    setupRegisterForm() {
        const registerForm = document.getElementById('registerForm');
        if (registerForm) {
            registerForm.addEventListener('submit', async (e) => {
                e.preventDefault();
                await this.handleRegister();
            });
        }
    }

    setupLogout() {
        const logoutBtn = document.getElementById('logoutBtn');
        if (logoutBtn) {
            logoutBtn.addEventListener('click', (e) => {
                e.preventDefault();
                this.logout();
            });
        }
    }

    async handleLogin() {
        const form = document.getElementById('loginForm');
        const submitBtn = form ? form.querySelector('button[type="submit"]') : null;
        const email = document.getElementById('email') ? document.getElementById('email').value : '';
        const password = document.getElementById('password') ? document.getElementById('password').value : '';

        // Validation (keep basic UX)
        if (!Utils.validateEmail(email)) {
            Utils.showNotification('Please enter a valid email address', 'error');
            return;
        }
        if (!Utils.validatePassword(password)) {
            Utils.showNotification('Password must be at least 6 characters long', 'error');
            return;
        }

        try {
            if (submitBtn) Utils.showLoading(submitBtn);

            if (typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED) {
                // Simulate login success
                Utils.setToken('demo_token');
                Utils.setUserData({ firstName: 'Demo', lastName: 'User', role: 'User', email });
                Utils.showNotification('Login successful (demo mode)', 'success');
                setTimeout(() => { window.location.href = 'dashboard.html'; }, 600);
                return;
            }

            const response = await Utils.post('/api/auth/login', { email, password });
            if (response && response.token) {
                Utils.setToken(response.token);
                Utils.setUserData(response.user);
                Utils.showNotification('Login successful!', 'success');
                setTimeout(() => { window.location.href = 'dashboard.html'; }, 1000);
            } else {
                Utils.showNotification('Login failed. Please check your credentials.', 'error');
            }
        } catch (error) {
            console.error('Login error:', error);
            Utils.showNotification('Login failed. Please try again.', 'error');
        } finally {
            if (submitBtn) Utils.hideLoading(submitBtn);
        }
    }

    async handleRegister() {
        const form = document.getElementById('registerForm');
        const submitBtn = form ? form.querySelector('button[type="submit"]') : null;

        const formData = {
            firstName: document.getElementById('firstName') ? document.getElementById('firstName').value : 'Demo',
            lastName: document.getElementById('lastName') ? document.getElementById('lastName').value : 'User',
            email: document.getElementById('email') ? document.getElementById('email').value : 'demo@example.com',
            phone: document.getElementById('phone') ? document.getElementById('phone').value : '+0000000000',
            password: document.getElementById('password') ? document.getElementById('password').value : '123456',
            confirmPassword: document.getElementById('confirmPassword') ? document.getElementById('confirmPassword').value : '123456',
            termsAccepted: document.getElementById('termsAccepted') ? document.getElementById('termsAccepted').checked : true
        };

        // Minimal UX checks
        if (!formData.firstName || !formData.lastName) {
            Utils.showNotification('Please enter your full name', 'error');
            return;
        }
        if (!Utils.validateEmail(formData.email)) {
            Utils.showNotification('Please enter a valid email address', 'error');
            return;
        }
        if (!Utils.validatePassword(formData.password)) {
            Utils.showNotification('Password must be at least 6 characters long', 'error');
            return;
        }
        if (formData.password !== formData.confirmPassword) {
            Utils.showNotification('Passwords do not match', 'error');
            return;
        }
        if (!formData.termsAccepted) {
            Utils.showNotification('Please accept the terms and conditions', 'error');
            return;
        }

        try {
            if (submitBtn) Utils.showLoading(submitBtn);

            if (typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED) {
                Utils.showNotification('Registration successful (demo mode). Please login.', 'success');
                setTimeout(() => { window.location.href = 'login.html'; }, 800);
                return;
            }

            const response = await Utils.post('/api/auth/register', {
                firstName: formData.firstName,
                lastName: formData.lastName,
                email: formData.email,
                phone: formData.phone,
                password: formData.password
            });

            if (response && response.success) {
                Utils.showNotification('Registration successful! Please login.', 'success');
                setTimeout(() => { window.location.href = 'login.html'; }, 1500);
            } else {
                Utils.showNotification('Registration failed. Please try again.', 'error');
            }
        } catch (error) {
            console.error('Registration error:', error);
            Utils.showNotification('Registration failed. Please try again.', 'error');
        } finally {
            if (submitBtn) Utils.hideLoading(submitBtn);
        }
    }

    async handleForgotPassword() {
        const form = document.getElementById('forgotPasswordForm');
        const submitBtn = form ? form.querySelector('button[type="submit"]') : null;
        const email = document.getElementById('email') ? document.getElementById('email').value : '';

        if (!Utils.validateEmail(email)) {
            Utils.showNotification('Please enter a valid email address', 'error');
            return;
        }

        try {
            if (submitBtn) Utils.showLoading(submitBtn);

            if (typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED) {
                Utils.showNotification('Reset link sent (demo mode). Check your email.', 'success');
                setTimeout(() => { window.location.href = 'reset-password.html?token=dummy'; }, 800);
                return;
            }

            await Utils.post('/api/auth/forgot-password', { email });
            Utils.showNotification('If the email exists, a reset link was sent.', 'success');
        } catch (error) {
            console.error('Forgot password error:', error);
            Utils.showNotification('Failed to request reset. Please try again.', 'error');
        } finally {
            if (submitBtn) Utils.hideLoading(submitBtn);
        }
    }

    async handleResetPassword() {
        const form = document.getElementById('resetPasswordForm');
        const submitBtn = form ? form.querySelector('button[type="submit"]') : null;
        const token = document.getElementById('token') ? document.getElementById('token').value.trim() : '';
        const password = document.getElementById('password') ? document.getElementById('password').value : '';
        const confirmPassword = document.getElementById('confirmPassword') ? document.getElementById('confirmPassword').value : '';

        if (!token) {
            Utils.showNotification('Reset token is required', 'error');
            return;
        }
        if (!Utils.validatePassword(password)) {
            Utils.showNotification('Password must be at least 6 characters long', 'error');
            return;
        }
        if (password !== confirmPassword) {
            Utils.showNotification('Passwords do not match', 'error');
            return;
        }

        try {
            if (submitBtn) Utils.showLoading(submitBtn);
            if (typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED) {
                Utils.showNotification('Password reset successful (demo mode). Please login.', 'success');
                setTimeout(() => { window.location.href = 'login.html'; }, 800);
                return;
            }
            await Utils.post('/api/auth/reset-password', { token, password });
            Utils.showNotification('Password reset successful. Please login.', 'success');
            setTimeout(() => { window.location.href = 'login.html'; }, 1200);
        } catch (error) {
            console.error('Reset password error:', error);
            Utils.showNotification('Reset failed. The token may be invalid or expired.', 'error');
        } finally {
            if (submitBtn) Utils.hideLoading(submitBtn);
        }
    }

    logout() {
        Utils.removeToken();
        Utils.removeUserData();
        Utils.showNotification('Logged out successfully', 'success');
        setTimeout(() => { window.location.href = 'index.html'; }, 600);
    }

    updateNavigation() {
        const userData = Utils.getUserData() || { firstName: 'Guest', lastName: 'User' };
        const isAuthenticated = Utils.isAuthenticated();

        const userNameElements = document.querySelectorAll('#userName, #userDisplayName');
        userNameElements.forEach(element => {
            element.textContent = `${userData.firstName} ${userData.lastName}`;
        });

        const loginBtn = document.getElementById('loginBtn');
        const registerBtn = document.getElementById('registerBtn');
        const userMenu = document.querySelector('.user-menu');

        if (isAuthenticated) {
            if (loginBtn) loginBtn.style.display = 'none';
            if (registerBtn) registerBtn.style.display = 'none';
            if (userMenu) userMenu.style.display = 'block';
        } else {
            if (loginBtn) loginBtn.style.display = 'inline-block';
            if (registerBtn) registerBtn.style.display = 'inline-block';
            if (userMenu) userMenu.style.display = 'none';
        }

        if (Utils.isAdmin && Utils.isAdmin()) {
            this.addAdminLink();
        }
    }

    addAdminLink() {
        const navMenu = document.querySelector('.nav-menu');
        if (navMenu && !document.querySelector('.admin-link')) {
            const adminLi = document.createElement('li');
            adminLi.className = 'admin-link';
            adminLi.innerHTML = '<a href="admin.html" class="nav-link">Admin</a>';
            navMenu.appendChild(adminLi);
        }
    }

    checkAuth() {
        // When disabled, always allow access
        if (typeof AUTH_DISABLED !== 'undefined' && AUTH_DISABLED) {
            return true;
        }
        const currentPage = window.location.pathname;
        const protectedPages = ['/dashboard.html', '/match.html', '/merchandise.html', '/admin.html'];
        if (protectedPages.includes(currentPage)) {
            if (!Utils.isAuthenticated()) {
                window.location.href = '/login.html';
                return false;
            }
        }
        if (currentPage === '/admin.html' && !Utils.isAdmin()) {
            window.location.href = '/dashboard.html';
            return false;
        }
        return true;
    }

    getCurrentUser() { return Utils.getUserData(); }
    hasRole(role) {
        const userData = Utils.getUserData();
        return userData && userData.role === role;
    }
    async refreshUserData() {
        try {
            const userData = await Utils.get('/api/auth/profile');
            if (userData) {
                Utils.setUserData(userData);
                this.updateNavigation();
            }
        } catch (error) {
            console.error('Error refreshing user data:', error);
        }
    }
}

// Initialize authentication when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.auth = new Auth();
    if (!window.auth.checkAuth()) { return; }
    window.auth.updateNavigation();
});

// Export for use in other files
window.Auth = Auth; 