# Football Club Ticket Selling Website - Frontend

A modern, responsive frontend for a football club ticket selling website built with vanilla HTML, CSS, and JavaScript. This application provides a complete user experience for booking match tickets, shopping for merchandise, and managing loyalty rewards.

## 🚀 Features

### User Features
- **Authentication & User Management**
  - User registration and login
  - JWT token-based authentication
  - User profile management
  - Secure logout functionality

- **Match & Ticket Booking**
  - Browse upcoming matches
  - Interactive seat selection with stadium layout
  - Real-time pricing calculation
  - Ticket booking with confirmation
  - QR code generation for tickets
  - Ticket download functionality

- **Loyalty & Rewards System**
  - Earn points with every purchase
  - Multiple loyalty tiers (Bronze, Silver, Gold, Platinum, Diamond)
  - Progress tracking to next tier
  - View and redeem rewards
  - Points calculation based on tier

- **Merchandise Shop**
  - Browse official club merchandise
  - Shopping cart functionality
  - Secure checkout process
  - Order history and tracking
  - Product filtering and sorting

- **Dashboard**
  - Personalized user dashboard
  - Quick access to all features
  - Recent activity overview
  - Loyalty status display

### Admin Features
- **Match Management**
  - Create and edit matches
  - Set pricing for different sectors
  - Manage match details and capacity

- **Merchandise Management**
  - Add and edit products
  - Manage inventory
  - Set pricing and categories

- **User Management**
  - View all users
  - Search and filter users
  - Manage user roles

- **Order Management**
  - View all orders
  - Update order status
  - Track revenue and statistics

## 📁 Project Structure

```
FrontEndTest/
├── index.html              # Home page
├── login.html              # Login page
├── register.html           # Registration page
├── dashboard.html          # User dashboard
├── match.html              # Match details and booking
├── merchandise.html        # Shop page
├── admin.html              # Admin panel
├── css/
│   └── style.css          # Main stylesheet
├── js/
│   ├── utils.js           # Utility functions and API helpers
│   ├── auth.js            # Authentication module
│   ├── matches.js         # Match and ticket booking
│   ├── loyalty.js         # Loyalty points and rewards
│   ├── merchandise.js     # Shop and cart functionality
│   ├── tickets.js         # Ticket management
│   ├── admin.js           # Admin panel functionality
│   └── main.js            # Main application module
└── README.md              # This file
```

## 🛠️ Technologies Used

- **HTML5**: Semantic markup and structure
- **CSS3**: Modern styling with Flexbox and Grid
- **Vanilla JavaScript**: ES6+ features and modular architecture
- **Font Awesome**: Icons and visual elements
- **Local Storage**: Client-side data persistence
- **Fetch API**: HTTP requests to backend

## 🚀 Getting Started

### Prerequisites
- A modern web browser (Chrome, Firefox, Safari, Edge)
- A local web server (for development)
- ASP.NET Core Web API backend (for full functionality)

### Installation

1. **Clone or download the project files**
   ```bash
   git clone <repository-url>
   cd FrontEndTest
   ```

2. **Set up a local web server**
   
   **Option 1: Using Python**
   ```bash
   # Python 3
   python -m http.server 8000
   
   # Python 2
   python -m SimpleHTTPServer 8000
   ```

   **Option 2: Using Node.js**
   ```bash
   npx http-server -p 8000
   ```

   **Option 3: Using PHP**
   ```bash
   php -S localhost:8000
   ```

3. **Configure API endpoint**
   
   Open `js/utils.js` and update the API base URL:
   ```javascript
   const API_BASE_URL = 'https://localhost:7001/api'; // Update to your API URL
   ```

4. **Access the application**
   
   Open your browser and navigate to:
   ```
   http://localhost:8000
   ```

## 📋 API Endpoints

The frontend expects the following API endpoints from your ASP.NET Core backend:

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/auth/profile` - Get user profile

### Matches
- `GET /api/matches` - Get all matches
- `GET /api/matches/{id}` - Get match details
- `GET /api/matches/{id}/sector-prices` - Get sector pricing

### Tickets
- `POST /api/tickets` - Book a ticket
- `GET /api/tickets` - Get user tickets
- `GET /api/tickets/{id}` - Get ticket details
- `GET /api/tickets/{id}/download` - Download ticket

### Loyalty
- `GET /api/loyalty/profile` - Get loyalty status
- `GET /api/loyalty/rewards` - Get user rewards
- `POST /api/loyalty/earn-points` - Earn points
- `POST /api/loyalty/redeem/{id}` - Redeem reward

### Merchandise
- `GET /api/merchandise` - Get all products
- `GET /api/merchandise/{id}` - Get product details

### Orders
- `POST /api/orders` - Create order
- `GET /api/orders` - Get user orders

### Admin (Admin role required)
- `GET /api/admin/stats` - Get admin statistics
- `GET /api/admin/matches` - Get all matches for admin
- `POST /api/admin/matches` - Create match
- `PUT /api/admin/matches/{id}` - Update match
- `DELETE /api/admin/matches/{id}` - Delete match
- `GET /api/admin/products` - Get all products for admin
- `POST /api/admin/products` - Create product
- `PUT /api/admin/products/{id}` - Update product
- `DELETE /api/admin/products/{id}` - Delete product
- `GET /api/admin/users` - Get all users
- `GET /api/admin/orders` - Get all orders

## 🎨 Customization

### Styling
The application uses a modern, responsive design with CSS custom properties for easy theming. To customize the appearance:

1. **Colors**: Update CSS custom properties in `css/style.css`
   ```css
   :root {
       --primary-color: #667eea;
       --secondary-color: #764ba2;
       --success-color: #27ae60;
       --danger-color: #e74c3c;
       --warning-color: #f39c12;
   }
   ```

2. **Fonts**: Change the font family in the CSS
   ```css
   body {
       font-family: 'Your-Font', sans-serif;
   }
   ```

### Configuration
Update the API configuration in `js/utils.js`:
```javascript
const API_BASE_URL = 'your-api-base-url';
```

## 🔧 Development

### Adding New Features

1. **Create new JavaScript module**
   ```javascript
   // js/new-feature.js
   class NewFeature {
       constructor() {
           this.initialize();
       }
       
       initialize() {
           // Initialize feature
       }
   }
   
   // Initialize when DOM is loaded
   document.addEventListener('DOMContentLoaded', () => {
       window.newFeature = new NewFeature();
   });
   ```

2. **Add to HTML pages**
   ```html
   <script src="js/new-feature.js"></script>
   ```

### Debugging
- Open browser developer tools (F12)
- Check the Console tab for JavaScript errors
- Use the Network tab to monitor API requests
- Use the Application tab to inspect localStorage

## 📱 Responsive Design

The application is fully responsive and works on:
- Desktop computers (1200px+)
- Tablets (768px - 1199px)
- Mobile phones (320px - 767px)

## 🔒 Security Features

- JWT token authentication
- Secure token storage in localStorage
- Automatic token refresh handling
- Input validation and sanitization
- XSS protection through proper DOM manipulation

## 🚀 Performance Optimizations

- Lazy loading of images
- Efficient DOM manipulation
- Debounced search functionality
- Optimized API calls
- Minimal dependencies

## 🧪 Testing

To test the application:

1. **Test user flows**:
   - Registration and login
   - Browsing matches
   - Booking tickets
   - Shopping for merchandise
   - Managing loyalty points

2. **Test responsive design**:
   - Resize browser window
   - Test on different devices
   - Check mobile menu functionality

3. **Test error handling**:
   - Disconnect internet
   - Enter invalid data
   - Test with expired tokens

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Check the browser console for errors
- Verify API endpoint configuration
- Ensure all files are properly loaded
- Test with a different browser

## 🔄 Updates

To update the application:
1. Backup your current files
2. Download the latest version
3. Replace files (preserve your customizations)
4. Test all functionality
5. Update API endpoints if needed

---

**Note**: This frontend is designed to work with an ASP.NET Core Web API backend. Make sure your backend implements all the required endpoints and follows the expected data formats. 

---

### 1. **Static Demo Data**

**Matches Example:**
```js
[
  {
    id: 1,
    homeTeam: "FC Example",
    awayTeam: "Demo United",
    matchDate: "2024-07-15T19:00:00",
    stadium: "Demo Stadium",
    capacity: 40000,
    basePrice: 25
  },
  ...
]
```

**Tickets Example:**
```js
[
  {
    id: 101,
    match: { ...matchObject },
    sector: "north1",
    price: 35,
    status: "confirmed"
  },
  ...
]
```

**Products Example:**
```js
[
  {
    id: 201,
    name: "Official Jersey",
    description: "2024 Home Jersey",
    price: 59.99,
    category: "jerseys",
    stockQuantity: 100,
    imageUrl: "https://via.placeholder.com/300x200?text=Jersey"
  },
  ...
]
```

---

### 2. **Implementation Plan**

- Update each module to use static data if the API call fails or always (for now).
- Add a `USE_DEMO_DATA` flag at the top of each file for easy switching.
- Demo data will be shown for:
  - Matches (on home, dashboard, match page)
  - Tickets (on dashboard)
  - Merchandise (shop page, cart, checkout)

---

I will now update the three JS modules (`matches.js`, `tickets.js`, `merchandise.js`) to use static demo data for a full preview experience. 